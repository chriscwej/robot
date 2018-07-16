using System;
using System.Collections.Generic;
using System.Text;

namespace RobotApp.Commands
{
    // Tell the robot to turn 90 degrees in the specified direction
    internal class TurnCommand : Command
    {
        private bool _clockwise;

        public TurnCommand(bool clockwise)
        {
            _clockwise = clockwise;
        }

        public bool CanExecute(Robot robot)
        {
            // Need a valid current bearing to turn
            return Bearing.IsValidBearing(robot.bearing);
        }

        // Turns the robot
        // Throws: ArgumentException
        public void Execute(Robot robot, string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
                throw new ArgumentException("No parameters expected");

            robot.Turn(_clockwise);
        }
    }
}
