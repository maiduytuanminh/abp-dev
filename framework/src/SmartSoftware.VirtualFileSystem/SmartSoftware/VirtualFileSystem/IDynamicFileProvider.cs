﻿using Microsoft.Extensions.FileProviders;

namespace SmartSoftware.VirtualFileSystem;

public interface IDynamicFileProvider : IFileProvider
{
    void AddOrUpdate(IFileInfo fileInfo);

    bool Delete(string filePath);
}
