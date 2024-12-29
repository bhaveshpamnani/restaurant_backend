using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class FeedbackCategoryRepository
{
    private readonly string _connectionString;

    public FeedbackCategoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all FeedbackCategory
    public IEnumerable<FeedbackCategoryModel> GetAllFeedbackCategory()
    {
        var FeedbackCategory = new List<FeedbackCategoryModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetFeedbackCategories", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FeedbackCategory.Add(
                    new FeedbackCategoryModel()
                    {
                        FeedbackCategoryID = Convert.ToInt32(reader["FeedbackCategoryID"]),
                        CategoryName = reader["CategoryName"].ToString()
                    }
                );
            }
        }
        return FeedbackCategory;
    }

    #endregion
    
    #region Get FeedbackCategory by id

    public IEnumerable<FeedbackCategoryModel> GetFeedbackCategoryByID(int FeedbackCategoryID)
    {
        var FeedbackCategory = new List<FeedbackCategoryModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetFeedbackCategoryById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@FeedbackCategoryID", FeedbackCategoryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                FeedbackCategory.Add(
                    new FeedbackCategoryModel()
                    {
                        FeedbackCategoryID = Convert.ToInt32(reader["FeedbackCategoryID"]),
                        CategoryName = reader["CategoryName"].ToString()
                    }
                );
            }
        }

        return FeedbackCategory;
    }

    #endregion
    
    #region Create FeedbackCategory
    public bool CreateFeedbackCategory(FeedbackCategoryModel FeedbackCategory)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateFeedbackCategory", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CategoryName", FeedbackCategory.CategoryName);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateFeedbackCategory

    public bool UpdateFeedbackCategory(FeedbackCategoryModel FeedbackCategory)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateFeedbackCategory",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@FeedbackCategoryID", FeedbackCategory.FeedbackCategoryID);
            cmd.Parameters.AddWithValue("@CategoryName", FeedbackCategory.CategoryName);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteFeedbackCategory

    public bool DeleteFeedbackCategory(int FeedbackCategoryID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteFeedbackCategory", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@FeedbackCategoryID", FeedbackCategoryID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}