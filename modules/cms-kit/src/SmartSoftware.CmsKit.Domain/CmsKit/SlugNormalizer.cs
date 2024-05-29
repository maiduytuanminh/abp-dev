﻿using Slugify;
using Unidecode.NET;

namespace SmartSoftware.CmsKit;

public static class SlugNormalizer
{
    static readonly SlugHelper SlugHelper = new(new SlugHelperConfiguration
    {
        AllowedChars =
        {
            '/'
        }
    });

    public static string Normalize(string value)
    {
        return SlugHelper.GenerateSlug(value?.Unidecode()).Trim('/');
    }
}
