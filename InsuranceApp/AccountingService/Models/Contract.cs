namespace AccountingService.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Registration { get; set; }
        public string Address { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public int Year { get; set; }
        public Boolean Tief { get; set; }
        public Boolean Weather { get; set; }
        public Boolean Crash { get; set; }
        public int Mounth { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean Activated { get; set; }
    }
}
