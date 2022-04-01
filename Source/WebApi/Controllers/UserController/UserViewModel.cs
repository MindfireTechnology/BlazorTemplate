namespace WebApi.Controllers.UserController;

public record UserViewModel
{
	public int? UserId { get; set; }
	public string? UserName { get; set; }
	public string? Email { get; set; }
}
