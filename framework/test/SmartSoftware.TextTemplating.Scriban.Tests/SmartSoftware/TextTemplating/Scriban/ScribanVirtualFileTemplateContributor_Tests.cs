using SmartSoftware.TextTemplating.VirtualFiles;

namespace SmartSoftware.TextTemplating.Scriban;

public class ScribanVirtualFileTemplateContributor_Tests : VirtualFileTemplateContributor_Tests<ScribanTextTemplatingTestModule>
{
    public ScribanVirtualFileTemplateContributor_Tests()
    {
        WelcomeEmailEnglishContent = "Welcome {{model.name}} to the smartsoftware.io!";
        WelcomeEmailTurkishContent = "Merhaba {{model.name}}, smartsoftware.io'ya hoşgeldiniz!";
        ForgotPasswordEmailEnglishContent = "{{L \"HelloText\" model.name}}, {{L \"HowAreYou\" }}. Please click to the following link to get an email to reset your password!";
    }
}
