using Lesson5_Homework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IT_AcademyHomework.Tests.Lesson5
{
    [TestClass]
    public class StudyTaskTest
    {
        [TestMethod]
        public void GetSubArrayWithMaxSumOfElements_SmokeTest_ReturnsCorrectArray()
        {
            // Arrange
            var studyTask = new StudyTask();
            var sourceArray = new int[] { -5, 20, -4, 10, -18 };

            // Act
            var resultArray = studyTask.GetSubArrayWithMaxSumOfElements(sourceArray);

            // Assert
            Assert.IsTrue(resultArray[0] == 20);
            Assert.IsTrue(resultArray[1] == -4);
            Assert.IsTrue(resultArray[2] == 10);
        }

        [TestMethod]
        public void GetSubArrayWithMaxSumOfElements_SmokeTestWithAnotherParameters_ReturnsCorrectArray()
        {
            // Arrange
            var studyTask = new StudyTask();
            var sourceArray = new int[] { -5, 20, -4, -10, -18 };

            // Act
            var resultArray = studyTask.GetSubArrayWithMaxSumOfElements(sourceArray);

            // Assert
            Assert.IsTrue(resultArray[0] == 20);
            Assert.IsTrue(resultArray[1] == -4);
        }
    }
}
