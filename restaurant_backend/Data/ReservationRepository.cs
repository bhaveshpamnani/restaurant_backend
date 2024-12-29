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
                        PersonCount = Convert.ToInt32(reader["PersonCount"].ToString()),
                        UserID = Convert.ToInt32(reader["UserID"].ToString()),
                        ReservationStatus = reader["ReservationStatus"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        UserEmail = reader["UserEmail"].ToString(),
                        TableID = Convert.ToInt32(reader["TableID"].ToString()),
                        TableCode = reader["TableCode"].ToString(),
                        Avaliablity_Status = reader["AvailabilityStatus"].ToString()
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
                        UserName = reader["UserName"].ToString(),
                        UserEmail = reader["UserEmail"].ToString(),
                        TableID = Convert.ToInt32(reader["TableID"].ToString()),
                        TableCode = reader["TableCode"].ToString(),
                        Avaliablity_Status = reader["AvailabilityStatus"].ToString()
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
            cmd.Parameters.AddWithValue("@TableID", reservation.TableID);
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

    public bool UpdateReservation(ReservationModel reservation)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateReservation",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@ReservationID", reservation.ReservationID);
            cmd.Parameters.AddWithValue("@UserID", reservation.UserID);
            cmd.Parameters.AddWithValue("@TableID", reservation.TableID);
            cmd.Parameters.AddWithValue("@BookDate", reservation.BookDate);
            cmd.Parameters.AddWithValue("@BookTime", reservation.BookTime);
            cmd.Parameters.AddWithValue("@PersonCount", reservation.PersonCount);
            cmd.Parameters.AddWithValue("@ReservationStatus", reservation.ReservationStatus);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
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
}