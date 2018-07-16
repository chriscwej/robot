using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotApp;
using System;
using System.IO;
using System.Text;

namespace RobotTest
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod, Description("Simple move")]
        public void TestSimpleMove()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("PLACE 0,0,NORTH");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("0,1,NORTH\r\n", sbOutput.ToString());
        }

        [TestMethod, Description("Simple turn")]
        public void TestSimpleTurn()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("PLACE 0,0,NORTH");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("0,0,WEST\r\n", sbOutput.ToString());
        }

        [TestMethod, Description("Moves and turns")]
        public void TestMoveStepStepTurnStepKick()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("PLACE 1,2,EAST");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("3,3,NORTH\r\n", sbOutput.ToString());
        }

        [TestMethod, Description("Test upper X and Y move boundaries")]
        public void TestLemming()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("PLACE 3,5,EAST");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("5,5,NORTH\r\n", sbOutput.ToString());
        }

        [TestMethod, Description("Test lower X and Y move boundaries")]
        public void TestReverseLemming()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("PLACE 2,0,WEST");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("0,0,SOUTH\r\n", sbOutput.ToString());
        }


        [TestMethod, Description("Full rotation test")]
        public void TestTheTurningTest()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("PLACE 3,3,EAST");
            sbInput.AppendLine("RIGHT");
            sbInput.AppendLine("RIGHT");
            sbInput.AppendLine("RIGHT");
            sbInput.AppendLine("RIGHT");
            sbInput.AppendLine("RIGHT");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("3,3,SOUTH\r\n", sbOutput.ToString());
        }

        [TestMethod, Description("Full rotation test reverse")]
        public void TestTheTurningTestReverse()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("PLACE 3,3,EAST");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("3,3,NORTH\r\n", sbOutput.ToString());
        }

        [TestMethod, Description("Ignore commands before place")]
        public void TestIgnorePlace()
        {
            Robot robot = new Robot(5);
            StringBuilder sbOutput = new StringBuilder();
            StringWriter writer = new StringWriter(sbOutput);
            StringBuilder sbInput = new StringBuilder();

            sbInput.AppendLine("LEFT");
            sbInput.AppendLine("RIGHT");
            sbInput.AppendLine("MOVE");
            sbInput.AppendLine("PLACE 3,3,EAST");
            sbInput.AppendLine("RIGHT");
            sbInput.AppendLine("REPORT");
            StringReader reader = new StringReader(sbInput.ToString());

            Program.ProcessCommands(robot, reader, writer);

            Assert.AreEqual("3,3,SOUTH\r\n", sbOutput.ToString());
        }
    }
}
