using System;
using System.Collections.Generic;
using AutoMapper;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Public.Pages;

namespace SmartSoftware.CmsKit.Public.Web.Pages.Public.CmsKit.Pages;

[AutoMap(typeof(PageDto), ReverseMap = true)]
public class PageViewModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public List<ContentFragment> ContentFragments { get; set; }

    public string Script { get; set; }

    public string Style { get; set; }
}