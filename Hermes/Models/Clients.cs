namespace Hermes.Models
{
  public class Clients
  {
    public int id { get; set; }
	public string createdAt { get; set; }
    public string email { get; set; }
    public string encryptedPassword { get; set; }
    public string name { get; set; }
    public string lastname { get; set; }
    public string phone { get; set; }    
    public string updatedAt { get; set; }
  }
}