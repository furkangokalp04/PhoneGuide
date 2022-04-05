using PhoneGuide.Shared.Dtos;
using PhoneGuide.Shared.ResultTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneGuide.Contacts.Services
{
    public interface IContactInfoManager
    {
        Task<DataResult<List<ContactInfoDto>>> GetAllByContactIdAsync(int contactId);
        Task<Result> AddAsync(ContactInfoDto contactInfo);
        Task<Result> DeleteAsync(ContactInfoDto contactInfo);
        Task<Result> UpdateAsync(ContactInfoDto contactInfo);
    }
}
