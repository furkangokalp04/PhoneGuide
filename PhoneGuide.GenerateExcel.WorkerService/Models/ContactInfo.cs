namespace PhoneGuide.GenerateExcel.WorkerService.Models
{
    public partial class ContactInfo
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
