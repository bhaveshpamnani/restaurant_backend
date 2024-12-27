using System.Runtime.InteropServices.JavaScript;

namespace restaurant_backend.Model;

public class ReservationModel
{
    public int ReservationID { get; set; }
    public int UserID { get; set; }
    public int TableID { get; set; }
    public string BookDate { get; set; }
    public string BookTime { get; set; }
    public int PersonCount { get; set; }
    public string ReservationStatus { get; set; }

}