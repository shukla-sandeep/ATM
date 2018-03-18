using ATM.Controllers;
using ATM.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ATM.Test
{
    [TestClass]
    public class SampleDataControllerTest
    {
        SampleDataController sampleDataController;
        [TestInitialize]
        public void Setup()
        {
            sampleDataController = new SampleDataController(new AccountData());
            sampleDataController.ControllerContext = new ControllerContext();
            sampleDataController.ControllerContext.HttpContext = new DefaultHttpContext();
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Append("cardNumber", "1234567891234567");
        }
        [TestMethod]
        public void Card_Details_Are_Correct()
        {
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Append("pin", "1234");
            bool result = sampleDataController.Authenticate();
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void Card_Details_Are_Not_Correct()
        {
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Remove("pin");
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Append("pin", "1235");
            bool result = sampleDataController.Authenticate();
            Assert.AreEqual(result, false);
        }
        [TestMethod]
        public void Authenticate_Throws_And_Handles_Exception()
        {
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Remove("pin");
            bool result = sampleDataController.Authenticate();
            Assert.AreEqual(sampleDataController.Response.StatusCode, 500);
        }
        [TestMethod]
        public void Get_Account_Balance()
        {
            var result = sampleDataController.Balance();
            Assert.AreEqual(result, 10000);
        }
        [TestMethod]
        public void Balance_Throws_And_Handles_Exception()
        {
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Remove("cardNumber");
            var result = sampleDataController.Balance();
            Assert.AreEqual(sampleDataController.Response.StatusCode, 500);
        }
        [TestMethod]
        public void Withdraw_Valid_Amount()
        {
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Append("cardNumber", "1234567891234567");
            dynamic param = new JObject();
            param.cardNumber = "1234567891234567";
            param.amount = "2000";
            var result = sampleDataController.Withdraw(param);
            Assert.AreEqual(result, 8000);
        }
        [TestMethod]
        public void Withdraw_Access_Amount()
        {
            dynamic param = new JObject();
            param.cardNumber = "1234567891234567";
            param.amount = "11000";
            var result = sampleDataController.Withdraw(param);
            Assert.AreEqual(result, null);
        }
        [TestMethod]
        public void Withdraw_Throws_And_Handles_Exception()
        {
            sampleDataController.ControllerContext.HttpContext.Request.Headers.Remove("cardNumber");
            var result = sampleDataController.Withdraw(JsonConvert.SerializeObject(new { cardNumber = "1234567891234567", amount = 9000 }));
            Assert.AreEqual(sampleDataController.Response.StatusCode, 500);
        }
    }
}
