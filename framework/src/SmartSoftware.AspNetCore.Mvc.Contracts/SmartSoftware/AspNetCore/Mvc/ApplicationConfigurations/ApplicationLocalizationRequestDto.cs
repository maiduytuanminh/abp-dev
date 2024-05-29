using System.ComponentModel.DataAnnotations;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public class ApplicationLocalizationRequestDto
{
    [Required]
    public string CultureName { get; set; } = default!;
    
    public bool OnlyDynamics { get; set; }
}