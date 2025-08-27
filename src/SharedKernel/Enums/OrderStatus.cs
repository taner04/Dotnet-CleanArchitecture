using System.ComponentModel;

namespace SharedKernel.Enums;

/// <summary>
/// Represents the status of an order.
/// </summary>
public enum OrderStatus
{
    [Description("Order is currently pending and awaiting processing.")]
    Pending = 1,

    [Description("Order has been successfully completed.")]
    Success = 2,

    [Description("Order has been cancelled.")]
    Failed = 3,

    [Description("Order has been cancelled by the user.")]
    Cancelled = 4
}