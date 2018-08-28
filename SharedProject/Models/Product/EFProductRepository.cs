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
    public class EFProductRepository : IProductRepository
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
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        /// <value>
        /// The entities.
        /// </value>
        public IEnumerable<Product> Entities(int quantity, int skip = 0) => GetProductsFromContext(quantity, skip);

        /// <summary>
        /// Gets the entities information.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        /// <value>
        /// The entities information.
        /// </value>
        public IEnumerable<Information> EntitiesInfo(int quantity, int skip = 0) => Entities(quantity, skip).Select(p => p.Info);

        /// <summary>
        /// Saves the entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task SaveEntity(Product entity)
        {
            Product dbEntry = context.Products.FirstOrDefault(p => p.Id == entity.Id);
            if (dbEntry != null)
            {
                entity.CopyTo(dbEntry);
            }
            else
            {
                context.Add(entity);
            }

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="id">The product identifier.</param>
        public async Task DeleteEntity(Guid id)
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
        /// Determines whether the specified entity is first.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        ///   <c>true</c> if the specified entity is first; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFirst(Product entity)
        {
            return !context.Products.Any(p => p.Id == entity.Id);
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        /// <returns></returns>
        public Product GetEntity(Guid entityId)
        {
            return context.Products.FirstOrDefault(p => p.Id == entityId);
        }

        /// <summary>
        /// Watches the specified product.
        /// </summary>
        /// <param name="prod">The product.</param>
        public void Watch(Product prod)
        {
            if (prod != null)
            {
                ++prod.Info.Watches;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the product with suppliers.
        /// </summary>
        /// <param name="productId">The product information identifier.</param>
        /// <returns></returns>
        public ProductWithSuppliers GetProductWithSuppliers(Guid productId)
        {
            var res = new ProductWithSuppliers
            {
                Product = GetEntity(productId),
                Suppliers = context.LinkProductsSuppliers
                    .Where(link => link.Product.Id == productId)
                    .Select(link => new SupplierAndPrice { Supplier = link.Supplier, Price = link.Price })
                    .ToArray(),
                DecimalProps = context.LinkProductPropertyDecimal.Where(lpp => lpp.Product.Id == productId),
                StringProps = context.LinkProductPropertyString.Where(lpp => lpp.Product.Id == productId),
                IntProps = context.LinkProductPropertyInt.Where(lpp => lpp.Product.Id == productId)
            };
            return res;
        }

        /// <summary>
        /// Gets products from context.
        /// </summary>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns>
        /// {quantity} products after {skip} skipped
        /// </returns>
        private IQueryable<Product> GetProductsFromContext(int quantity, int skip = 0)
        {
            return context.Products.Skip(skip).Take(quantity);
        }

        /// <summary>
        /// Categorieses this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> Categories()
        {
            return context.Categories;
        }

        /// <summary>
        /// Get properties related to specified <see cref="Category"/>
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns></returns>
        public IEnumerable<object> Properties(Guid categoryId)
        {
            return context.Properties.Where(x => x.RelatedCategory.Id == categoryId).Select(x => new { x.Id, x.Name, x.Type });
        }

        public int EntitiesCount()
        {
            return context.Products.Count();
        }

        public ProductInfosAndCount GetProductInformationsByProductFilter(ProductFilterInfo filter)
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
            return products; //.Where(p => p.Category != null && p.Category.HasParent(categoryId));
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

        public async Task<double> SetRatingAsync(Guid productId, string userId, int rating)
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

        public IEnumerable<LinkRatingEntity> GetAllRatings(Guid productId)
        {
            return context.LinkRatingProduct.Where(lra => lra.EntityId == productId);
        }

        public int? GetUserRating(Guid productId, string userId)
        {
            return context.LinkRatingProduct.FirstOrDefault(lra => lra.EntityId == productId && lra.UserId == userId)?.Rating;
        }

        public async Task<Guid> AddCommentAsync(Guid productId, string userId, Guid? baseCommentId, string comment)
        {
            var newCommentId = await commentHelper.AddCommentGenericAsync<LinkCommentProduct>(context, productId, userId, baseCommentId, comment);

            await RecalcCommentsAsync(productId); // or just increment?

            return newCommentId;
        }

        public IEnumerable<LinkCommentEntity> GetAllComments(Guid productId)
        {
            var fortst = context.LinkCommentProduct.Where(lra => lra.Product.Id == productId)
                .Select(x => new LinkCommentProduct() { BaseComment = x.BaseComment, Comment = x.Comment, Id = x.Id, User = x.User });
            return fortst;
        }

        public async Task DeleteCommentAsync(Guid productId, Guid commentId, string userId)
        {
            await commentHelper.DeleteCommentGenericAsync(context.LinkCommentProduct, context, productId, commentId);

            await RecalcCommentsAsync(productId);
        }

        public async Task EditCommentAsync(Guid commentId, string newContent, string userId)
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
