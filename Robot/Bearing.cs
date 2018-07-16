using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotApp
{
    using Movement = Tuple<string, int, int>;   // bearing, x movement, y movement

    // Helper class for interacting with bearings
    static internal class Bearing
    {
        private static readonly List<Movement> _bearings = new List<Movement>
        {
            new Movement ("NORTH", 0, 1),
            new Movement ("EAST", 1, 0),
            new Movement ("SOUTH", 0, -1),
            new Movement ("WEST", -1, 0)
        };

        public static bool IsValidBearing(string bearing)
        {
            return _bearings.Exists(x => x.Item1 == bearing);
        }

        // For a given bearing, return the movement offset in x and y directions for one step
        public static void GetMovementOffset(string bearing, out int xMove, out int yMove)
        {
            Movement move;
            try
            {
                move = _bearings.First(x => x.Item1 == bearing);   // Throws InvalidOperationException if bearing not found
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("Cannot resolve movement - current bearing is unknown");
            }

            xMove = move.Item2;
            yMove = move.Item3;
        }

        // For the given bearing, return the new bearing after rotating 90 degrees in given direction
        public static string GetNewBearing(string bearing, bool clockwise)
        {
            int current = _bearings.FindIndex(x => x.Item1 == bearing);
            if (current == -1)
                throw new InvalidOperationException("Failed to change bearing - current bearing is unknown");

            if (clockwise)
                current = (current + 1) % _bearings.Count;  // % to loop forward WEST->NORTH
            else
                current = (current == 0 ? _bearings.Count : current) - 1; // == 0 to loop back NORTH->WEST

            return _bearings[current].Item1;
        }
    }
}
