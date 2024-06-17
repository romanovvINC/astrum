using System.Text.Json.Serialization;

namespace FuckWeb.Data.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    
    public string FingerId { get; set; }
    
    public string Function { get; set; }

    [JsonIgnore] public ICollection<FingerCheck> Checks { get; set; } = new List<FingerCheck>();

}