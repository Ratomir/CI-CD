using Moq;
using ms17.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms17.BLL.Tests.Model
{
    public class BaseServiceTest
    {
        public Mock<PhotoSharingContext> _mockContext;

        public BaseServiceTest()
        {
            _mockContext = new Mock<PhotoSharingContext>();
        }
    }
}
