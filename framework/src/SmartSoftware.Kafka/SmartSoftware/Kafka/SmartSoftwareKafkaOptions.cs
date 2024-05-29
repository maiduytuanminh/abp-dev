using System;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace SmartSoftware.Kafka;

public class SmartSoftwareKafkaOptions
{
    public KafkaConnections Connections { get; }

    public Action<ProducerConfig>? ConfigureProducer { get; set; }

    public Action<ConsumerConfig>? ConfigureConsumer { get; set; }

    public Action<TopicSpecification>? ConfigureTopic { get; set; }

    public SmartSoftwareKafkaOptions()
    {
        Connections = new KafkaConnections();
    }
}
