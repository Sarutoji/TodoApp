using System;
using NUnit.Framework;

namespace ToDoListApplication.Tests
{
    [TestFixture]
#pragma warning disable SA1600 // Elements should be documented
    public class TaskTests
    {
        [TestCase(1, "Some task name", "Some tasks description", "2222.11.22", false)]
        [TestCase(14, "Shopping", "Some tasks description-- Shopping", "2226.05.21", false)]
        public void CreateTaskFiveParameters(int listID, string taskName, string taskDescription, DateTime dateTime, bool taskStatus)
        {
            var task = TODOApp.Program.CreateTask(listID, taskName, taskDescription, dateTime, taskStatus);
            Assert.IsTrue(task.listId == listID && task.taskName == taskName && task.taskDescription == taskDescription && task.dateTime == dateTime && task.taskStatus == taskStatus);
        }

        [TestCase(-10, "Some task name", "Some tasks description", "2222.11.22", false)]
        [TestCase(0, "Some task name", "Some tasks description", "2222.11.22", false)]
        [TestCase(1, "Some task name", "Some tasks description", "2022.05.22", false)]
        [TestCase(1, "Some task name", "Some tasks description", "2000.11.22", false)]
        public void CreateTaskThrowsArgumentException(int listID, string taskName, string taskDescription, DateTime dateTime, bool taskStatus)
        {
            Assert.Throws<ArgumentException>(() => TODOApp.Program.CreateTask(listID, taskName, taskDescription, dateTime, taskStatus));
        }

        [TestCase(1, "", "Some tasks description", "2222.11.22", false)]
        [TestCase(1, null, "Some tasks description", "2222.11.22", false)]
        public void CreateTaskThrowsArgumentNullException(int listID, string taskName, string taskDescription, DateTime dateTime, bool taskStatus)
        {
            Assert.Throws<ArgumentNullException>(() => TODOApp.Program.CreateTask(listID, taskName, taskDescription, dateTime, taskStatus));
        }
    }
#pragma warning restore SA1600 // Elements should be documented
}
