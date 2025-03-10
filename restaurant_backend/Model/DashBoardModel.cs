namespace restaurant_backend.Model;

public class DashboardCounts
{
    public string Metric { get; set; }
    public int Value { get; set; }
}

public class Inventory
{
    public int InventoryID { get; set; }
    public string ItemName { get; set; }
    public double QuantityAvailable { get; set; }
    public double QuantityWanted { get; set; }
}

public class DashBoardModel
{
    public List<DashboardCounts> Counts { get; set; }
    public List<Inventory> RecentOrders { get; set; }
}