﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.UI;
using SmartSoftware.Uow;

namespace SmartSoftware.AspNetCore.Mvc.Uow;

[Dependency(ReplaceServices = true)]
public class TestUnitOfWork : UnitOfWork
{
    private readonly TestUnitOfWorkConfig _config;

    public TestUnitOfWork(
        IServiceProvider serviceProvider,
        IUnitOfWorkEventPublisher unitOfWorkEventPublisher,
        IOptions<SmartSoftwareUnitOfWorkDefaultOptions> options, TestUnitOfWorkConfig config)
        : base(
            serviceProvider,
            unitOfWorkEventPublisher,
            options)
    {
        _config = config;
    }

    public override Task CompleteAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        ThrowExceptionIfRequested();
        return base.CompleteAsync(cancellationToken);
    }

    private void ThrowExceptionIfRequested()
    {
        if (_config.ThrowExceptionOnComplete)
        {
            throw new UserFriendlyException(TestUnitOfWorkConfig.ExceptionOnCompleteMessage);
        }
    }
}