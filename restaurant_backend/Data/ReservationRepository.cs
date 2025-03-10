using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class ReservationRepository
{
    private readonly string _connectionString;

    public ReservationRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region Get all Reservation
    public IEnumerable<GetReservationModel> GetAllReservation()
    {
        var reservation = new List<GetReservationModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetReservations", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reservation.Add(
                    new GetReservationModel()
                    {
                        ReservationID = Convert.ToInt32(reader["ReservationID"]),
                        BookDate = reader["BookDate"].ToString(),
                        BookTime = reader["BookTime"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        PersonCount = Convert.ToInt32(reader["PersonCount"].ToString()),
                        UserID = Convert.ToInt32(reader["UserID"].ToString()),
                        ReservationStatus = reader["ReservationStatus"].ToString(),
                        TableCode = reader["TableCode"].ToString(),
                    }
                );
            }
        }
        return reservation;
    }

    #endregion
    
    #region Get reservation by id

    public IEnumerable<GetReservationModel> GetReservationByID(int ReservationID)
    {
        var reservation = new List<GetReservationModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetReservationById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@ReservationID", ReservationID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reservation.Add(
                    new GetReservationModel()
                    {
                        ReservationID = Convert.ToInt32(reader["ReservationID"]),
                        BookDate = reader["BookDate"].ToString(),
                        BookTime = reader["BookTime"].ToString(),
                        PersonCount = Convert.ToInt32(reader["PersonCount"].ToString()),
                        UserID = Convert.ToInt32(reader["UserID"].ToString()),
                        ReservationStatus = reader["ReservationStatus"].ToString(),
                        TableCode = reader["TableCode"].ToString()
                    }
                );
            }
        }

        return reservation;
    }

    #endregion
    
    #region Create Reservation
    public bool CreateReservation(ReservationModel reservation)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateReservation", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@UserID", reservation.UserID);
            cmd.Parameters.AddWithValue("@BookDate", reservation.BookDate);
            cmd.Parameters.AddWithValue("@BookTime", reservation.BookTime);
            cmd.Parameters.AddWithValue("@PersonCount", reservation.PersonCount);
            cmd.Parameters.AddWithValue("@ReservationStatus", reservation.ReservationStatus);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion
    
    #region UpdateReservation

    public bool UpdateReservation(ReservationUpdateModel reservation)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateReservation", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
        
            cmd.Parameters.AddWithValue("@ReservationID", reservation.ReservationId);
            cmd.Parameters.AddWithValue("@UserID", reservation.UserId);
            cmd.Parameters.AddWithValue("@TableID", reservation.TableId.HasValue ? reservation.TableId.Value : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ReservationStatus", reservation.ReservationStatus);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Database update successful.");
                return true;
            }
            else
            {
                Console.WriteLine("Database update failed.");
                return false;
            }
        }
    }


    #endregion

    #region DeleteReservation

    public bool DeleteReservation(int ReservationID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteReservation", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@ReservationID", ReservationID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
    
    #region Get Reservation by User ID
    public IEnumerable<GetReservationModel> GetReservationsByUserId(int userId)
    {
        var reservations = new List<GetReservationModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetReservationsByUserId", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reservations.Add(new GetReservationModel()
                {
                    ReservationID = Convert.ToInt32(reader["ReservationID"]),
                    BookDate = reader["BookDate"].ToString(),
                    BookTime = reader["BookTime"].ToString(),
                    PersonCount = Convert.ToInt32(reader["PersonCount"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                    ReservationStatus = reader["ReservationStatus"].ToString(),
                    TableCode = reader["TableCode"].ToString()
                });
            }
        }
        return reservations;
    }
    #endregion
    
    
    #region UpdateReservation

    public bool UpdateByUserReservation(ReservationModel reservation)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateByUserReservation", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
        
            cmd.Parameters.AddWithValue("@ReservationID", reservation.ReservationID);
            cmd.Parameters.AddWithValue("@UserID", reservation.UserID);
            cmd.Parameters.AddWithValue("@BookTime", reservation.BookTime);
            cmd.Parameters.AddWithValue("@BookDate", reservation.BookDate);
            cmd.Parameters.AddWithValue("@PersonCount", reservation.PersonCount);
            cmd.Parameters.AddWithValue("@ReservationStatus", reservation.ReservationStatus);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Database update successful.");
                return true;
            }
            else
            {
                Console.WriteLine("Database update failed.");
                return false;
            }
        }
    }


    #endregion
}