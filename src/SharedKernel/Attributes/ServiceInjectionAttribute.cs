using SharedKernel.Enums;

namespace SharedKernel.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ServiceInjectionAttribute : Attribute
{
    public ServiceInjectionAttribute(Type @interface, ScopeType scope)
    {
        Interface = @interface;
        Scope = scope;
    }

    public Type Interface { get; set; }
    public ScopeType Scope { get; set; }
}