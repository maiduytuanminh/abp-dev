using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Linq;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Identity;
using SmartSoftware.IdentityServer.AspNetIdentity;
using SmartSoftware.Security.Claims;
using IdentityUser = SmartSoftware.Identity.IdentityUser;

namespace SmartSoftware.IdentityServer;

public static class SmartSoftwareIdentityServerBuilderExtensions
{
    public static IIdentityServerBuilder AddSmartSoftwareIdentityServer(
        this IIdentityServerBuilder builder,
        SmartSoftwareIdentityServerBuilderOptions options = null)
    {
        if (options == null)
        {
            options = new SmartSoftwareIdentityServerBuilderOptions();
        }

        //TODO: AspNet Identity integration lines. Can be extracted to a extension method
        if (options.IntegrateToAspNetIdentity)
        {
            builder.AddAspNetIdentity<IdentityUser>();
            builder.AddProfileService<SmartSoftwareProfileService>();
            builder.AddResourceOwnerValidator<SmartSoftwareResourceOwnerPasswordValidator>();

            builder.Services.Remove(builder.Services.LastOrDefault(x => x.ServiceType == typeof(IUserClaimsPrincipalFactory<IdentityUser>)));
            builder.Services.AddTransient<IUserClaimsPrincipalFactory<IdentityUser>, SmartSoftwareUserClaimsFactory<IdentityUser>>();
            builder.Services.AddTransient<IObjectAccessor<IUserClaimsPrincipalFactory<IdentityUser>>, ObjectAccessor<SmartSoftwareUserClaimsPrincipalFactory>>();
        }

        builder.Services.Replace(ServiceDescriptor.Transient<IClaimsService, SmartSoftwareClaimsService>());

        if (options.UpdateSmartSoftwareClaimTypes)
        {
            SmartSoftwareClaimTypes.UserId = JwtClaimTypes.Subject;
            SmartSoftwareClaimTypes.UserName = JwtClaimTypes.Name;
            SmartSoftwareClaimTypes.Role = JwtClaimTypes.Role;
            SmartSoftwareClaimTypes.Email = JwtClaimTypes.Email;
        }

        if (options.UpdateJwtSecurityTokenHandlerDefaultInboundClaimTypeMap)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[SmartSoftwareClaimTypes.UserId] = SmartSoftwareClaimTypes.UserId;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[SmartSoftwareClaimTypes.UserName] = SmartSoftwareClaimTypes.UserName;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[SmartSoftwareClaimTypes.Role] = SmartSoftwareClaimTypes.Role;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[SmartSoftwareClaimTypes.Email] = SmartSoftwareClaimTypes.Email;
        }

        return builder;
    }

    //TODO: Use the latest Identity server code to optimize performance.
    // https://github.com/IdentityServer/IdentityServer4/blob/main/src/IdentityServer4/src/Configuration/DependencyInjection/BuilderExtensions/Crypto.cs
    private static IIdentityServerBuilder AddSmartSoftwareDeveloperSigningCredential(
        this IIdentityServerBuilder builder,
        bool persistKey = true,
        string filename = null,
        IdentityServerConstants.RsaSigningAlgorithm signingAlgorithm = IdentityServerConstants.RsaSigningAlgorithm.RS256)
    {
        if (filename == null)
        {
            filename = Path.Combine(Directory.GetCurrentDirectory(), "tempkey.rsa");
        }

        if (File.Exists(filename))
        {
            var keyFile = File.ReadAllText(filename);

            var json = JObject.Parse(keyFile);
            var keyId = json.GetValue("KeyId").Value<string>();
            var jsonParameters = json.GetValue("Parameters");
            RSAParameters rsaParameters;
            rsaParameters.D = Convert.FromBase64String(jsonParameters["D"].Value<string>());
            rsaParameters.DP = Convert.FromBase64String(jsonParameters["DP"].Value<string>());
            rsaParameters.DQ = Convert.FromBase64String(jsonParameters["DQ"].Value<string>());
            rsaParameters.Exponent = Convert.FromBase64String(jsonParameters["Exponent"].Value<string>());
            rsaParameters.InverseQ = Convert.FromBase64String(jsonParameters["InverseQ"].Value<string>());
            rsaParameters.Modulus = Convert.FromBase64String(jsonParameters["Modulus"].Value<string>());
            rsaParameters.P = Convert.FromBase64String(jsonParameters["P"].Value<string>());
            rsaParameters.Q = Convert.FromBase64String(jsonParameters["Q"].Value<string>());

            return builder.AddSigningCredential(CryptoHelper.CreateRsaSecurityKey(rsaParameters, keyId), signingAlgorithm);
        }
        else
        {
            var key = CryptoHelper.CreateRsaSecurityKey();

            RSAParameters parameters;

            if (key.Rsa != null)
            {
                parameters = key.Rsa.ExportParameters(includePrivateParameters: true);
            }
            else
            {
                parameters = key.Parameters;
            }

            var jObject = new JObject
                {
                    {
                        "KeyId", key.KeyId
                    },
                    {
                        "Parameters", new JObject
                        {
                            {"D", Convert.ToBase64String(parameters.D)},
                            {"DP", Convert.ToBase64String(parameters.DP)},
                            {"DQ", Convert.ToBase64String(parameters.DQ)},
                            {"Exponent", Convert.ToBase64String(parameters.Exponent)},
                            {"InverseQ", Convert.ToBase64String(parameters.InverseQ)},
                            {"Modulus", Convert.ToBase64String(parameters.Modulus)},
                            {"P", Convert.ToBase64String(parameters.P)},
                            {"Q", Convert.ToBase64String(parameters.Q)}
                        }
                    }
                };

            if (persistKey)
            {
                File.WriteAllText(filename, jObject.ToString());
            }
            return builder.AddSigningCredential(key, signingAlgorithm);
        }
    }
}
