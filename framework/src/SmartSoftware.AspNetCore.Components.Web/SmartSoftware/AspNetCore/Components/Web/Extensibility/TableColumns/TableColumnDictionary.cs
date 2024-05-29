using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Components.Web.Extensibility.TableColumns;

public class TableColumnDictionary : Dictionary<string, List<TableColumn>>
{
    public List<TableColumn> Get<T>()
    {
        return this.GetOrAdd(typeof(T).FullName!, () => new List<TableColumn>());
    }
}
