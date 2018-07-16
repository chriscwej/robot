using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotApp;
using RobotApp.Commands;
using System;
using System.IO;
using System.Text;

namespace RobotTest
{
    [TestClass]
    public class CommandTest
    {
        #region MoveCommand
        [TestMethod, Description("Move command can execute with valid robot")]
        public void TestCommandMoveCanExecuteValid()
        {
            Robot robot = new Robot(2);
            robot.SetPosition(1, 1);
            robot.SetBearing("NORTH");

            var command = new MoveCommand();

            Assert.IsTrue(command.CanExecute(robot));
        }

        [TestMethod, Description("Move command cannot execute without position")]
        public void TestCommandMoveCanExecuteInvalidPosition()
        {
            Robot robot = new Robot(2);
            robot.SetBearing("NORTH");

            var command = new MoveCommand();

            Assert.IsFalse(command.CanExecute(robot));
        }

        [TestMethod, Description("Move command cannot execute without bearing")]
        public void TestCommandMoveCanExecuteInvalidBearing()
        {
            Robot robot = new Robot(2);
            robot.SetPosition(1, 2);

            var command = new MoveCommand();

            Assert.IsFalse(command.CanExecute(robot));
        }

        [TestMethod, Description("Move command validates parameters")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandMoveExecuteValidatesParameters()
        {
            Robot robot = new Robot(2);
            var command = new MoveCommand();

            command.Execute(robot,"A,B,C");
        }

        [TestMethod, Description("Move command updates robot")]
        public void TestCommandMoveExecuteValid()
        {
            Robot robot = new Robot(2);
            robot.SetPosition(1, 1);
            robot.SetBearing("NORTH");

            var command = new MoveCommand();
            command.Execute(robot, "");

            Assert.AreEqual(1, robot.xPos);
            Assert.AreEqual(2, robot.yPos);
            Assert.AreEqual("NORTH", robot.bearing);
        }
        #endregion

        #region PlaceCommand
        [TestMethod, Description("Place command can execute always")]
        public void TestCommandPlaceCanExecuteValid()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            Assert.IsTrue(command.CanExecute(robot));
        }

        [TestMethod, Description("Place command validates parameters")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteHasParameters()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "");
        }

        [TestMethod, Description("Place command validates 3 parameters")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteTooFewParameters()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "A,B");
        }

        [TestMethod, Description("Place command validates 3 parameters")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteTooManyParameters()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "1,2,NORTH,SHOE");
        }

        [TestMethod, Description("Place command validates X parameter is numeric")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteXParameter()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "A,2,NORTH");
        }

        [TestMethod, Description("Place command validates X parameter is integer")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteXParameterFloat()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "1.0,2,NORTH");
        }

        [TestMethod, Description("Place command validates X parameter overflow")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteXParameterOverflow()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "12345678901,2,NORTH");
        }

        [TestMethod, Description("Place command validates Y parameter is numeric")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteYParameter()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "2,A,NORTH");
        }

        [TestMethod, Description("Place command validates Y parameter is integer")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteYParameterFloat()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "1,2.0,NORTH");
        }

        [TestMethod, Description("Place command validates Y parameter overflow")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteYParameterOverflow()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "1,23456789012,NORTH");
        }

        [TestMethod, Description("Place command validates F parameter is a bearing")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandPlaceExecuteFParameter()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "2,1,ANA");
        }

        [TestMethod, Description("Place command updates robot")]
        public void TestCommandPlaceExecuteValid()
        {
            Robot robot = new Robot(2);
            var command = new PlaceCommand();

            command.Execute(robot, "2,1,NORTH");

            Assert.AreEqual(2, robot.xPos);
            Assert.AreEqual(1, robot.yPos);
            Assert.AreEqual("NORTH", robot.bearing);
        }
        #endregion

        #region ReportCommand
        [TestMethod, Description("Report command can execute always")]
        public void TestCommandReportCanExecuteValid()
        {
            Robot robot = new Robot(2);
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            var command = new ReportCommand(writer);

            Assert.IsTrue(command.CanExecute(robot));
        }

        [TestMethod, Description("Report command validates parameters")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandReportExecuteValidateParameters()
        {
            Robot robot = new Robot(2);
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            var command = new ReportCommand(writer);

            command.Execute(robot, "A,B,C");
        }

        [TestMethod, Description("Report command outputs robot state")]
        public void TestCommandReportExecuteValidRobot()
        {
            Robot robot = new Robot(2);
            robot.SetPosition(1, 2);
            robot.SetBearing("SOUTH");

            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            var command = new ReportCommand(writer);
            command.Execute(robot, "");

            Assert.AreEqual("1,2,SOUTH\r\n",sb.ToString());
        }

        [TestMethod, Description("Report command handles unset robot states")]
        public void TestCommandReportExecuteValidNewRobot()
        {
            Robot robot = new Robot(2);

            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            var command = new ReportCommand(writer);
            command.Execute(robot, "");

            Assert.AreEqual("None,None,None\r\n", sb.ToString());
        }

        #endregion

        #region TurnCommand
        [TestMethod, Description("Turn command can execute with valid robot")]
        public void TestCommandTurnCanExecuteValid()
        {
            Robot robot = new Robot(2);
            robot.SetBearing("NORTH");

            var command = new TurnCommand(true);

            Assert.IsTrue(command.CanExecute(robot));
        }

        [TestMethod, Description("Turn command cannot execute without bearing")]
        public void TestCommandTurnCanExecuteInvalidBearing()
        {
            Robot robot = new Robot(2);
            robot.SetPosition(1, 2);

            var command = new TurnCommand(true);

            Assert.IsFalse(command.CanExecute(robot));
        }

        [TestMethod, Description("Turn command validates parameters")]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCommandTurnExecuteValidateParameters()
        {
            Robot robot = new Robot(2);
            var command = new TurnCommand(true);

            command.Execute(robot, "A,B,C");
        }

        [TestMethod, Description("Turn command updates robot clockwise")]
        public void TestCommandTurnExecuteClockwise()
        {
            Robot robot = new Robot(2);
            robot.SetPosition(1, 1);
            robot.SetBearing("NORTH");

            var command = new TurnCommand(true);
            command.Execute(robot, "");

            Assert.AreEqual(1, robot.xPos);
            Assert.AreEqual(1, robot.yPos);
            Assert.AreEqual("EAST", robot.bearing);
        }

        [TestMethod, Description("Turn command updates robot anticlockwise")]
        public void TestCommandTurnExecuteAntiClockwise()
        {
            Robot robot = new Robot(2);
            robot.SetPosition(1, 1);
            robot.SetBearing("NORTH");

            var command = new TurnCommand(false);
            command.Execute(robot, "");

            Assert.AreEqual(1, robot.xPos);
            Assert.AreEqual(1, robot.yPos);
            Assert.AreEqual("WEST", robot.bearing);
        }
        #endregion


    }
}
