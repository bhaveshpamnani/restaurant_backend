namespace restaurant_backend.Model;

public class DinningTableModel
{
    public int? TableID { get; set; }
    public string TableCode { get; set; }
    public bool AvailabilityStatus { get; set; }
    public int PersonCount { get; set; }
}