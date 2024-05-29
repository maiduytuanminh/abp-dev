using System;
using SmartSoftware.Application.Dtos;

namespace SmartSoftware.TestApp.Application.Dto;

public class DistrictDto : EntityDto
{
    public Guid CityId { get; set; }

    public string Name { get; set; }

    public int Population { get; set; }
}
