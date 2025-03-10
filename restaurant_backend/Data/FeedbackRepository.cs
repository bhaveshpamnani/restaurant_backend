using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class FeedbackRepository
{
    private readonly string _connectionString;

    public FeedbackRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all Feedback
    public IEnumerable<GetFeedbackModel> GetAllFeedback()
    {
        var feedbacks = new List<GetFeedbackModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetFeedbacks", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                feedbacks.Add(
                    new GetFeedbackModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Description = reader["Description"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserEmail = reader["UserEmail"].ToString(),
                        FeedbackID = Convert.ToInt32(reader["FeedbackID"].ToString()),
                        FeedbackCategoryID = Convert.ToInt32(reader["FeedbackCategoryID"].ToString())
                    }
                );
            }
        }

        return feedbacks;
    }

    #endregion
    
    #region Get feedback by id

    public IEnumerable<GetFeedbackModel> GetFeedbackByID(int FeedbackID)
    {
        var feedback = new List<GetFeedbackModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetFeedbackById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@FeedbackID", FeedbackID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                feedback.Add(
                    new GetFeedbackModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Description = reader["Description"].ToString(),
                        CategoryName = reader["CategoryName"].ToString(),
                        FeedbackID = Convert.ToInt32(reader["FeedbackID"].ToString()),
                        FeedbackCategoryID = Convert.ToInt32(reader["FeedbackCategoryID"].ToString())
                    }
                );
            }
        }

        return feedback;
    }

    #endregion
    
    #region Create Feedback
    public bool CreateFeedback(FeedbackModel feedback)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateFeedback", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@Description", feedback.Description);
            cmd.Parameters.AddWithValue("@UserID", feedback.UserID);
            cmd.Parameters.AddWithValue("@FeedbackCategoryID", feedback.FeedbackCategoryID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateFeedback

    public bool UpdateFeedback(FeedbackModel feedback)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateFeedback",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@FeedbackID", feedback.FeedbackID);
            cmd.Parameters.AddWithValue("@Description", feedback.Description);
            cmd.Parameters.AddWithValue("@UserID", feedback.UserID);
            cmd.Parameters.AddWithValue("@FeedbackCategoryID", feedback.FeedbackCategoryID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteFeedback

    public bool DeleteFeedback(int FeedbackID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteFeedback", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@FeedbackID", FeedbackID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}