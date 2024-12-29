using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class CustomerOrderRepository
{
    private readonly string _connectionString;

    public CustomerOrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all CustomerOrder
    public IEnumerable<CustomerOrderModel> GetAllCustomerOrder()
    {
        var CustomerOrder = new List<CustomerOrderModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetCustomerOrders", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CustomerOrder.Add(
                    new CustomerOrderModel()
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        MenuID = Convert.ToInt32(reader["MenuID"].ToString()),
                        UserID = Convert.ToInt32(reader["UserID"].ToString()),
                        Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString()),
                        CreatedAt = reader["CreatedAt"].ToString(),
                        TableID = Convert.ToInt32(reader["TableID"].ToString()),
                        PaymentStatus = reader["PaymentStatus"].ToString(),
                        OrderStatus = reader["OrderStatus"].ToString()
                    }
                );
            }
        }
        return CustomerOrder;
    }

    #endregion
    
    #region Get CustomerOrder by id

    public IEnumerable<CustomerOrderModel> GetCustomerOrderByID(int OrderID)
    {
        var CustomerOrder = new List<CustomerOrderModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetCustomerOrderById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                CustomerOrder.Add(
                    new CustomerOrderModel()
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        MenuID = Convert.ToInt32(reader["MenuID"].ToString()),
                        UserID = Convert.ToInt32(reader["UserID"].ToString()),
                        Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString()),
                        CreatedAt = reader["CreatedAt"].ToString(),
                        TableID = Convert.ToInt32(reader["TableID"].ToString()),
                        PaymentStatus = reader["PaymentStatus"].ToString(),
                        OrderStatus = reader["OrderStatus"].ToString()
                    }
                );
            }
        }

        return CustomerOrder;
    }

    #endregion
    
    #region Create CustomerOrder
    public bool CreateCustomerOrder(CustomerOrderModel CustomerOrder)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateCustomerOrder", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@OrderID", CustomerOrder.OrderID);
            cmd.Parameters.AddWithValue("@UserID", CustomerOrder.UserID);
            cmd.Parameters.AddWithValue("@TableID", CustomerOrder.TableID);
            cmd.Parameters.AddWithValue("@MenuID", CustomerOrder.MenuID);
            cmd.Parameters.AddWithValue("@Quantity", CustomerOrder.Quantity);
            cmd.Parameters.AddWithValue("@TotalAmount", CustomerOrder.TotalAmount);
            cmd.Parameters.AddWithValue("@PaymentStatus", CustomerOrder.PaymentStatus);
            cmd.Parameters.AddWithValue("@OrderStatus", CustomerOrder.OrderStatus);
            cmd.Parameters.AddWithValue("@CreatedAt", CustomerOrder.CreatedAt);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateCustomerOrder

    public bool UpdateCustomerOrder(CustomerOrderModel CustomerOrder)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateCustomerOrder",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@OrderID", CustomerOrder.OrderID);
            cmd.Parameters.AddWithValue("@UserID", CustomerOrder.UserID);
            cmd.Parameters.AddWithValue("@TableID", CustomerOrder.TableID);
            cmd.Parameters.AddWithValue("@MenuID", CustomerOrder.MenuID);
            cmd.Parameters.AddWithValue("@Quantity", CustomerOrder.Quantity);
            cmd.Parameters.AddWithValue("@TotalAmount", CustomerOrder.TotalAmount);
            cmd.Parameters.AddWithValue("@PaymentStatus", CustomerOrder.PaymentStatus);
            cmd.Parameters.AddWithValue("@OrderStatus", CustomerOrder.OrderStatus);
            cmd.Parameters.AddWithValue("@CreatedAt", CustomerOrder.CreatedAt);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteCustomerOrder

    public bool DeleteCustomerOrder(int OrderID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteCustomerOrder", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}