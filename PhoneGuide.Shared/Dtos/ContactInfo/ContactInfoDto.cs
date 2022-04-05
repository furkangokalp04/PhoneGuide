namespace PhoneGuide.Shared.Dtos
{
    public class ContactInfoDto:IDto
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string PhoneNumber { get; set; }
        public string EMailAddress { get; set; }
        public string Location { get; set; }
    }
}
