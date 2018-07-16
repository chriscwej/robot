using RobotApp.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RobotTest")]

namespace RobotApp
{
    class Program
    {
        private const int TABLE_SIZE = 5;
        private static Dictionary<string, Command> mapCommands;

        static void Main(string[] args)
        {
            Robot robot = new Robot(TABLE_SIZE);
            ProcessCommands(robot, new StreamReader(Console.OpenStandardInput()), new StreamWriter(Console.OpenStandardOutput()));
        }

        static void InitCommands(TextWriter writer)
        {
            mapCommands = new Dictionary<string, Command>()
            {
                { "PLACE", new PlaceCommand() },
                { "MOVE", new MoveCommand() },
                { "LEFT", new TurnCommand(false) },
                { "RIGHT", new TurnCommand(true) },
                { "REPORT", new ReportCommand(writer) }
            };

        }

        static internal void ProcessCommands(Robot robot, TextReader input, TextWriter output)
        {
            InitCommands(output);

            string currentLine;
            Command command;
            while ((currentLine = input.ReadLine()) != "END" && !string.IsNullOrEmpty(currentLine))
            {
                var request = currentLine.Split(" ");
                if (request.Length < 1)
                    continue;

                var action = request[0];
                if (!mapCommands.TryGetValue(action, out command))
                {
                    Console.Error.WriteLine("Invalid command specified - " + action);
                    continue;
                }
                if (!command.CanExecute(robot))
                {
                    Console.Error.WriteLine("Skipping " + action + " command - cannot execute now");
                    continue;
                }

                try
                {
                    command.Execute(robot, currentLine.Substring(request[0].Length));
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(action + " command failed - " + e.Message);
                }
            }
        }
    }
}
