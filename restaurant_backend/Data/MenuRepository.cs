using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class MenuRepository
{
    private readonly string _connectionString;

    public MenuRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all Menu
    public IEnumerable<GetMenuModel> GetAllMenu()
    {
        var menu = new List<GetMenuModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetMenus", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                menu.Add(
                    new GetMenuModel()
                    {
                        MenuID = Convert.ToInt32(reader["MenuID"]),
                        DishName = reader["DishName"].ToString(),
                        Description = reader["Description"].ToString(),
                        CategoryID = Convert.ToInt32(reader["CategoryID"].ToString()),
                        Rating = Convert.ToInt32(reader["Rating"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        ImageURL = reader["ImgURL"].ToString(),
                        AvailabilityStatus = Convert.ToBoolean(reader["AvailabilityStatus"])
                    }
                );
            }
        }
        return menu;
    }

    #endregion
    
    #region Get menu by id

    public IEnumerable<GetMenuModel> GetMenuByID(int MenuID)
    {
        var menu = new List<GetMenuModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetMenuById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@MenuID", MenuID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                menu.Add(
                    new GetMenuModel()
                    {
                        MenuID = Convert.ToInt32(reader["MenuID"]),
                        DishName = reader["DishName"].ToString(),
                        Description = reader["Description"].ToString(),
                        CategoryID = Convert.ToInt32(reader["CategoryID"].ToString()),
                        Rating = Convert.ToInt32(reader["Rating"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        ImageURL = reader["ImgURL"].ToString(),
                        AvailabilityStatus = Convert.ToBoolean(reader["AvailabilityStatus"])
                    }
                );
            }
        }
        return menu;
    }

    #endregion
    
    #region Get Category wise menu items

    public IEnumerable<GetMenuModel> GetMenuByCategoryID(int CategoryID)
    {
        var menu = new List<GetMenuModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetMenuByCategoryID", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                menu.Add(
                    new GetMenuModel()
                    {
                        MenuID = Convert.ToInt32(reader["MenuID"]),
                        DishName = reader["DishName"].ToString(),
                        Description = reader["Description"].ToString(),
                        CategoryID = Convert.ToInt32(reader["CategoryID"].ToString()),
                        Rating = Convert.ToInt32(reader["Rating"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        ImageURL = reader["ImgURL"].ToString(),
                        AvailabilityStatus = Convert.ToBoolean(reader["AvailabilityStatus"])
                    }
                );
            }
        }

        return menu;
    }

    #endregion
    
    #region Create Menu
    public bool CreateMenu(MenuModel menu)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateMenu", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CategoryID", menu.CategoryID);
            cmd.Parameters.AddWithValue("@DishName", menu.DishName);
            cmd.Parameters.AddWithValue("@Description", menu.Description);
            cmd.Parameters.AddWithValue("@Price", menu.Price);
            cmd.Parameters.AddWithValue("@ImgURL", menu.ImageURL);
            cmd.Parameters.AddWithValue("@Rating", menu.Rating);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", menu.AvailabilityStatus);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateMenu

    public bool UpdateMenu(MenuModel menu)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateMenu",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@MenuID", menu.MenuID);
            cmd.Parameters.AddWithValue("@CategoryID", menu.CategoryID);
            cmd.Parameters.AddWithValue("@DishName", menu.DishName);
            cmd.Parameters.AddWithValue("@Description", menu.Description);
            cmd.Parameters.AddWithValue("@Price", menu.Price);
            cmd.Parameters.AddWithValue("@ImgURL", menu.ImageURL);
            cmd.Parameters.AddWithValue("@Rating", menu.Rating);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", menu.AvailabilityStatus);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteMenu

    public bool DeleteMenu(int MenuID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteMenu", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@MenuID", MenuID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}