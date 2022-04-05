using AutoMapper;
using PhoneGuide.Contacts.Entities;
using PhoneGuide.Shared.Dtos;

namespace PhoneGuide.Contacts.Utilities.Mapping
{
    public class PhoneGuideContactsProfile:Profile
    {
        public PhoneGuideContactsProfile()
        {
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
        }
    }
}
