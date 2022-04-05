using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhoneGuide.Contacts.Entities;
using PhoneGuide.Shared.Dtos;
using PhoneGuide.Shared.ResultTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneGuide.Contacts.Services
{
    public class ContactManager : IContactManager
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly ContactContext _contactContext;

        public ContactManager(ContactContext contactContext, IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _contactContext = contactContext;

        }

        public async Task<Result> AddAsync(ContactDto contact)
        {

            var data = _mapper.Map<Contact>(contact);
            await _contactContext.AddAsync(data);
            await _contactContext.SaveChangesAsync();
            return new SuccessResult();
        }

        public async Task<Result> DeleteAsync(ContactDto contact)
        {
            var data = _mapper.Map<Contact>(contact);
            _contactContext.Remove(data);
            await _contactContext.SaveChangesAsync();
            return new SuccessResult();
        }

        public async Task<DataResult<List<ContactDto>>> GetAllAsync()
        {
            var data = await _contactContext.Contacts.ToListAsync();
            var model = _mapper.Map<List<ContactDto>>(data);
            return new SuccessDataResult<List<ContactDto>> (model);
        }
        public async Task<DataResult<ContactDto>> GetByIdAsync(int id)
        {
            var data = await _contactContext.Contacts.SingleOrDefaultAsync(contact=>contact.Id==id);
            var model = _mapper.Map<ContactDto>(data);
            return new SuccessDataResult<ContactDto>(model);
        }

        public async Task<Result> UpdateAsync(ContactDto contact)
        {
            var data = _mapper.Map<Contact>(contact);
            _contactContext.Update(data);
            await _contactContext.SaveChangesAsync();
            return new SuccessResult();
        }
    }
}
