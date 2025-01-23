using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using restaurant_backend.Model;

namespace restaurant_backend.Data;

public class UserRepository
{
    private readonly string _connectionString;
    private readonly IConfiguration _configuration;

    public UserRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    #region GetAll User
    public IEnumerable<GetUserModel> GetAllUser()
    {
        var users = new List<GetUserModel>();
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
                    new GetUserModel()
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

    public IEnumerable<GetUserModel> GetUserProfile(int UserID)
    {
        var user = new List<GetUserModel>();
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
                    new GetUserModel()
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
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("CreateUser", conn)
                {
                    CommandType = CommandType.StoredProcedure,
                };
                conn.Open();
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                cmd.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(user.Password));
                cmd.Parameters.AddWithValue("@Phone", user.Phone);
                cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
                int rawEff = cmd.ExecuteNonQuery();
                return rawEff > 0;
            }
        }
        catch (Exception ex)
        {
            // Log error details
            Console.WriteLine($"Error: {ex.Message}");
            return false;
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
    
    #region Login
    public class LoginResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
    public LoginResponse Login(string email, string password)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("ValidateUser", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            cmd.Parameters.AddWithValue("@UserEmail", email);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                string storedHashedPassword = reader["Password"].ToString();
                if (BCrypt.Net.BCrypt.Verify(password, storedHashedPassword))
                {
                    var userId = Convert.ToInt32(reader["UserID"]);
                    var roleId = Convert.ToInt32(reader["RoleId"]);
                    var token = GenerateJwtToken(userId, roleId, _configuration);

                    return new LoginResponse
                    {
                        Token = token,
                        UserId = userId
                    };
                }
            }
            return null; // Invalid credentials
        }
    }
    #endregion

    #region MyRegion
    public string GenerateJwtToken(int userId, int roleId, IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, roleId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Read expiration in days from configuration
        var expiresInDays = Convert.ToInt32(configuration["Jwt:ExpiresInDays"]);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(expiresInDays),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}