using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetPOS.Database.Models;
using DotNetPOS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetPOS.Domain.Features
{
    public class ProductCategoryService
    {
        // applied dependency injection
        private readonly AppDbContext _db;

        public ProductCategoryService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<List<ProductCategoryRespModel>>> GetProductCategories()
        {
            var lst = await _db.TblProductCategories.AsNoTracking().ToListAsync();
            var model = lst.Select(x => new ProductCategoryRespModel()
            {
                Name = x.Name,
                ProductCategoryCode = x.ProductCategoryCode,
            }).ToList();

            return Result<List<ProductCategoryRespModel>>.Success(model);
        }
        public async Task<Result<ProductCategoryRespModel>> GetProductCategoryByCode(string code)
        {
            var item = await _db.TblProductCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductCategoryCode == code)!;
            if (item is null)
            {
                return Result<ProductCategoryRespModel>.NoProductFound("Product Category Not Found");
            }
            var model = new ProductCategoryRespModel()
            {
                Name = item.Name,
                ProductCategoryCode = item.ProductCategoryCode,
            };
            return Result<ProductCategoryRespModel>.Success(model);
        }
        public async Task<Result<string>> PostProductCategory(string categoryName)
        {
            var item = new TblProductCategory
            {
                ProductCategoryCode = "PC-" + Ulid.NewUlid().ToString(),
                Name = categoryName
            };
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
            return Result<string>.Success("New Product Category Created Successfully");
        }

        public async Task<Result<string>> UpdateProductCategory(string categoryCode , string categoryName)
        {
            var item = await _db.TblProductCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductCategoryCode == categoryCode);
            if (item is null)
                return Result<string>.NoProductFound("No category found");
            if(!string.IsNullOrEmpty(categoryName))
                item.Name = categoryName;
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return Result<string>.Success("Category Updated Successfully");
        }
        public async Task<Result<string>> DeleteProductCategory(string categoryCode)
        {
            var item = await _db.TblProductCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>x.ProductCategoryCode == categoryCode);
            if (item is null)
                return Result<string>.NoProductFound("Category not found");

            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Deleted;
           await _db.SaveChangesAsync();
            return Result<string>.Success("Deleted Successfully");
        }
    }
}
