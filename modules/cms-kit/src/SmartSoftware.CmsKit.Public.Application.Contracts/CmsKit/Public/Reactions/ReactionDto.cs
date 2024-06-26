﻿using System;
using JetBrains.Annotations;

namespace SmartSoftware.CmsKit.Public.Reactions;

[Serializable]
public class ReactionDto
{
    [NotNull]
    public string Name { get; set; }

    [CanBeNull]
    public string DisplayName { get; set; }
}
