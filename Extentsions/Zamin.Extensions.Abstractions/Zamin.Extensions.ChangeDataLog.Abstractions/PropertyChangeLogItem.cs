using System;

namespace Zamin.Extensions.ChangeDataLog.Abstractions;
public class PropertyChangeLogItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ChageInterceptorItemId { get; set; }
    public string PropertyName { get; set; }
    public string Value { get; set; }
}

