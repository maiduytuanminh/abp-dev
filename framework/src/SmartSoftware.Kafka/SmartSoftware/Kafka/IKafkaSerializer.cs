using System;

namespace SmartSoftware.Kafka;

public interface IKafkaSerializer
{
    byte[] Serialize(object obj);

    object Deserialize(byte[] value, Type type);

    T Deserialize<T>(byte[] value);
}
