using DotNetPOS.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace DotNetPOS.Domain.Features
{
    public class ProductService
    {
        private readonly AppDbContext _db = new AppDbContext();
        public List<TblProduct> GetProducts()
        {
            var lst = _db.TblProducts.ToList();
            return lst;
        }

        public TblProduct GetProduct(string productCode)
        {
            var item = _db.TblProducts.FirstOrDefault(x => x.ProductCode == productCode);
            if (item is null)
            {
                return null;
            }
            return item;
        }

        public int ProductCreate(string productName, double price, string productCategoryCode)
        {
            TblProduct product = new TblProduct()
            {
                ProductCode = Ulid.NewUlid().ToString(),
                Name = productName,
                Price = price,
                ProductCategoryCode = productCategoryCode
            };
            
             _db.TblProducts.Add(product);
            var result = _db.SaveChanges();
            return result;
        }

       public int ProductUpdate(string productCode, double price, string productCategoryCode, string productName )
        {
            var item =_db.TblProducts.AsNoTracking().FirstOrDefault(x => x.ProductCode == productCode);
            item.Name = productName;
            item.Price = price;
            item.ProductCategoryCode = productCategoryCode;
            _db.Entry(item).State = EntityState.Modified;
          var result = _db.SaveChanges();
            return result;
        }

        public int ProductDelete(string productCode)
        {
            var item = _db.TblProducts.AsNoTracking().FirstOrDefault(x => x.ProductCode == productCode);
            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return result;
        }
    }
}
