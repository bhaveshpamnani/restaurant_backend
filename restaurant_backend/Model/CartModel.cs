namespace restaurant_backend.Model;

public class CartModel
{
    public int  CartID { get; set; }
    public int  MenuID { get; set; }
    public int  UserID { get; set; }
    public int?  TableID { get; set; }
    public int  Quantity { get; set; }
}

public class GetCartItemModel
{
    public int?  CartID { get; set; }
    public int  MenuID { get; set; }
    public int  UserID { get; set; }
    public int  TableID { get; set; }
    public int  Quantity { get; set; }
    public String  DishName { get; set; }
    public decimal  Price { get; set; }
    public String  ImgURL { get; set; }
    public String  TableCode { get; set; }
    public String  CategoryName { get; set; }
    public String  Description { get; set; }
    public int  Rating { get; set; }
}