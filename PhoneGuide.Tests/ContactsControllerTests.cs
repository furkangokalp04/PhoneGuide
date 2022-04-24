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
    public class ContactsControllerTests
    {
        private readonly ContactsController _contactsController;
        private readonly Mock<IContactInfoManager> _contactInfoManager = new Mock<IContactInfoManager>();
        private readonly Mock<IContactManager> _contactManager = new Mock<IContactManager>();
        public ContactsControllerTests()
        {
            _contactsController = new ContactsController(_contactManager.Object, _contactInfoManager.Object);
        }

        [TestMethod]
        public async Task GetById_ReturnsTheContact_ByGivenContactId()
        {
            //Arrange
            var contactId = 4;
            var contactInfo = new ContactDto
            {
                Id = contactId,
                FirstName = "name",
                LastName = "surname",
                CompanyName = "test"
            };

            var dataResult = new DataResult<ContactDto>(contactInfo, true);
            _contactManager.Setup(x => x.GetByIdAsync(contactId)).ReturnsAsync(dataResult);

            //Act
            var result = await _contactsController.GetById(contactId);
            var resultAsOkObject = result as OkObjectResult;

            //Assert        
            Assert.AreEqual(dataResult.Data, resultAsOkObject.Value);
        }
    }
}
