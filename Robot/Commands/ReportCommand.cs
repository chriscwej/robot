using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RobotApp.Commands
{
    // Print out the current position and bearing of the robot
    internal class ReportCommand : Command
    {
        private TextWriter _writer;

        public ReportCommand(TextWriter output)
        {
            _writer = output;
        }


        public bool CanExecute(Robot robot)
        {
            return true;
        }

        public void Execute(Robot robot, string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
                throw new ArgumentException("No parameters expected");

            _writer.WriteLine((robot.xPos?.ToString() ?? "None") + ","
                            + (robot.yPos?.ToString() ?? "None") + ","
                            + (string.IsNullOrEmpty(robot.bearing) ? "None" : robot.bearing));
            _writer.Flush();
        }
    }
}
