using System;

namespace SmartSoftware.Gdpr;

[Serializable]
public class GdprUserDataDeletionRequestedEto
{
    public Guid UserId { get; set; }
}