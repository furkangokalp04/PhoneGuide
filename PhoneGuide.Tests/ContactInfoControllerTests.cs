using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneGuide.Contacts.Controllers;
using PhoneGuide.Contacts.Services;
using PhoneGuide.Shared.ResultTypes;
using System.Threading.Tasks;
using PhoneGuide.Shared.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace PhoneGuide.Tests
{
    [TestClass]
    public class ContactInfoControllerTests
    {
        private readonly ContactInfoController _contactInfoController;
        private readonly Mock<IContactInfoManager> _contactInfoManager = new Mock<IContactInfoManager>(); 
        public ContactInfoControllerTests()
        {
            _contactInfoController = new ContactInfoController(_contactInfoManager.Object);
        }

        [TestMethod]
        public async Task GetAll_ReturnsTheContactList_ByGivenContactId()
        {
            //Arrange
            var contactId = 4;
            var contactInfo = new ContactInfoDto
            {
                Id = 1,
                ContactId = contactId,
                EMailAddress = "mail",
                Location="asdf",
                PhoneNumber="12312"
            };

            var contactInfoList = new List<ContactInfoDto>();
            contactInfoList.Add(contactInfo);

            var dataResult = new DataResult<List<ContactInfoDto>>(contactInfoList, true);
            _contactInfoManager.Setup(x => x.GetAllByContactIdAsync(contactId)).ReturnsAsync(dataResult);

            //Act
            var result = await _contactInfoController.GetAll(contactId);
            var resultAsOkObject = result as OkObjectResult;
            var resultValue = resultAsOkObject.Value as List<ContactInfoDto>;
            
            //Assert        
            Assert.AreEqual(dataResult.Data, resultValue);
        }
    }
}
