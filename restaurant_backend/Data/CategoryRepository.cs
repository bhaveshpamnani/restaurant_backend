using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class CategoryRepository
{
    private readonly string _connectionString;

    public CategoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all Category
    public IEnumerable<CategoryModel> GetAllCategory()
    {
        var Category = new List<CategoryModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetCategories", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Category.Add(
                    new CategoryModel()
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString(),
                        ImagePath = reader["ImagePath"].ToString(),
                    }
                );
            }
        }
        return Category;
    }

    #endregion
    
    #region Get Category by id

    public IEnumerable<CategoryModel> GetCategoryByID(int CategoryID)
    {
        var Category = new List<CategoryModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetCategoryById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Category.Add(
                    new CategoryModel()
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        Description = reader["Description"].ToString(),
                    }
                );
            }
        }

        return Category;
    }

    #endregion
    
    #region Create Category
    public bool CreateCategory(CategoryModel Category)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateCategory", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CategoryName", Category.CategoryName);
            cmd.Parameters.AddWithValue("@Description", Category.Description);
            cmd.Parameters.AddWithValue("@ImagePath", Category.ImagePath ?? string.Empty); // Handle image path
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
    
    #region UpdateCategory

    public bool UpdateCategory(CategoryModel Category)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateCategory", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CategoryID", Category.CategoryID);
            cmd.Parameters.AddWithValue("@CategoryName", Category.CategoryName);
            cmd.Parameters.AddWithValue("@Description", Category.Description);
            cmd.Parameters.AddWithValue("@ImagePath", Category.ImagePath ?? string.Empty); // Handle image path

            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteCategory

    public bool DeleteCategory(int CategoryID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteCategory", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}