using System;
using System.Collections.Generic;
using System.Text;

namespace RobotApp.Commands
{
    // Tell the robot to go to the specified position
    internal class PlaceCommand : Command
    {
        public bool CanExecute(Robot robot)
        {
            return true;
        }

        public void Execute(Robot robot, string parameters)
        {
            var inputs = parameters.Split(",");
            if (inputs.Length != 3)
                throw new ArgumentException("Expected X,Y,F");

            int x, y;
            if (!Int32.TryParse(inputs[0], out x))
                throw new ArgumentException("Invalid X parameter for PLACE");
            if (!Int32.TryParse(inputs[1], out y))
                throw new ArgumentException("Invalid Y parameter for PLACE");
            if (!Bearing.IsValidBearing(inputs[2]))
                throw new ArgumentException("Invalid F parameter for PLACE");

            robot.SetPosition(x, y);
            robot.SetBearing(inputs[2]);
        }

    }
}
