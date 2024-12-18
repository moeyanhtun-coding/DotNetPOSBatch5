using DotNetPOS.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using DotNetPOS.Database.RequestModel;

namespace DotNetPOS.Domain.Features;

public class ProductService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<TblProduct> GetProducts()
    {
        var lst = _db.TblProducts.Where(x => x.DeleteFlag == false).ToList();
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

    public int ProductCreate(ProductRequestModel reqModel)
    {
        TblProduct product = new TblProduct()
        {
            ProductCode = Ulid.NewUlid().ToString(),
            Name = reqModel.ProductName,
            Price = reqModel.Price,
            ProductCategoryCode = reqModel.ProductCategoryCode,
            DeleteFlag = false
        };

        _db.TblProducts.Add(product);
        var result = _db.SaveChanges();
        return result;
    }

    public int ProductUpdate(string productCode, ProductRequestModel reqModel)
    {
        var item = _db.TblProducts.AsNoTracking().FirstOrDefault(x => x.ProductCode == productCode);
        item.Name = reqModel.ProductName;
        item.Price = reqModel.Price;
        item.ProductCategoryCode = reqModel.ProductCategoryCode;
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