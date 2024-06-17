﻿using Astrum.SharedLib.Domain.Entities;

namespace Astrum.Account.Aggregates;

public class SocialNetworks : AggregateRootBase<Guid>
{
    public SocialNetworks()
    {
    }

    public SocialNetworks(Guid networksId)
    {
        Id = networksId;
    }

    public string? Behance { get; set; }
    public string? Figma { get; set; }
    public string? GitHub { get; set; }
    public string? GitLab { get; set; }
    public string? Instagram { get; set; }
    public string? Telegram { get; set; }
    public string? VK { get; set; }
}