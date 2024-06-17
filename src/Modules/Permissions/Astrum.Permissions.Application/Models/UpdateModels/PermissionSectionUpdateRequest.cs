namespace Astrum.Permissions.Application.Models.UpdateModels
{
    public class PermissionSectionUpdateRequest
    {
        public string TitleSection { get; set; }
        public bool Permission { get; set; }
        public DateTimeOffset? DateCreated { get; set; }
        public DateTimeOffset? DateModifed { get; set; }
    }
}
