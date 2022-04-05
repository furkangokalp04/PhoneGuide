namespace PhoneGuide.Shared.Dtos
{
    public class ContactDto: IDto
    {
       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
    }
}
