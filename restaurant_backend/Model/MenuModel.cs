namespace restaurant_backend.Model;

public class MenuModel
{
    public int? MenuID { get; set; }
    public int CategoryID { get; set; }
    public string DishName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageURL { get; set; }
    public bool AvailabilityStatus { get; set; }
}

public class GetMenuModel
{
    public int MenuID { get; set; }
    public int CategoryID { get; set; }
    public string DishName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageURL { get; set; }
    public bool AvailabilityStatus { get; set; }
    public string CategoryName { get; set; }
}