using System.ComponentModel;

namespace SharedKernel.Enums
{
    public enum OrderStatus
    {
        [Description("Order is currently pending and awaiting processing.")]
        Pending = 1,
        [Description("Order has been successfully completed.")]
        Success = 2,
        [Description("Order has been cancelled.")]
        Failed = 3
    }
}
