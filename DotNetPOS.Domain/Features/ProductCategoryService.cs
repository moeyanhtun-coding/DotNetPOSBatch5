using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetPOS.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetPOS.Domain.Features
{
    public class ProductCategoryService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public List<TblProductCategory> GetProductCategories()
        {
            var lst = _db.TblProductCategories.ToList();
            return lst;
        }
        public TblProductCategory GetProductCategoryByCode(string code)
        {
            var item = _db.TblProductCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.ProductCategoryCode == code)!;
            if (item is null)
                return null!;
            return item;
        }
        public int PostProductCategory(string categoryName)
        {
            var item = new TblProductCategory
            {
                ProductCategoryCode = "PC-" + Ulid.NewUlid().ToString(),
                Name = categoryName
            };
            _db.Add(item);
            var result = _db.SaveChanges();
            return result;
        }

        public int UpdateProductCategory(string categoryCode , string categoryName)
        {
            var item = _db.TblProductCategories
                .AsNoTracking()
                .FirstOrDefault(x => x.ProductCategoryCode == categoryCode);
            if(item is null)
                return 0;
            if(!string.IsNullOrEmpty(categoryName))
                item.Name = categoryName;
            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return result;
        }
        public int DeleteProductCategory(string categoryCode)
        {
            var item = _db.TblProductCategories
                .AsNoTracking()
                .FirstOrDefault(x =>x.ProductCategoryCode == categoryCode);
            if (item is null)
                return 0;
            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Modified;
           var result = _db.SaveChanges();
            return result;
        }
    }
}
