using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotApp;
using System;

namespace RobotTest
{
    [TestClass]
    public class BearingTest
    {
        [TestMethod, Description("IsValidBearing returns true for valid bearing")]
        public void TestIsValidBearingTrue()
        {
            Assert.IsTrue(Bearing.IsValidBearing("NORTH"));
        }

        [TestMethod, Description("IsValidBearing returns false for invalid bearing")]
        public void TestIsValidBearingFalse()
        {
            Assert.IsFalse(Bearing.IsValidBearing("SPAM"));
        }

        [TestMethod, Description("GetMovementOffset returns movement for valid bearing")]
        public void TestGetMovementOffsetValid()
        {
            int x, y;
            Bearing.GetMovementOffset("NORTH", out x, out y);
            Assert.AreEqual(0, x);
            Assert.AreEqual(1, y);
        }

        [TestMethod, Description("GetMovementOffset throws exception for invalid bearing")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetMovementOffsetInvalid()
        {
            int x, y;
            Bearing.GetMovementOffset("NARF", out x, out y);
        }


        [TestMethod, Description("GetNewBearing turns clockwise 90 degrees")]
        public void TestGetNewBearingRightClockwise()
        {
            string bearing = Bearing.GetNewBearing("NORTH", true);
            Assert.AreEqual("EAST", bearing);
        }

        [TestMethod, Description("GetNewBearing turns anticlockwise 90 degrees")]
        public void TestGetNewBearingRightAntiClockwise()
        {
            string bearing = Bearing.GetNewBearing("EAST", false);
            Assert.AreEqual("NORTH", bearing);
        }

        [TestMethod, Description("GetNewBearing cycles clockwise correctly")]
        public void TestGetNewBearingRightFullClockwise()
        {
            // Turn 450 degrees
            string bearing = "NORTH";
            bearing = Bearing.GetNewBearing(bearing, true);
            bearing = Bearing.GetNewBearing(bearing, true);
            bearing = Bearing.GetNewBearing(bearing, true);
            bearing = Bearing.GetNewBearing(bearing, true);
            bearing = Bearing.GetNewBearing(bearing, true);
            Assert.AreEqual("EAST", bearing);
        }

        [TestMethod, Description("GetNewBearing cycles anticlockwise correctly")]
        public void TestGetNewBearingRightFullAntiClockwise()
        {
            // Turn 450 degrees
            string bearing = "EAST";
            bearing = Bearing.GetNewBearing(bearing, false);
            bearing = Bearing.GetNewBearing(bearing, false);
            bearing = Bearing.GetNewBearing(bearing, false);
            bearing = Bearing.GetNewBearing(bearing, false);
            bearing = Bearing.GetNewBearing(bearing, false);
            Assert.AreEqual("NORTH", bearing);
        }

        [TestMethod, Description("GetNewBearing throws exception for invalid bearing")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetNewBearingThrowsException()
        {
            string bearing = Bearing.GetNewBearing("PARMA", false);
        }
    }
}
