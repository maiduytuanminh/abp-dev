using System;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.CmsKit.Menus;

public interface IMenuItemRepository : IBasicRepository<MenuItem, Guid>
{
}
