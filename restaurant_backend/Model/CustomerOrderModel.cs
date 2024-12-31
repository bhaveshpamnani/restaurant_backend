namespace restaurant_backend.Model;

public class CustomerOrderModel
{
    public int?  OrderID { get; set; }
    public int  TableID { get; set; }
    public int  UserID { get; set; }
    public int  MenuID { get; set; }
    public int  Quantity { get; set; }
    public decimal  TotalAmount { get; set; }
    public string  PaymentStatus { get; set; }
    public string  OrderStatus { get; set; }
    public string?  CreatedAt { get; set; }
}