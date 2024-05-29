using JetBrains.Annotations;

namespace SmartSoftware.Users;

public interface IUpdateUserData
{
    bool Update([NotNull] IUserData user);
}
