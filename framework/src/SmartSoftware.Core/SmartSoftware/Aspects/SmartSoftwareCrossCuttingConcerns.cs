using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.Aspects;

public static class SmartSoftwareCrossCuttingConcerns
{
    //TODO: Move these constants to their own assemblies!

    public const string Auditing = "SmartSoftwareAuditing";
    public const string UnitOfWork = "SmartSoftwareUnitOfWork";
    public const string FeatureChecking = "SmartSoftwareFeatureChecking";
    public const string GlobalFeatureChecking = "SmartSoftwareGlobalFeatureChecking";

    public static void AddApplied(object obj, params string[] concerns)
    {
        if (concerns.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(concerns), $"{nameof(concerns)} should be provided!");
        }

        (obj as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.AddRange(concerns);
    }

    public static void RemoveApplied(object obj, params string[] concerns)
    {
        if (concerns.IsNullOrEmpty())
        {
            throw new ArgumentNullException(nameof(concerns), $"{nameof(concerns)} should be provided!");
        }

        var crossCuttingEnabledObj = obj as IAvoidDuplicateCrossCuttingConcerns;
        if (crossCuttingEnabledObj == null)
        {
            return;
        }

        foreach (var concern in concerns)
        {
            crossCuttingEnabledObj.AppliedCrossCuttingConcerns.RemoveAll(c => c == concern);
        }
    }

    public static bool IsApplied([NotNull] object obj, [NotNull] string concern)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        if (concern == null)
        {
            throw new ArgumentNullException(nameof(concern));
        }

        return (obj as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.Contains(concern) ?? false;
    }

    public static IDisposable Applying(object obj, params string[] concerns)
    {
        AddApplied(obj, concerns);
        return new DisposeAction<ValueTuple<object, string[]>>(static (state) =>
        {
            var (obj, concerns) = state;
            RemoveApplied(obj, concerns);
        }, (obj, concerns));
    }

    public static string[] GetApplieds(object obj)
    {
        var crossCuttingEnabledObj = obj as IAvoidDuplicateCrossCuttingConcerns;
        if (crossCuttingEnabledObj == null)
        {
            return new string[0];
        }

        return crossCuttingEnabledObj.AppliedCrossCuttingConcerns.ToArray();
    }
}
