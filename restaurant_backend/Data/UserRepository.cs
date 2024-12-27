using System.Data;
using Microsoft.Data.SqlClient;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region GetAll User
    public IEnumerable<UserModel> GetAllUser()
    {
        var users = new List<UserModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetUsers", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(
                    new UserModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        UserEmail = reader["UserEmail"].ToString(),
                        Password = reader["Password"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        RoleID = Convert.ToInt32(reader["RoleId"].ToString()),
                        RoleName = reader["RoleName"].ToString(),
                    }
                );
            }
        }

        return users;
    }
    #endregion

    #region GetUserProfile

    public IEnumerable<UserModel> GetUserProfile(int UserID)
    {
        var user = new List<UserModel>();
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("GetUserById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user.Add(
                    new UserModel()
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        UserEmail = reader["UserEmail"].ToString(),
                        Password = reader["Password"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        RoleID = Convert.ToInt32(reader["RoleId"].ToString()),
                        RoleName = reader["RoleName"].ToString(),
                    }
                );
            }
        }

        return user;
    }

    #endregion

    #region SignUpUser

    public bool SignUpUser(UserModel user)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("CreateUser", conn)
            {
                CommandType = CommandType.StoredProcedure,
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.UserEmail);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }
    #endregion

    #region UpdateUserProfile

    public bool UpdateUser(UserModel user)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("UpdateUser",conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@UserID", user.UserID);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@UserEmail", user.UserEmail);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@Phone", user.Phone);
            cmd.Parameters.AddWithValue("@RoleId", user.RoleID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion

    #region DeleteUser

    public bool DeleteUser(int UserID)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DeleteUser", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            conn.Open();
            cmd.Parameters.AddWithValue("@UserID", UserID);
            int rawEff = cmd.ExecuteNonQuery();
            return rawEff > 0;
        }
    }

    #endregion
}