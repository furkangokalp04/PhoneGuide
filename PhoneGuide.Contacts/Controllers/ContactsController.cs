using Microsoft.AspNetCore.Mvc;
using PhoneGuide.Contacts.Services;
using PhoneGuide.Shared.Dtos;
using PhoneGuide.Shared.ResultTypes;
using System.Threading.Tasks;

namespace PhoneGuide.Contacts.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactManager _contactManager;
        private readonly IContactInfoManager _contactInfoManager;

        public ContactsController(IContactManager contactService, IContactInfoManager contactInfoManager)
        {
            _contactManager = contactService;
            _contactInfoManager = contactInfoManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contactManager.GetAllAsync();
            return Ok(result.Data);
        }

        [HttpGet("getwithinfo/{id}")]
        public async Task<IActionResult> GetWithInfo(int id)
        {
            var contactResult = await _contactManager.GetByIdAsync(id);
            var contactInfoResult = await _contactInfoManager.GetAllByContactIdAsync(id);
            var model = new ContactInfoListDto
            {
                Contact = contactResult.Data,
                ContactInfoList = contactInfoResult.Data
            };
            return Ok(model);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _contactManager.GetByIdAsync(id);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactDto model)
        {
            await _contactManager.AddAsync(model);
            return Ok(new SuccessResult());
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContactDto model)
        {
            await _contactManager.UpdateAsync(model);
            return Ok(new SuccessResult());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactManager.DeleteAsync(new ContactDto { Id=id });
            return Ok(new SuccessResult());
        }
    }
}
