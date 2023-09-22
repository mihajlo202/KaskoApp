namespace ContractService.Models
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
        public bool Tief { get; set; }
        public bool Weather { get; set; }
        public bool Crash { get; set; }
        public int Mounth { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Activated { get; set; }

    }
}
