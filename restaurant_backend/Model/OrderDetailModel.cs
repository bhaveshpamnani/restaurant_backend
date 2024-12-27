namespace restaurant_backend.Model;

public class OrderDetailModel
{
    public int OrderDetailID { get; set; }
    public int OrderID { get; set; }
    public int MenuID { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}