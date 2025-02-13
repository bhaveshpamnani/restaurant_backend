using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class CartRepository
{
    private readonly string _connectionString;

    public CartRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    #region Add Item in Cart
    public bool AddCart(CartModel cart)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("AddMenuItemToCart", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@MenuID", cart.MenuID);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@UserID", cart.UserID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateCart

    public bool UpdateCart(CartModel cart)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateCartQuantity",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@CartID", cart.CartID);
            cmd.Parameters.AddWithValue("@MenuID", cart.MenuID);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@UserID", cart.UserID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
    
    #region Get Cart Item by UserID

    public IEnumerable<GetCartItemModel> GetCartItemByUserID(int UserID)
    {
        var cart = new List<GetCartItemModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetUserCartDetails", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = cmd.ExecuteReader();
        
            while (reader.Read()) // Change 'if' to 'while'
            {
                cart.Add(
                    new GetCartItemModel()
                    {
                        CartID = Convert.ToInt32(reader["CartID"]),
                        MenuID = Convert.ToInt32(reader["MenuID"]),
                        TableID = reader["TableID"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TableID"]),
                        DishName = reader["DishName"].ToString(),
                        Description = reader["Description"].ToString(),
                        Rating = Convert.ToInt32(reader["Rating"].ToString()),
                        Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                        CategoryName = reader["CategoryName"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        ImgURL = reader["ImgURL"].ToString(),
                        TableCode = reader["TableCode"].ToString(),
                    }
                );
            }
        }
        return cart;
    }


    #endregion
}