namespace BasicAuth.Data.Models;

public class User {
    public int Id {get; set;}
    public required string Firstname {get; set;}
    public required string Lastname {get; set;}
    public string PasswordHash {get; set;} = "";
    public required string Email {get; set;}
}
