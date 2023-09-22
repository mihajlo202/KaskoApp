namespace ProcessBankStatementService.Models
{
    public class BankStatement
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Error { get; set; }
        public bool Applied { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
