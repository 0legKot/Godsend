// <copyright file="EFProductRepository.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///
    /// </summary>
    public class EFProductRepository : AProductRepository
    {
        /// <summary>
        /// The admin user
        /// </summary>
        private const string adminUser = "Admin";

        /// <summary>
        /// The admin password
        /// </summary>
        private const string adminPassword = "Secret123$";

        /// <summary>
        /// The maximum depth
        /// </summary>
        private const int maxDepth = 5;

        /// <summary>
        /// The context
        /// </summary>
        private DataContext context;

        private IRatingHelper ratingHelper;

        private ICommentHelper commentHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFProductRepository" /> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public EFProductRepository(DataContext ctx, ISeedHelper seedHelper, IRatingHelper ratingHelper, ICommentHelper commentHelper)
        {
            context = ctx;
            this.ratingHelper = ratingHelper;
            this.commentHelper = commentHelper;
            seedHelper.EnsurePopulated(ctx);
            cats = context.Categories;
        }

        protected override IQueryable<Product> EntitiesSource { get => context.Products; }

        protected override void SaveChangedState(Product changedProduct)
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public override async Task SaveEntity(Product entity)
        {
            Product dbEntry = context.Products.FirstOrDefault(p => p.Id == entity.Id);
            if (dbEntry != null)
            {
                // todo do something with properties

                context.RemoveRange(dbEntry.DecimalProps);
                context.RemoveRange(dbEntry.IntProps);
                context.RemoveRange(dbEntry.StringProps);
                context.RemoveRange(dbEntry.LinkProductsSuppliers);
                context.RemoveRange(dbEntry.LinkProductImages);

                entity.CopyTo(dbEntry);
            }
            else
            {
                entity.Category = context.Categories.FirstOrDefault(c => c.BaseCategory == null);
                context.Add(entity);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        public override async Task DeleteEntity(Guid id)
        {
            Product dbEntry = context.Products.FirstOrDefault(p => p.Id == id);
            if (dbEntry != null)
            {
                var quantumMagic = dbEntry.Info;
                ////context.RemoveRange(context.LinkProductPropertyDecimal.Where(p => p.Product.Id == dbEntry.Id));
                ////context.RemoveRange(context.LinkProductPropertyInt.Where(p => p.Product.Id == dbEntry.Id));
                ////context.RemoveRange(context.LinkProductPropertyString.Where(p => p.Product.Id == dbEntry.Id));
                ////context.RemoveRange(context.LinkProductsSuppliers.Where(p => p.Product.Id == dbEntry.Id));
                ////context.RemoveRange(context.LinkRatingProduct.Where(lrp => lrp.EntityId == dbEntry.Id));
                ////context.RemoveRange(context.LinkCommentProduct.Where(lrp => lrp.ProductId == dbEntry.Id));
                context.Products.Remove(dbEntry);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Categorieses this instance.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Category> Categories()
        {
            return context.Categories;
        }

        private IEnumerable<Category> cats; 
        /// <summary>
        /// Get properties related to specified <see cref="Category"/>
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns></returns>
        public override IEnumerable<object> Properties(Guid categoryId)
        {
            return context.Properties.Where(x => x.RelatedCategory.Id == categoryId).Select(x => new { x.Id, x.Name, x.Type });
        }

        public override ProductInfosAndCount GetProductInformationsByProductFilter(ProductFilterInfo filter)
        {
            IQueryable<Product> products = context.Products;

            if (filter.CategoryId.HasValue)
            {
                products = FilterByCategory(products, filter.CategoryId.Value);
            }

            if (filter.IntProps != null && filter.IntProps.Any())
            {
                products = FilterByIntProps(products, filter.IntProps);
            }

            if (filter.DecimalProps != null && filter.DecimalProps.Any())
            {
                products = FilterByDecimalProps(products, filter.DecimalProps);
            }

            if (filter.StringProps != null && filter.StringProps.Any())
            {
                products = FilterByStringProps(products, filter.StringProps);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                products = FilterBySearch(products, filter.SearchTerm);
            }

            if (filter.OrderBy == OrderBy.Property && !filter.SortingPropertyId.HasValue)
            {
                throw new ArgumentException("Sorting property must have value");
            }

            products = OrderProducts(products, filter.OrderBy, filter.SortingPropertyId, filter.SortAscending);

            return new ProductInfosAndCount
            {
                Count = products.Count(),
                Infos = products.Skip(filter.Skip).Take(filter.Quantity).Select(p => p.Info).ToList()
            };
        }

        #region Filter private methods

        private IQueryable<Product> OrderProducts(IQueryable<Product> products, OrderBy orderBy, Guid? sortingPropertyId, bool sortAscending)
        {
            IQueryable<Product> tmp;

            switch (orderBy)
            {
                case OrderBy.Name:
                    tmp = products.OrderByDescending(p => p.Info.Name);
                    break;
                case OrderBy.Rating:
                    tmp = products.OrderByDescending(p => p.Info.Rating);
                    break;
                case OrderBy.Watches:
                    tmp = products.OrderByDescending(p => p.Info.Watches);
                    break;
                case OrderBy.Property:
                    return OrderByProperty(products, sortingPropertyId.Value, sortAscending);
                default:
                    throw new ArgumentException("Illegal sort type");
            }

            return MaybeReverse(tmp, sortAscending);
        }

        private IQueryable<T> MaybeReverse<T>(IQueryable<T> query, bool reverse)
        {
            return reverse ? query.AsEnumerable().Reverse().AsQueryable() : query;
        }

        private IQueryable<Product> OrderByProperty(IQueryable<Product> products, Guid sortingPropertyId, bool sortAscending)
        {
            var property = context.Properties.First(p => p.Id == sortingPropertyId);

            switch (property.Type)
            {
                case PropertyTypes.Decimal:
                    return OrderByPropertyCommon(products, sortingPropertyId, context.LinkProductPropertyDecimal, sortAscending);
                case PropertyTypes.Int:
                    return OrderByPropertyCommon(products, sortingPropertyId, context.LinkProductPropertyInt, sortAscending);
                case PropertyTypes.String:
                    return OrderByPropertyCommon(products, sortingPropertyId, context.LinkProductPropertyString, sortAscending);
                default:
                    throw new ArgumentException("Unknown property type");
            }
        }

        private IQueryable<Product> OrderByPropertyCommon<T>(IEnumerable<Product> products, Guid sortingPropertyId, IQueryable<EAV<T>> eavs, bool sortAscending)
        {
            var tmp = products.GroupJoin(
                    eavs.Where(lpp => lpp.Property.Id == sortingPropertyId),
                    p => p.Id,
                    lpp => lpp.Product.Id,
                    (p, lpp) => new { Product = p, Links = lpp })
                .OrderByDescending(x => x.Links.Any());

            if (sortAscending)
            {
                tmp = tmp.ThenBy(x =>
                {
                    var t = x.Links.FirstOrDefault();
                    return t == null ? default(T) : t.Value;
                });
            }
            else
            {
                tmp = tmp.ThenByDescending(x =>
                {
                    var t = x.Links.FirstOrDefault();
                    return t == null ? default(T) : t.Value;
                });
            }

            return tmp.Select(x => x.Product).AsQueryable();
        }

        private IQueryable<Product> FilterBySearch(IQueryable<Product> products, string searchTerm)
        {
            return products.Where(p => p.Info.Name.ToLower().Contains(searchTerm.ToLower()));
        }

        private IQueryable<Product> FilterByCategory(IQueryable<Product> products, Guid categoryId)
        {
            var curCat = new CatWithSubs() { Cat = cats.FirstOrDefault(x => x.Id == categoryId) };
            var apropriateCats = GetRecursiveCats(ref curCat);
            apropriateCats.Add(curCat.Cat);
            return products.Where(p => apropriateCats.Contains(p.Category)); 
        }

        private IQueryable<Product> FilterByDecimalProps(IQueryable<Product> products, IEnumerable<DecimalPropertyInfo> decimalProps)
        {
            var tmp = products.GroupJoin(
                    context.LinkProductPropertyDecimal,
                    p => p.Id,
                    lpp => lpp.Product.Id,
                    (p, lpp) => new { Product = p, Links = lpp });

            foreach (var prop in decimalProps)
            {
                tmp = tmp.Where(group => group.Links.Any(lpp => lpp.Property.Id == prop.PropId && lpp.Value >= prop.Left && lpp.Value <= prop.Right));
            }

            return tmp.Select(group => group.Product);
        }

        private IQueryable<Product> FilterByStringProps(IQueryable<Product> products, IEnumerable<StringPropertyInfo> stringProps)
        {
            var tmp = products.GroupJoin(
                    context.LinkProductPropertyString,
                    p => p.Id,
                    lpp => lpp.Product.Id,
                    (p, lpp) => new { Product = p, Links = lpp });

            foreach (var prop in stringProps)
            {
                tmp = tmp.Where(group => group.Links.Any(lpp => lpp.Property.Id == prop.PropId && lpp.Value.Contains(prop.Part)));
            }

            return tmp.Select(group => group.Product);
        }

        private IQueryable<Product> FilterByIntProps(IQueryable<Product> products, IEnumerable<IntPropertyInfo> intProps)
        {
            var tmp = products.GroupJoin(
                    context.LinkProductPropertyInt,
                    p => p.Id,
                    lpp => lpp.Product.Id,
                    (p, lpp) => new { Product = p, Links = lpp });

            foreach (var prop in intProps)
            {
                tmp = tmp.Where(group => group.Links.Any(lpp => lpp.Property.Id == prop.PropId && lpp.Value >= prop.Left && lpp.Value <= prop.Right));
            }

            return tmp.Select(group => group.Product);
        }

        #endregion

        public IEnumerable<Category> GetSubCategories(Guid id)
        {
            return Categories().Where(x => x.BaseCategory?.Id == id);
        }

        private List<Category> GetRecursiveCats(ref CatWithSubs cur)
        {
            var result = new List<Category>();
            var subs = new List<CatWithSubs>();
            var curSubCats = GetSubCategories(cur.Cat.Id);
            if (curSubCats.Any())
            {
                foreach (var cat in curSubCats)
                {
                    result.Add(cat);
                    var tmp = new CatWithSubs() { Cat = cat };
                    result.AddRange(GetRecursiveCats(ref tmp));
                    var tmpClone = new CatWithSubs() { Cat = tmp.Cat, Subs = tmp.Subs };
                    subs.Add(tmpClone);
                }
            }

            cur.Subs = subs;
            return result;
        }
        
        public override async Task<double> SetRatingAsync(Guid productId, string userId, int rating)
        {
            await ratingHelper.SetRatingAsync(productId, userId, rating, context.LinkRatingProduct, context);

            return await RecalcRatings(productId);
        }

        private async Task<double> RecalcRatings(Guid productId)
        {
            var avg = await ratingHelper.CalculateAverageAsync(context.LinkRatingProduct, productId);

            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            product.Info.Rating = avg;

            await context.SaveChangesAsync();

            return avg;
        }

        public override IEnumerable<LinkRatingEntity> GetAllRatings(Guid productId)
        {
            return context.LinkRatingProduct.Where(lra => lra.EntityId == productId);
        }

        public override int? GetUserRating(Guid productId, string userId)
        {
            return context.LinkRatingProduct.FirstOrDefault(lra => lra.EntityId == productId && lra.UserId == userId)?.Rating;
        }

        public override async Task<Guid> AddCommentAsync(Guid productId, string userId, Guid? baseCommentId, string comment)
        {
            var newCommentId = await commentHelper.AddCommentGenericAsync<LinkCommentProduct>(context, productId, userId, baseCommentId, comment);

            await RecalcCommentsAsync(productId); // or just increment?

            return newCommentId;
        }

        public override IEnumerable<LinkCommentEntity> GetAllComments(Guid productId)
        {
            var fortst = context.LinkCommentProduct.Where(lra => lra.Product.Id == productId)
                .Select(x => new LinkCommentProduct() { BaseComment = x.BaseComment, Comment = x.Comment, Id = x.Id, User = x.User });
            return fortst;
        }

        public override async Task DeleteCommentAsync(Guid productId, Guid commentId, string userId)
        {
            await commentHelper.DeleteCommentGenericAsync(context.LinkCommentProduct, context, productId, commentId);

            await RecalcCommentsAsync(productId);
        }

        public override async Task EditCommentAsync(Guid commentId, string newContent, string userId)
        {
            var comment = await context.LinkCommentProduct.FirstOrDefaultAsync(lce => lce.Id == commentId);
            if (comment.UserId != userId) throw new InvalidOperationException("Incorrect user tried to edit comment");
            comment.Comment = newContent;

            await context.SaveChangesAsync();
        }

        public async Task RecalcCommentsAsync(Guid productId)
        {
            var commentCount = await context.LinkCommentProduct.CountAsync(lce => lce.ProductId == productId);

            var product = await context.Products.FirstOrDefaultAsync(a => a.Id == productId);

            product.Info.CommentsCount = commentCount;

            await context.SaveChangesAsync();
        }
    }
}
