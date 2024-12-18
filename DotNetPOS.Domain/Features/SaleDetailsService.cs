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
    public class SaleDetailsService
    {
        private readonly AppDbContext _db;
        private readonly SaleService _saleService;

        public SaleDetailsService(AppDbContext db, SaleService saleService)
        {
            _db = db;
            _saleService = saleService;
        }

        public async Task<Result<string>> CreateSaleDetails(SalesDetailsRequestModel model)
        {
            try
            {
                var voucherNo = await _saleService.CreateVoucher();
                
                var productPrices = await _db.TblProducts
                    .AsNoTracking()
                    .Where(p => model.Products.Select(mp => mp.ProductCode).Contains(p.ProductCode))
                    .ToDictionaryAsync(p => p.ProductCode, p => p.Price);

                if (productPrices.Count != model.Products.Count)
                {
                    return Result<string>.Failure("Some products in the sale could not be found in the inventory. Please check the product codes.");
                }

                var saleDetails = model.Products.Select(product => new TblSaleDetail()
                {
                    VoucherNo = voucherNo,
                    ProductCode = product.ProductCode,
                    Quantity = product.Quantity,
                    Price = productPrices[product.ProductCode]
                }).ToList();
                
                await _db.AddRangeAsync(saleDetails);
                await _db.SaveChangesAsync();
                await _saleService.UpdateVoucher(voucherNo);

                return Result<string>.Success($"Sale Details Created Successfully with voucher no {voucherNo}");
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }


    }
}
