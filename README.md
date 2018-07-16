# Robot v1.0
Toy Robot Code Challenge

Developer: Mark Longmuir <BR>
Language: C# .NET Core <BR>

## Description:
Simulates a robot moving on a square tabletop of 5x5 units

## Build & Run:
* Open Robot.sln and build and run in Visual Studio 2017
* Or from Robot sub-directory:
  * dotnet run RobotApp.csproj

## Unit & function tests:
* Run tests in VS2017 test runner
* Or from RobotTest sub-directory:
  * dotnet test RobotTest.csproj

## Commands:
Accepts input from the command line.<BR>
Reports write to standard output. <BR>
Diagnostic information to standard error.<BR>

**PLACE _X,Y,F_** - Positions the robot at X, Y with bearing F (NORTH, EAST, SOUTH, WEST) <BR>
**MOVE** - Moves the robot one step forward <BR>
**LEFT** - Turns the robot left 90 degrees <BR>
**RIGHT** - Turns the robot right 90 degress <BR>
**REPORT** - Prints the robot's current position <BR>
**END** - Exits the program (or blank row) <BR>

