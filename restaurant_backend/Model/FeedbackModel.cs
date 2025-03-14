namespace restaurant_backend.Model;

public class GetFeedbackModel
{
    public int FeedbackID { get; set; }
    public string Description { get; set; }
    public int UserID { get; set; }
    public String UserName { get; set; }
    public String UserEmail { get; set; }
    public int FeedbackCategoryID { get; set; }
    public string CategoryName { get; set; }
}

public class FeedbackModel
{
    public int FeedbackID { get; set; }
    public string Description { get; set; }
    public int UserID { get; set; }
    public int FeedbackCategoryID { get; set; }
}