﻿using System;
using JetBrains.Annotations;
using SmartSoftware;
using SmartSoftware.Auditing;
using SmartSoftware.Domain.Entities.Auditing;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.CmsKit.Pages;

public class Page : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual string Title { get; protected set; }

    public virtual string Slug { get; protected set; }

    public virtual string Content { get; protected set; }

    public virtual string Script { get; protected set; }

    public virtual string Style { get; protected set; }

    public virtual bool IsHomePage { get; protected set; }

    public virtual int EntityVersion { get; protected set; }

    protected Page()
    {
    }

    internal Page(
        Guid id,
        [NotNull] string title,
        [NotNull] string slug,
        string content = null,
        string script = null,
        string style = null,
        Guid? tenantId = null) : base(id)
    {
        TenantId = tenantId;

        SetTitle(title);
        SetSlug(slug);
        SetContent(content);
        SetScript(script);
        SetStyle(style);
    }

    public virtual void SetTitle(string title)
    {
        Title = Check.NotNullOrEmpty(title, nameof(title), PageConsts.MaxTitleLength);
    }

    internal virtual void SetSlug(string slug)
    {
        Slug = SlugNormalizer.Normalize(Check.NotNullOrEmpty(slug, nameof(slug), PageConsts.MaxSlugLength));
    }

    public virtual void SetContent(string content)
    {
        Content = Check.Length(content, nameof(content), PageConsts.MaxContentLength);
    }

    public virtual void SetScript(string script)
    {
        Script = Check.Length(script, nameof(script), PageConsts.MaxScriptLength);
    }

    public virtual void SetStyle(string style)
    {
        Style = Check.Length(style, nameof(style), PageConsts.MaxStyleLength);
    }

    internal void SetIsHomePage(bool isHomePage)
    {
        IsHomePage = isHomePage;
    }
}
