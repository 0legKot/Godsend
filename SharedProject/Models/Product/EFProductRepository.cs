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
        /// The creationlock
        /// </summary>
        private static object creationlock = new object();

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

        /// <summary>
        /// Initializes a new instance of the <see cref="EFProductRepository" /> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        /// <param name="userManager">The user manager.</param>
        public EFProductRepository(DataContext ctx, UserManager<IdentityUser> userManager)
        {
            context = ctx;

            SeedHelper.EnsurePopulated(ctx);
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
        public void SaveEntity(Product entity)
        {
            Product dbEntry = context.Products.Include(p => p.Info).Include(p => p.Category).FirstOrDefault(p => p.Id == entity.Id);
            if (dbEntry != null)
            {
                // TODO: implement IClonable
                dbEntry.Info.Name = entity.Info.Name;
                dbEntry.Info.Description = entity.Info.Description;
            }
            else
            {
                ////entity.SetIds();
                context.Add(entity);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        public void DeleteEntity(Guid infoId)
        {
            Product dbEntry = context.Products.Include(p => p.Info).Include(p => p.Category).FirstOrDefault(p => p.Info.Id == infoId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
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
            return context.Products.Include(p => p.Info).Include(p => p.Category).FirstOrDefault(p => p.Id == entityId);
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
        /// <param name="productInfoId">The product information identifier.</param>
        /// <returns></returns>
        public ProductWithSuppliers GetProductWithSuppliers(Guid productInfoId)
        {
            var tmp = context.LinkProductsSuppliers
                    .Include(ps => ps.Product)
                    .ThenInclude(s => s.Info)
                    .Include(ps => ps.Supplier)
                    .ThenInclude(s => s.Info)
                    .Include(ps => ps.Supplier)
                    .ThenInclude(x => x.Info.Location)
                    .ToArray();

            var res = new ProductWithSuppliers
            {
                Product = GetEntityByInfoId(productInfoId),
                Suppliers = tmp
                    .Where(link => link.Product.Info.Id == productInfoId)
                    .Select(link => new SupplierAndPrice { Supplier = link.Supplier, Price = link.Price })
                    .ToArray(),
                DecimalProps = context.LinkProductPropertyDecimal.Include(lpp => lpp.Property).Where(lpp => lpp.Product.Info.Id == productInfoId),
                StringProps = context.LinkProductPropertyString.Include(lpp => lpp.Property).Where(lpp => lpp.Product.Info.Id == productInfoId),
                IntProps = context.LinkProductPropertyInt.Include(lpp => lpp.Property).Where(lpp => lpp.Product.Info.Id == productInfoId)
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
            return context.Products.Include(p => p.Info).Include(p => p.Category).Skip(skip).Take(quantity);
        }

        /// <summary>
        /// Categorieses this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> Categories()
        {
            var res = context.Categories.Include(c => c.BaseCategory);
            for (int i = 0; i < maxDepth; i++)
            {
                res = res.ThenInclude(c => c.BaseCategory);
            }

            return res;
        }

        /// <summary>
        /// Get properties related to specified <see cref="Category"/>
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns></returns>
        public IEnumerable<object> Properties(Guid categoryId)
        {
            return context.Properties.Include(x => x.RelatedCategory).Where(x => x.RelatedCategory.Id == categoryId).Select(x => new { x.Id, x.Name, x.Type });
        }

        

        public IQueryable<ProductInformation> GetOrdered(IQueryable<ProductInformation> infosToSort, OrderBy orderBy)
        {
            switch (orderBy)
            {
                case OrderBy.Name: return infosToSort.OrderBy(x => x.Name);
                case OrderBy.Watches: return infosToSort.OrderBy(x => x.Watches);
                case OrderBy.Rating: return infosToSort.OrderBy(x => x.Rating);
                default: return infosToSort;
            }
        }

        public IEnumerable<ProductInformation> GetProductInformationsByFilter(FilterInfo filter, int quantity = 10, int skip = 0, OrderBy orderBy = OrderBy.Rating)
        {
            // intersect
            IEnumerable<ProductInformation> res = Enumerable.Empty<ProductInformation>();
            if (filter.IntProps.Any())
            {
                var tmp = GetOrdered(FilterByInt(filter.IntProps, filter.SortingPropertyId), orderBy);
                res = res.Any() ? res.Intersect(tmp) : tmp;
            }

            if (filter.DecimalProps.Any())
            {
                var tmp = GetOrdered(FilterByDecimal(filter.DecimalProps, filter.SortingPropertyId), orderBy);
                res = res.Any() ? res.Intersect(tmp) : tmp;
            }

            if (filter.StringProps.Any())
            {
                var tmp = GetOrdered(FilterByString(filter.StringProps, filter.SortingPropertyId), orderBy);
                res = res.Any() ? res.Intersect(tmp) : tmp;
            }

            res = res.Skip(skip).Take(quantity);
            return res;
        }

        /// <summary>
        /// Filters the by int.
        /// </summary>
        /// <param name="props">The props.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public IQueryable<ProductInformation> FilterByInt(IEnumerable<IntPropertyInfo> props,Guid orderPropertyId)
        {
            var tmp = context.LinkProductPropertyInt
                .Include(p => p.Property)
                .Include(p => p.Product).ThenInclude(p => p.Info)
                .Where(p => props.Any(x => p.Property.Id == x.PropId));

            foreach (var prop in props)
            {
                tmp = tmp.Where(p => prop.PropId != p.Property.Id || (prop.PropId == p.Property.Id && p.Value >= prop.Left && p.Value <= prop.Right));
            }
            return GetOrderedByProperty(orderPropertyId, tmp);
        }

        private static IQueryable<ProductInformation> GetOrderedByProperty<T>(Guid orderPropertyId, IQueryable<EAV<T>> tmp)
        {
            return tmp.OrderBy(x => x.Property.Id != orderPropertyId).ThenBy(x => x.Value).Select(x => x.Product.Info).Distinct();
        }

        /// <summary>
        /// Filters the by decimal.
        /// </summary>
        /// <param name="props">The props.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public IQueryable<ProductInformation> FilterByDecimal(IEnumerable<DecimalPropertyInfo> props,Guid orderPropertyId)
        {
            var tmp = context.LinkProductPropertyDecimal
                .Include(p => p.Property)
                .Include(p => p.Product).ThenInclude(p => p.Info).Where(p => props.Any(x => p.Property.Id == x.PropId));

            foreach (var prop in props)
            {
                tmp = tmp.Where(p => prop.PropId != p.Property.Id || (prop.PropId == p.Property.Id && p.Value >= prop.Left && p.Value <= prop.Right));
            }

            return tmp.OrderBy(x => x.Property.Id != orderPropertyId).ThenBy(x => x.Value).Select(x => x.Product.Info).Distinct();
        }

        /// <summary>
        /// Filters the by string.
        /// </summary>
        /// <param name="props">The props.</param>
        /// <param name="quantity">The quantity.</param>
        /// <param name="skip">The skip.</param>
        /// <returns></returns>
        public IQueryable<ProductInformation> FilterByString(IEnumerable<StringPropertyInfo> props,Guid orderPropertyId)
        {
            var tmp = context.LinkProductPropertyString
                .Include(p => p.Property)
                .Include(p => p.Product).ThenInclude(p => p.Info).Where(p => props.Any(x => p.Property.Id == x.PropId));

            foreach (var prop in props)
            {
                tmp = tmp.Where(p => prop.PropId != p.Property.Id || (prop.PropId == p.Property.Id && prop.Part == p.Value));
            }

            return GetOrderedByProperty(orderPropertyId, tmp);
        }

        /// <summary>
        /// Products the properties int.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<object> ProductPropertiesInt(Guid id)
        {
            return context.LinkProductPropertyInt
                .Include(x => x.Product).ThenInclude(x => x.Info)
                .Include(x => x.Property)
                ////.ThenInclude(x => x.RelatedCategory)
                .Where(x => x.Product.Info.Id == id).Select(p => new { p.Property.Id, p.Property.Name, p.Value });
        }

        /// <summary>
        /// Products the properties decimal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<object> ProductPropertiesDecimal(Guid id)
        {
            return context.LinkProductPropertyDecimal
                .Include(x => x.Product).ThenInclude(x => x.Info)
                .Include(x => x.Property)
                ////.ThenInclude(x => x.RelatedCategory)
                .Where(x => x.Product.Info.Id == id).Select(p => new { p.Property.Id, p.Property.Name, p.Value });
        }

        /// <summary>
        /// Products the properties string.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IEnumerable<object> ProductPropertiesString(Guid id)
        {
            return context.LinkProductPropertyString
                .Include(x => x.Product).ThenInclude(x => x.Info)
                .Include(x => x.Property)
                ////.ThenInclude(x => x.RelatedCategory)
                .Where(x => x.Product.Info.Id == id).Select(p => new { p.Property.Id, p.Property.Name, p.Value });
        }

        /// <summary>
        /// Gets the entity by information identifier.
        /// </summary>
        /// <param name="infoId">The information identifier.</param>
        /// <returns></returns>
        public Product GetEntityByInfoId(Guid infoId)
        {
            return context.Products.Include(p => p.Info).Include(p => p.Category).FirstOrDefault(p => p.Info.Id == infoId);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class IntPropertyInfo: IPropertyInfo
    {
        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public Guid PropId { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public int Left { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public int Right { get; set; }
    }
    public interface IPropertyInfo
    {
        Guid PropId { get; set; }
    }
    /// <summary>
    ///
    /// </summary>
    public class DecimalPropertyInfo: IPropertyInfo
    {
        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public Guid PropId { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        public decimal Left { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>
        /// The right.
        /// </value>
        public decimal Right { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class StringPropertyInfo: IPropertyInfo
    {
        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        public Guid PropId { get; set; }

        /// <summary>
        /// Gets or sets the part.
        /// </summary>
        /// <value>
        /// The part.
        /// </value>
        public string Part { get; set; }
    }

}
