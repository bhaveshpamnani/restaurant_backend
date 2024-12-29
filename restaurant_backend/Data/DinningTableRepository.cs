using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class DinningTableRepository
{
    private readonly string _connectionString;

    public DinningTableRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all DinningTable
    public IEnumerable<DinningTableModel> GetAllDinningTable()
    {
        var DinningTable = new List<DinningTableModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetDiningTables", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DinningTable.Add(
                    new DinningTableModel()
                    {
                        PersonCount = Convert.ToInt32(reader["PersonCount"]),
                        AvailabilityStatus = Convert.ToBoolean(reader["AvailabilityStatus"].ToString()),
                        TableCode = reader["TableCode"].ToString(),
                    }
                );
            }
        }
        return DinningTable;
    }

    #endregion
    
    #region Get DinningTable by id

    public IEnumerable<DinningTableModel> GetDinningTableByID(int TableID)
    {
        var DinningTable = new List<DinningTableModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetDinningTableById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@TableID", TableID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                DinningTable.Add(
                    new DinningTableModel()
                    {
                        PersonCount = Convert.ToInt32(reader["PersonCount"]),
                        AvailabilityStatus = Convert.ToBoolean(reader["AvailabilityStatus"].ToString()),
                        TableCode = reader["TableCode"].ToString(),
                    }
                );
            }
        }

        return DinningTable;
    }

    #endregion
    
    #region Create DinningTable
    public bool CreateDinningTable(DinningTableModel DinningTable)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateDinningTable", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@TableCode", DinningTable.TableCode);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", DinningTable.AvailabilityStatus);
            cmd.Parameters.AddWithValue("@PersonCount", DinningTable.PersonCount);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateDinningTable

    public bool UpdateDinningTable(DinningTableModel DinningTable)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateDinningTable",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@TableID", DinningTable.TableID);
            cmd.Parameters.AddWithValue("@TableCode", DinningTable.TableCode);
            cmd.Parameters.AddWithValue("@AvailabilityStatus", DinningTable.AvailabilityStatus);
            cmd.Parameters.AddWithValue("@PersonCount", DinningTable.PersonCount);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteDinningTable

    public bool DeleteDinningTable(int TableID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteDinningTable", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@TableID", TableID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}