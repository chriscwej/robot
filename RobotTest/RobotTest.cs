using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotApp;
using System;

namespace RobotTest
{
    [TestClass]
    public class RobotTest
    {
        [TestMethod, Description("Robot validates table size is positive")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRobotRejectsBadTableSize()
        {
            Robot robot = new Robot(-2);
        }

        #region ValidPosition

        [TestMethod, Description("ValidPosition returns true within table bounds")]
        public void TestRobotValidPositionValid()
        {
            Robot robot = new Robot(5);
            Assert.IsTrue(robot.ValidPosition(2));
        }

        [TestMethod, Description("ValidPosition returns true at lower table bounds")]
        public void TestRobotValidPositionValidLower()
        {
            Robot robot = new Robot(5);
            Assert.IsTrue(robot.ValidPosition(0));
        }

        [TestMethod, Description("ValidPosition returns true at upper table bounds")]
        public void TestRobotValidPositionValidUpper()
        {
            Robot robot = new Robot(5);
            Assert.IsTrue(robot.ValidPosition(5));
        }

        [TestMethod, Description("ValidPosition returns false below table bounds")]
        public void TestRobotValidPositionInvalidLower()
        {
            Robot robot = new Robot(5);
            Assert.IsFalse(robot.ValidPosition(-2));
        }

        [TestMethod, Description("ValidPosition returns true above table bounds")]
        public void TestRobotValidPositionInvalidUpper()
        {
            Robot robot = new Robot(5);
            Assert.IsFalse(robot.ValidPosition(6));
        }
        #endregion 

        #region SetPosition

        [TestMethod, Description("SetPosition updates for valid inputs")]
        public void TestRobotSetPositionValid()
        {
            Robot robot = new Robot(5);
            robot.SetPosition(3, 4);
            robot.SetPosition(1, 2);

            Assert.AreEqual(1, robot.xPos);
            Assert.AreEqual(2, robot.yPos);
        }

        [TestMethod, Description("SetPosition accepts lower bounds")]
        public void TestRobotSetPositionValidLower()
        {
            Robot robot = new Robot(5);
            robot.SetPosition(0, 0);

            Assert.AreEqual(0, robot.xPos);
            Assert.AreEqual(0, robot.yPos);
        }

        [TestMethod, Description("SetPosition accepts upper bounds")]
        public void TestRobotSetPositionValidUpper()
        {
            Robot robot = new Robot(4);
            robot.SetPosition(4, 4);

            Assert.AreEqual(4, robot.xPos);
            Assert.AreEqual(4, robot.yPos);
        }

        [TestMethod, Description("SetPosition detects invalid X")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRobotSetPositionInvalidX()
        {
            Robot robot = new Robot(5);
            robot.SetPosition(-3, 2);

            // Position not changed by invalid
            Assert.IsNull(robot.xPos);
            Assert.IsNull(robot.yPos);
        }

        [TestMethod, Description("SetPosition detects invalid Y")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRobotSetPositionInvalidY()
        {
            Robot robot = new Robot(5);
            robot.SetPosition(3, 2);    // Valid
            robot.SetPosition(1, 16);   // Invalid

            // Position not changed by invalid
            Assert.AreEqual(3, robot.xPos);
            Assert.AreEqual(2, robot.yPos);
        }
        #endregion 

        #region SetBearing

        [TestMethod, Description("SetBearing accepts valid value")]
        public void TestRobotSetBearingValid()
        {
            Robot robot = new Robot(5);
            robot.SetBearing("SOUTH");
            robot.SetBearing("EAST");
            robot.SetBearing("WEST");
            robot.SetBearing("NORTH");

            Assert.AreEqual("NORTH", robot.bearing);
        }

        [TestMethod, Description("SetBearing rejects invalid value")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestRobotSetBearingInvalid()
        {
            Robot robot = new Robot(5);
            robot.SetBearing("KATA");

            Assert.IsNull(robot.bearing);
        }

        #endregion

        #region Turn

        [TestMethod, Description("Turn clockwise works for valid bearing")]
        public void TestRobotTurnValidClockwise()
        {
            Robot robot = new Robot(5);
            robot.SetBearing("SOUTH");

            robot.Turn(true);

            Assert.AreEqual("WEST", robot.bearing);
        }

        [TestMethod, Description("Turn anticlockwise works for valid bearing")]
        public void TestRobotTurnValidAntiClockwise()
        {
            Robot robot = new Robot(5);
            robot.SetBearing("SOUTH");

            robot.Turn(false);

            Assert.AreEqual("EAST", robot.bearing);
        }

        [TestMethod, Description("Turn clockwise fails for invalid bearing")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotTurnInvalidClockwise()
        {
            Robot robot = new Robot(5);
            robot.Turn(true);

            Assert.IsNull(robot.bearing);
        }

        [TestMethod, Description("Turn anticlockwise fails for invalid bearing")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotTurnInvalidAntiClockwise()
        {
            Robot robot = new Robot(5);
            robot.Turn(false);

            Assert.IsNull(robot.bearing);
        }
        #endregion

        #region MoveForward
        [TestMethod, Description("MoveForward works for valid position")]
        public void TestRobotMoveForwardValid()
        {
            Robot robot = new Robot(5);
            robot.SetPosition(1, 2);
            robot.SetBearing("NORTH");

            robot.MoveForward();

            Assert.AreEqual(1, robot.xPos);
            Assert.AreEqual(3, robot.yPos);
            Assert.AreEqual("NORTH", robot.bearing);
        }

        [TestMethod, Description("MoveForward requires valid position")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotMoveForwardNoPosition()
        {
            Robot robot = new Robot(5);
            robot.SetBearing("NORTH");

            robot.MoveForward();

            Assert.IsNull(robot.xPos);
            Assert.IsNull(robot.yPos);
            Assert.AreEqual("NORTH", robot.bearing);
        }

        [TestMethod, Description("MoveForward requires valid bearing")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotMoveForwardNoBearing()
        {
            Robot robot = new Robot(5);
            robot.SetPosition(1, 2);

            robot.MoveForward();

            Assert.AreEqual(1, robot.xPos);
            Assert.AreEqual(2, robot.yPos);
            Assert.IsNull(robot.bearing);
        }

        [TestMethod, Description("MoveForward validates upper X")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotMoveForwardUpperX()
        {
            Robot robot = new Robot(3);
            robot.SetPosition(3, 3);
            robot.SetBearing("EAST");

            robot.MoveForward();

            Assert.AreEqual(3, robot.xPos);
            Assert.AreEqual(3, robot.yPos);
            Assert.AreEqual("EAST", robot.bearing);
        }

        [TestMethod, Description("MoveForward validates upper Y")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotMoveForwardUpperY()
        {
            Robot robot = new Robot(3);
            robot.SetPosition(3, 3);
            robot.SetBearing("NORTH");

            robot.MoveForward();

            Assert.AreEqual(3, robot.xPos);
            Assert.AreEqual(3, robot.yPos);
            Assert.AreEqual("NORTH", robot.bearing);
        }

        [TestMethod, Description("MoveForward validates lower X")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotMoveForwardLowerX()
        {
            Robot robot = new Robot(3);
            robot.SetPosition(0, 0);
            robot.SetBearing("WEST");

            robot.MoveForward();

            Assert.AreEqual(0, robot.xPos);
            Assert.AreEqual(0, robot.yPos);
            Assert.AreEqual("WEST", robot.bearing);
        }

        [TestMethod, Description("MoveForward validates lower Y")]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestRobotMoveForwardLowerY()
        {
            Robot robot = new Robot(3);
            robot.SetPosition(0, 0);
            robot.SetBearing("SOUTH");

            robot.MoveForward();

            Assert.AreEqual(0, robot.xPos);
            Assert.AreEqual(0, robot.yPos);
            Assert.AreEqual("SOUTH", robot.bearing);
        }

        #endregion
    }
}
