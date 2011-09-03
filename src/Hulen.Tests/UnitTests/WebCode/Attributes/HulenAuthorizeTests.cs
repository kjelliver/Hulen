using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Services;
using Hulen.WebCode.Attributes;
using Moq;
using NUnit.Framework;

namespace Hulen.Tests.UnitTests.WebCode.Attributes
{
    [TestFixture]
    public class HulenAuthorizeTests
    {
        private HulenAuthorizeAttribute _subject;
        private Mock<UserService> _userServiceMock;

        [SetUp]
        public void SetUp()
        {
            _userServiceMock = new Mock<UserService>();
            _subject = new HulenAuthorizeAttribute("Test");
        }
    }
}
