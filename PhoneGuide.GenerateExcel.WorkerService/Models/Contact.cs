using System.Collections.Generic;

namespace PhoneGuide.GenerateExcel.WorkerService.Models
{
    public partial class Contact
    {
        public Contact()
        {
            ContactInfos = new HashSet<ContactInfo>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
