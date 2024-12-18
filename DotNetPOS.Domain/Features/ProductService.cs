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

        public async Task<Result<List<ProductRespModel>>> GetProductsAsync()
        {
            try
            {
                var products = await _db.TblProducts
                    .AsNoTracking()
                    .Where(x => x.DeleteFlag == false)
                    .ToListAsync();

                var model = products.Select(x => new ProductRespModel()
                {
                    ProductCode = x.ProductCode,
                    Name = x.Name,
                    Price = x.Price,
                    ProductCategoryCode = x.ProductCategoryCode
                }).ToList();

                return Result<List<ProductRespModel>>.Success(model);
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
                var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == productCode && x.DeleteFlag == false);
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

        public async Task<Result<ProductRespModel>> CreateProduct(ProductRequestModel product)
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
                var model = new ProductRespModel()
                {
                    ProductCode = item.ProductCode,
                    Name = item.Name,
                    Price = item.Price,
                    ProductCategoryCode = item.ProductCategoryCode
                };
                return Result<ProductRespModel>.Success(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<Result<string>> ChangeProductInfo(string productCode, ChangeProductInfoRequestModel productInfo)
        {
            try
            {
                var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == productCode);
                
                if (product is null)
                {
                    return Result<string>.NoProductFound("Product Id Not Found");
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
                return Result<string>.Success("Product Info Changed Successfully");

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<Result<string>> DeleteProduct(string productCode)
        {
            try
            {
                var product = await _db.TblProducts.AsNoTracking().FirstOrDefaultAsync(x => x.ProductCode == productCode);
                if (product is null)
                {
                    return Result<string>.NoProductFound("Product id is not found");
                }
                product.DeleteFlag = true;
                _db.Entry(product).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Result<string>.Success("Product is deleted successfully");

            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }

    }
}
