namespace TransactionService.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int OpenAmount { get; set; }
        public int ClosedAmount { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int? ContractId { get; set; }
    }
}
