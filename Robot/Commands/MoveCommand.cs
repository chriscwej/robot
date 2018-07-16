using System;
using System.Collections.Generic;
using System.Text;

namespace RobotApp.Commands
{
    // Tell the robot to move one step forward in the current direction
    internal class MoveCommand : Command
    {
        public bool CanExecute(Robot robot)
        {
            // Need a valid current position and bearing to move
            return robot.xPos != null && robot.yPos != null && Bearing.IsValidBearing(robot.bearing);
        }

        public void Execute(Robot robot, string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
                throw new ArgumentException("No parameters expected");

            robot.MoveForward();
        }
    }
}
