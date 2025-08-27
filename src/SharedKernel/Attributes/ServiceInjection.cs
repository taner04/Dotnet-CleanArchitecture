using SharedKernel.Enums;

namespace SharedKernel.Attributes;

/// <summary>
/// Attribute to mark a class or struct for dependency injection.
/// </summary>
/// <param name="interface">The interface type to inject.</param>
/// <param name="scope">The scope of the service.</param>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class ServiceInjection(Type @interface, ScopeType scope) : Attribute
{
    /// <summary>
    /// Gets the interface type to inject.
    /// </summary>
    public Type Interface => @interface;

    /// <summary>
    /// Gets the scope of the service.
    /// </summary>
    public ScopeType Scope => scope;
}