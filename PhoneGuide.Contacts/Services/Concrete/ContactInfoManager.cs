using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PhoneGuide.Contacts.Entities;
using PhoneGuide.Shared.Dtos;
using PhoneGuide.Shared.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneGuide.Contacts.Services
{
    public class ContactInfoManager : IContactInfoManager
    {
        private readonly IMapper _mapper;

        private readonly ContactContext _contactContext;
        public ContactInfoManager(ContactContext contactContext, IMapper mapper)
        {
            _mapper = mapper;
            _contactContext = contactContext;
        }

        public async Task<Result> AddAsync(ContactInfoDto contactInfo)
        {
            var data = _mapper.Map<ContactInfo>(contactInfo);
            await _contactContext.AddAsync(data);
            await _contactContext.SaveChangesAsync();
            return new SuccessResult();
        }

        public async Task<Result> DeleteAsync(ContactInfoDto contactInfo)
        {
            var data = _mapper.Map<ContactInfo>(contactInfo);
            _contactContext.Remove(data);
            await _contactContext.SaveChangesAsync();
            return new SuccessResult();
        }

        public async Task<DataResult<List<ContactInfoDto>>> GetAllByContactIdAsync(int contactId)
        {
            var data = await _contactContext.ContactInfos.Where(contactInfo=>contactInfo.ContactId==contactId).ToListAsync();
            var model = data==null?new List<ContactInfoDto>():_mapper.Map<List<ContactInfoDto>>(data);
            return new SuccessDataResult<List<ContactInfoDto>>(model);
        }

        public Task<Result> UpdateAsync(ContactInfoDto contactInfo)
        {
            throw new NotImplementedException();
        }
    }
}
