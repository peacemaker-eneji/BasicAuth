namespace BasicAuth.Web.Dtos {
    public record UserInfoDto(
        int Id,
        string Firstname,
        string Lastname,
        string Email
    );
}
