using FuckWeb.Data.Entities;

namespace FuckWeb.Features.WorkingSession;

public class WorkingSession
{
    public User User { get; set; }
    
    public DateOnly Date { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    
    public List<FingerCheck> Checks { get; set; }

    public TimeSpan Duration => Status switch
    {
        PresenceStatus.DidntCome => TimeSpan.Zero,
        PresenceStatus.InTheOffice => DateTime.UtcNow - Start.Value,
        PresenceStatus.Gone => End.Value - Start.Value
    };
    
    public PresenceStatus Status
    {
        get 
        {
            if (Start is null && End is null)
                return PresenceStatus.DidntCome;
            if (Start is not null && End is null)
                return PresenceStatus.InTheOffice;
            if (Start is not null && End is not null)
                return PresenceStatus.Gone;
            return PresenceStatus.DidntCome;
        }
    }

    public WorkingSession(List<FingerCheck> checks , User user, DateOnly date)
    {
        this.User = user;
        this.Date = date;
        this.Checks = checks;
        
        if (checks.Count == 0)
        {
            Start = null;
            End = null;
        }
        else if(checks.Count % 2 != 0)
        {
            Start = checks.First().Date;
            End = null;
        }
        else
        {
            Start = checks.Min(x => x.Date);
            End = checks.Max(x => x.Date);
        }
    }
}