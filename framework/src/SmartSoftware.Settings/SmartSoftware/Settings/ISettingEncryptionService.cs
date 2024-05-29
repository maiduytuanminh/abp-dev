using JetBrains.Annotations;

namespace SmartSoftware.Settings;

public interface ISettingEncryptionService
{
    string? Encrypt([NotNull] SettingDefinition settingDefinition, string? plainValue);

    string? Decrypt([NotNull] SettingDefinition settingDefinition, string? encryptedValue);
}
