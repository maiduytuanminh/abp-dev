﻿namespace SmartSoftware.BlobStoring.Aws;

public interface IAwsBlobNameCalculator
{
    string Calculate(BlobProviderArgs args);
}
