namespace FuckWeb.Data.Entities;

public class FingerCheck : BaseEntity
{
    public User User { get; set; }
    
    public long UserId { get; set; }
    
    public DateTime Date { get; set; } = DateTime.UtcNow;
}