﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSoftware.AspNetCore.Mvc.MultiTenancy;

public class CurrentTenantDto
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public bool IsAvailable { get; set; }
}
