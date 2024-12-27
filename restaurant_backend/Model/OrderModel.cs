namespace restaurant_backend.Model;

public class OrderModel
{
    public int OrderID { get; set; }
    public int UserID { get; set; }
    public int TableID { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OrderStatus { get; set; }

}