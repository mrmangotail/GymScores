using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GymScores.Domain.Abstract;
using GymScores.Domain.Entities;
using GymScores.Controllers;
using Moq;

namespace GymScores.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void MeetList_Contains_AllMeets()
        {
            // create mock repository
            Mock<IMeetRepository> mock = new Mock<IMeetRepository>();

            mock.Setup(m => m.Meets).Returns(new Meet[]
            {
                new Meet { Location = "Colorado", Name = "Pikes Peak", MeetDate = DateTime.Now , MeetID = 1 },
                new Meet { Location = "California", Name = "Aliso Creek", MeetDate = DateTime.Now, MeetID = 2 }
            }.AsQueryable());

            // create a controller
            MeetController target = new MeetController(mock.Object);

            // action
            Meet[] result = ((IEnumerable<Meet>)target.List().ViewData.Model).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual("Colorado", result[0].Location);
            Assert.AreEqual("California", result[1].Location);
        }
    }
}
