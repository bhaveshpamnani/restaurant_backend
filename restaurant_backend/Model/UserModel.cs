namespace restaurant_backend.Model;

public class UserModel
{
    public int? UserID { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public int RoleID { get; set; }
}

public class GetUserModel
{
    public int? UserID { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public string Password { get; set; }
    public string Phone { get; set; }
    public int RoleID { get; set; }
    public String RoleName { get; set; }
}