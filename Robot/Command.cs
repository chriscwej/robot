using System;
using System.Collections.Generic;
using System.Text;

namespace RobotApp
{
    interface Command
    {
        bool CanExecute(Robot robot);        // Can the command be attempted now - e.g. bind to command buttons
        void Execute(Robot robot, string parameters);        // Executes the command
    }
}
