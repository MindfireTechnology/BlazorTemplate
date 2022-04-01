namespace Web;
public record ClientSettings
{
	public string? ApiUrl { get; set; }
	public int Timeout { get; set; }
}
