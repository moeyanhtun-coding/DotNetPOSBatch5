using DotNetPOS.Database.Models;
using DotNetPOS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPOS.Domain.Features
{
    public class ProductService
    {
        private readonly AppDbContext _db;

        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<List<TblProduct>>> GetProductsAsync()
        {
            try
            {
                var products = await _db.TblProducts.AsNoTracking().ToListAsync();

                var model = Result<List<TblProduct>>.Success(products);
                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<Result<TblProduct>> GetProductWithCode(string productCode)
        {
            try
            {
                var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == productCode);
                if (product is null)
                {
                    return Result<TblProduct>.NoProductFound("No product found with this product code");
                }
                var model = Result<TblProduct>.Success(product);
                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<Result<TblProduct>> CreateProduct(ProductRequestModel product)
        {
            try
            {
                var item = new TblProduct()
                {
                    ProductCode = "PDT-" + Ulid.NewUlid().ToString(),
                    Name = product.Name,
                    Price = product.Price,
                    ProductCategoryCode = product.ProductCategoryCode
                };
                await _db.TblProducts.AddAsync(item);
                await _db.SaveChangesAsync();
                var model = Result<TblProduct>.Success(item);
                return model;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<Result<TblProduct>> ChangeProductInfo(int productId, ChangeProductInfoRequestModel productInfo)
        {
            try
            {
                var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == productId);
                
                if (product is null)
                {
                    return Result<TblProduct>.NoProductFound("Product Id Not Found");
                }
                if (!string.IsNullOrEmpty(productInfo.Name))
                {
                    product.Name = productInfo.Name;
                } 
                if (productInfo.Price is not null)
                {
                    product.Price = productInfo.Price.Value;
                }
                _db.Entry(product).State = EntityState.Modified;
                
                var result = _db.SaveChangesAsync();
                return Result<TblProduct>.Success(product);

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        //public async Task<Result<string>> DeleteProduct(int productId)
        //{
        //    try
        //    {
        //        var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == productId);

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
