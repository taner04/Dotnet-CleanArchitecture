using SharedKernel.Enums;

namespace SharedKernel.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ServiceInjectionAttribute(Type @interface, ScopeType scope) : Attribute
{
    public Type Interface => @interface;
    public ScopeType Scope => scope;
}