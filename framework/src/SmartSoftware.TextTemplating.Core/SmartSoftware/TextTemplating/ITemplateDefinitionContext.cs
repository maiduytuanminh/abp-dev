using System.Collections.Generic;

namespace SmartSoftware.TextTemplating;

public interface ITemplateDefinitionContext
{
    IReadOnlyList<TemplateDefinition> GetAll();

    TemplateDefinition? GetOrNull(string name);

    void Add(params TemplateDefinition[] definitions);
}
