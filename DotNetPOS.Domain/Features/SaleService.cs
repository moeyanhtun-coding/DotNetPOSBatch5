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
    public class SaleService
    {
        private readonly AppDbContext _db;

        public SaleService(AppDbContext db)
        {
            _db = db;
        }
        public async Task<string> CreateVoucher()
        {
            try
            {
                var newVoucher = new TblSale()
                {
                    VoucherNo = "VC-" + Ulid.NewUlid().ToString(),
                    SaleDate = DateTime.Now
                };
                await _db.AddAsync(newVoucher);
                await _db.SaveChangesAsync();
                return newVoucher.VoucherNo;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<Result<string>> UpdateVoucher(string voucherNo)
        {
            try
            {
                var existingVoucherNo = await _db.TblSales
                    .FirstOrDefaultAsync(x => x.VoucherNo == voucherNo);
                if (existingVoucherNo is null)
                {
                    return Result<string>.Failure("Voucher No Is Not Found");
                }
                var saleDetails = await _db.TblSaleDetails
                    .AsNoTracking()
                    .Where(x => x.VoucherNo == voucherNo)
                    .ToListAsync();

                if (!saleDetails.Any())
                {
                    return Result<string>.Failure("No sale details found for the specified voucher number.");
                }

                decimal totalAmount = saleDetails.Sum(detail => detail.Quantity * detail.Price);
                existingVoucherNo.TotalAmount = totalAmount;

                _db.Entry(existingVoucherNo).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return Result<string>.Success("Voucher Updated Successfully");
            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }

        public async Task<Result<VoucherRespModel>> GenerateVoucher(string voucherNo)
        {
            try
            {
                var voucherData = await _db.TblSales
                    .Where(x => x.VoucherNo == voucherNo)
                    .Select(s => new VoucherRespModel()
                    {
                        VoucherNo = voucherNo,
                        Products = s.TblSaleDetails.Select(sd => new VoucherItemDetails()
                        {
                            ProductCode = sd.ProductCode,
                            ProductName = sd.ProductCodeNavigation.Name,
                            Quantity = sd.Quantity,
                            UnitPrice = sd.Price

                        }).ToList(),
                        TotalPrice = s.TotalAmount
                    }).FirstOrDefaultAsync();

                if (voucherData == null)
                {
                    return Result<VoucherRespModel>.Failure("Voucher not found.");
                }

                return Result<VoucherRespModel>.Success(voucherData);

            }
            catch (Exception e)
            {

                throw new Exception(e.ToString());
            }
        }
    }
}
