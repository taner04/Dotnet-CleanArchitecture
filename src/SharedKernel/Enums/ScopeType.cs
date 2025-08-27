using SharedKernel.Attributes;

namespace SharedKernel.Enums;

/// <summary>
/// Represents the lifetime scope of a service in dependency injection for <see cref="ServiceInjection"/>
/// </summary>
public enum ScopeType
{
    Singleton,
    Scoped,
    Transient
}