using System;
using NUnit.Framework;

namespace ToDoListApplication.Tests
{
    [TestFixture]
#pragma warning disable SA1600 // Elements should be documented
    public class ListTests
    {
        [TestCase("TODO")]
        [TestCase("Monday TODO list")]
        public void CreateListOneParameter(string listName)
        {
            var list = TODOApp.Program.CreateList(listName);
            Assert.IsTrue(list.listName == listName);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateListThrowsArgumentNullException(string listName)
        {
            Assert.Throws<ArgumentNullException>(() => TODOApp.Program.CreateList(listName));
        }
    }
#pragma warning restore SA1600 // Elements should be documented
}
