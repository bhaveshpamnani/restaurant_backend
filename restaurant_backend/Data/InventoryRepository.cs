using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class InventoryRepository
{
    private readonly string _connectionString;

    public InventoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all Inventory
    public IEnumerable<InventoryModel> GetAllInventory()
    {
        var Inventory = new List<InventoryModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetInventories", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Inventory.Add(
                    new InventoryModel()
                    {
                        InventoryID = Convert.ToInt32(reader["InventoryID"]),
                        ItemName = reader["ItemName"].ToString(),
                        QuantityAvailable = Convert.ToDecimal(reader["QuantityAvailable"]),
                        QuantityWanted = Convert.ToDecimal(reader["QuantityWanted"])
                    }
                );
            }
        }
        return Inventory;
    }

    #endregion
    
    #region Get Inventory by id

    public IEnumerable<InventoryModel> GetInventoryByID(int InventoryID)
    {
        var Inventory = new List<InventoryModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetInventoryById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Inventory.Add(
                    new InventoryModel()
                    {
                        InventoryID = Convert.ToInt32(reader["InventoryID"]),
                        ItemName = reader["ItemName"].ToString(),
                        QuantityAvailable = Convert.ToDecimal(reader["QuantityAvailable"]),
                        QuantityWanted = Convert.ToDecimal(reader["QuantityWanted"])
                    }
                );
            }
        }

        return Inventory;
    }

    #endregion
    
    #region Create Inventory
    public bool CreateInventory(InventoryModel Inventory)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateInventory", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@ItemName", Inventory.ItemName);
            cmd.Parameters.AddWithValue("@QuantityAvailable", Inventory.QuantityAvailable);
            cmd.Parameters.AddWithValue("@QuantityWanted", Inventory.QuantityWanted);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateInventory

    public bool UpdateInventory(InventoryModel Inventory)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateInventory",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@InventoryID", Inventory.InventoryID);
            cmd.Parameters.AddWithValue("@ItemName", Inventory.ItemName);
            cmd.Parameters.AddWithValue("@QuantityAvailable", Inventory.QuantityAvailable);
            cmd.Parameters.AddWithValue("@QuantityWanted", Inventory.QuantityWanted);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteInventory

    public bool DeleteInventory(int InventoryID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteInventory", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}