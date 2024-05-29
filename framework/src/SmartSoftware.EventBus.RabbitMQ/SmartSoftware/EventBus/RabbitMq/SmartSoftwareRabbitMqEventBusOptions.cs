using System.Collections.Generic;
using SmartSoftware.RabbitMQ;

namespace SmartSoftware.EventBus.RabbitMq;

public class SmartSoftwareRabbitMqEventBusOptions
{
    public const string DefaultExchangeType = RabbitMqConsts.ExchangeTypes.Direct;

    public string? ConnectionName { get; set; }

    public string ClientName { get; set; } = default!;

    public string ExchangeName { get; set; } = default!;

    public string? ExchangeType { get; set; }

    public ushort? PrefetchCount { get; set; }

    public IDictionary<string, object> QueueArguments { get; set; } = new Dictionary<string, object>();

    public IDictionary<string, object> ExchangeArguments { get; set; } = new Dictionary<string, object>();
    
    public string GetExchangeTypeOrDefault()
    {
        return string.IsNullOrEmpty(ExchangeType)
            ? DefaultExchangeType
            : ExchangeType!;
    }
}
