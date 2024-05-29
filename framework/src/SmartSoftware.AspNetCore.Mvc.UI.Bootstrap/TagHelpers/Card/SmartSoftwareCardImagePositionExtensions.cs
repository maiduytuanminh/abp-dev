namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card;

public static class SmartSoftwareCardImagePositionExtensions
{
    public static string ToClassName(this SmartSoftwareCardImagePosition position)
    {
        switch (position)
        {
            case SmartSoftwareCardImagePosition.None:
                return "card-img";
            case SmartSoftwareCardImagePosition.Top:
                return "card-img-top";
            case SmartSoftwareCardImagePosition.Bottom:
                return "card-img-bottom";
            default:
                throw new SmartSoftwareException("Unknown SmartSoftwareCardImagePosition: " + position);
        }
    }
}
