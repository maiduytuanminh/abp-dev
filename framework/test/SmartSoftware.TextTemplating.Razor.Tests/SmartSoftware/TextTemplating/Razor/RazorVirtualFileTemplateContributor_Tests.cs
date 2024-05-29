﻿using System;
using SmartSoftware.TextTemplating.VirtualFiles;

namespace SmartSoftware.TextTemplating.Razor;

public class RazorVirtualFileTemplateContributor_Tests : VirtualFileTemplateContributor_Tests<RazorTextTemplatingTestModule>
{
    public RazorVirtualFileTemplateContributor_Tests()
    {
        WelcomeEmailEnglishContent = "@inherits SmartSoftware.TextTemplating.Razor.RazorTemplatePageBase<SmartSoftware.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>" +
                                     Environment.NewLine +
                                     "Welcome @Model.Name to the smartsoftware.io!";

        WelcomeEmailTurkishContent = "@inherits SmartSoftware.TextTemplating.Razor.RazorTemplatePageBase<SmartSoftware.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.WelcomeEmailModel>" +
                                     Environment.NewLine +
                                     "Merhaba @Model.Name, smartsoftware.io'ya hoşgeldiniz!";

        ForgotPasswordEmailEnglishContent = "@inherits SmartSoftware.TextTemplating.Razor.RazorTemplatePageBase<SmartSoftware.TextTemplating.Razor.RazorTemplateRendererProvider_Tests.ForgotPasswordEmailModel>" +
                                            Environment.NewLine +
                                            "@{" +
                                            Environment.NewLine +
                                            "    var url = @\"https://smartsoftware.io/Account/ResetPassword\";" +
                                            Environment.NewLine +
                                            "}" +
                                            Environment.NewLine +
                                            "@Localizer[\"HelloText\", Model.Name], @Localizer[\"HowAreYou\"]. Please click to the following link to get an email to reset your password!<a target=\"_blank\" href=\"@url\">Reset your password</a>";
    }
}
