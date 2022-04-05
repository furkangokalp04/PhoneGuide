namespace PhoneGuide.Contacts.Entities
{
    public class ContactInfo:IEntity
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string PhoneNumber { get; set; }
        public string EMailAddress { get; set; }
        public string Location { get; set; }

        public Contact Contact { get; set; }
    }

}
