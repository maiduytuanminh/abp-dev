using System.Collections.Generic;

namespace SmartSoftware.MultiLingualObjects;

public interface IMultiLingualObject<TTranslation>
    where TTranslation : class, IObjectTranslation
{
    ICollection<TTranslation> Translations { get; set; }
}
