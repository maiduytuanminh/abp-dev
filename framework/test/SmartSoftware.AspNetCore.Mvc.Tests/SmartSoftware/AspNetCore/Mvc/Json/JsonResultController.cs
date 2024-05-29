﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartSoftware.AspNetCore.Mvc.Json;

[Route("api/json-result-test")]
public class JsonResultController : SmartSoftwareController
{
    [HttpGet]
    [Route("json-result-action")]
    public Task<JsonResultModel> ObjectResultAction()
    {
        return Task.FromResult(new JsonResultModel
        {
            Time = DateTime.Parse("2019-01-01 11:59:59")
        });
    }

    public class JsonResultModel
    {
        public DateTime Time { get; set; }
    }
}
