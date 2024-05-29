using System;
using System.ComponentModel.DataAnnotations;
using SmartSoftware.Validation;
using SmartSoftware.CmsKit.Ratings;

namespace SmartSoftware.CmsKit.Public.Ratings;

[Serializable]
public class CreateUpdateRatingInput
{
    [Required]
    [DynamicRange(typeof(RatingConsts), typeof(int), nameof(RatingConsts.MinStarCount), nameof(RatingConsts.MaxStarCount))]
    public short StarCount { get; set; }
}
