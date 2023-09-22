namespace AccountingService.Models
{
    public class Transaction
    {
        public Invoice ItemFrom { get; set; }
        public List<Invoice> ItemsTo { get; set; }

        public Transaction(Invoice itemFrom, List<Invoice> itemsTo) 
        {
            this.ItemFrom = itemFrom;
            this.ItemsTo = itemsTo;
        }
    }
}
