namespace Billing.core
{
    public interface IBook
    {
        string sku { get; set; }
        int Quantity { get; set; }
    }
}
