using PhoneGuide.Shared.Dtos;
using PhoneGuide.Shared.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneGuide.Contacts.Services
{
    public interface IContactManager
    {
        Task<DataResult<List<ContactDto>>> GetAllAsync();
        Task<DataResult<ContactDto>> GetByIdAsync(int id);
        Task<Result> AddAsync(ContactDto contact);
        Task<Result> UpdateAsync(ContactDto contact);
        Task<Result> DeleteAsync(ContactDto contact);
    }
}
