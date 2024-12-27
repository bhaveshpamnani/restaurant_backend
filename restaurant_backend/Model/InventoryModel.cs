namespace restaurant_backend.Model;

public class InventoryModel
{
    public int InventoryID { get; set; }
    public string ItemName { get; set; }
    public decimal QuantityAvailable { get; set; }
    public decimal QuantityWanted { get; set; }
}