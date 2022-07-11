namespace Billing.core
{
    public interface IDeal
    {
        string Name { get; }
        decimal Discount { get; }
        int ForPurchaseOf { get; }
    }
}
