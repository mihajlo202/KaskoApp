namespace TransactionService.Models
{
    public class InvoiceTransaction
    {
        public Invoice ItemFrom { get; set; }
        public List<Invoice> ItemsTo { get; set; }

        public InvoiceTransaction(Invoice itemFrom, List<Invoice> itemsTo)
        {
            this.ItemFrom = itemFrom;
            this.ItemsTo = itemsTo;
        }
    }
}
