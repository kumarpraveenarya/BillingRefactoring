namespace Billing.core
{
    public interface IBookStock
    {
        string SKU { get; set; }
        decimal Price { get; set; }        
    }
}
