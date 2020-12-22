using IT_AcademyHomework.Common.Validation;
using Lesson4_Homework.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IT_AcademyHomework.Tests.Lesson4
{
    [TestClass]
    public class TemperatureConverterControllerTests
    {
        [TestMethod]
        public void GenerateResponseWithContentMethod_()
        {
            var testCelsiumValue = 10d;
            var moqValidator = new Mock<IValidator<double>>();
            moqValidator.Setup(v => v.Validate(It.IsAny<double>())).Returns(new ValidationResult(true));

            var temperatureController = new TemperatureConverterController(moqValidator.Object);
            var resultFahrenheitValue = temperatureController.CelsiumToFahrenheits(testCelsiumValue);
        }
    }
}
