using Moq;
using TestApp.Controllers;
using TestApp.Data.interfaces;
// using TestApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TestAppTesting
{
    [TestClass]
    public class OverviewControllerTests
    {
        [TestMethod]
        public void Index_ReturnsViewWithModel()
        {
            int id = 1;
            var mockJsonsRepository = new Mock<IAllJsons>();
            string json = @"{
                ""keyA"": {
                    ""keyB"": {
                        ""keyC"": ""value1"",
                        ""keyD"": ""value2""
                    },
                    ""keyD"": ""value5""
                },
                ""keyE"": ""value3"",
                ""keyC"": {
                    ""keyD"": ""value4""
                },
                ""array"": [1, 2, 3]
            }";
            var expectedData = JsonConvert.DeserializeObject<UploadJson>(json);
            mockJsonsRepository.Setup(repo => repo.GetUploadJson(id)).Returns(expectedData);



            var controller = new OverviewController(mockJsonsRepository.Object);

            var result = controller.Index(id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual(expectedData, result.Model as UploadJson);
        }
    }
}
