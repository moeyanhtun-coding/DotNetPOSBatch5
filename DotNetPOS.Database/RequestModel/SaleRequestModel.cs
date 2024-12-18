namespace DotNetPOS.Database.RequestModel;

public class SaleRequestModel
{
    public string ProductCode { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}