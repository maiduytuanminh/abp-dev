﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Threading.Tasks;
using NSubstitute;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.MultiTenancy;
using SmartSoftware.TestApp.Application.Dto;
using SmartSoftware.TestApp.Domain;
using Xunit;

namespace SmartSoftware.TestApp.Application;

public class PersonAppService_Tests : TestAppTestBase
{
    private readonly IPeopleAppService _peopleAppService;
    private ICurrentTenant _fakeCurrentTenant;

    public PersonAppService_Tests()
    {
        _peopleAppService = ServiceProvider.GetRequiredService<IPeopleAppService>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _fakeCurrentTenant = Substitute.For<ICurrentTenant>();
        services.AddSingleton(_fakeCurrentTenant);
    }

    [Fact]
    public async Task GetList()
    {
        var people = await _peopleAppService.GetListAsync(new PagedAndSortedResultRequestDto())
            ;
        people.Items.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task Create()
    {
        var uniquePersonName = Guid.NewGuid().ToString();
        var personDto = await _peopleAppService.CreateAsync(new PersonDto { Name = uniquePersonName });

        var repository = ServiceProvider.GetService<IRepository<Person, Guid>>();
        var person = await repository.FindAsync(personDto.Id);

        person.ShouldNotBeNull();
        person.TenantId.ShouldBeNull();
    }

    [Fact]
    public async Task Create_SetsTenantId()
    {
        _fakeCurrentTenant.Id.Returns(TestDataBuilder.TenantId1);

        var uniquePersonName = Guid.NewGuid().ToString();
        var personDto = await _peopleAppService.CreateAsync(new PersonDto { Name = uniquePersonName });

        var repository = ServiceProvider.GetRequiredService<IRepository<Person, Guid>>();
        var person = await repository.FindAsync(personDto.Id);

        person.ShouldNotBeNull();
        person.TenantId.ShouldNotBeNull();
        person.TenantId.ShouldNotBe(Guid.Empty);
        person.TenantId.ShouldBe(TestDataBuilder.TenantId1);
    }
}
