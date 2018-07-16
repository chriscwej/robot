using System;
using System.Collections.Generic;
using System.Text;

namespace RobotApp
{
    internal class Robot
    {
        public int? xPos { get; private set; }
        public int? yPos { get; private set; }
        public string bearing { get; private set; }

        private int _maxPos = 0;

        public Robot (int maxPosition)
        {
            if (maxPosition < 0)
                throw new ArgumentOutOfRangeException("maxPosition", "Invalid table size specified");

            _maxPos = maxPosition;     
        }

        // Determines whether a given position is within the bounds of the table
        // Assuming 0->_maxPos for simple square table. Could extend to x,y or a function to handle more complex table shapes
        internal bool ValidPosition(int position)
        {
            if (position < 0 || position > _maxPos)
                return false;

            return true;
        }

        // Updates the robot's position if the new position is valid
        public void SetPosition(int newX, int newY)
        {
            if (!ValidPosition(newX))
                throw new ArgumentOutOfRangeException("newX", "Invalid X position specified");

            if (!ValidPosition(newY))
                throw new ArgumentOutOfRangeException("newY", "Invalid Y position specified");

            xPos = newX;
            yPos = newY;
        }


        // Updates the robot's bearing if the new bearing is valid
        public void SetBearing(string newBearing)
        {
            if (!Bearing.IsValidBearing(newBearing))
                throw new ArgumentOutOfRangeException("newBearing", "Invalid bearing specified");

            bearing = newBearing;
        }

        // Updates the robot's bearing by turning 90 degrees in the specified direction
        // Throws: InvalidOperationException if current bearing is bad
        public void Turn(bool clockwise)
        {
            bearing = Bearing.GetNewBearing(bearing, clockwise);
        }

        // Updates the robot's position by moving one step in the current direction
        public void MoveForward()
        {
            if (xPos == null || yPos == null || bearing == null)
                throw new InvalidOperationException("Cannot move without a current position and bearing");

            int xMove, yMove;
            Bearing.GetMovementOffset(bearing, out xMove, out yMove);
            if (!ValidPosition(xPos.Value + xMove))
                throw new InvalidOperationException("Cannot move - out of bounds");
            if (!ValidPosition(yPos.Value + yMove))
                throw new InvalidOperationException("Cannot move - out of bounds");

            xPos += xMove;
            yPos += yMove;
        }
    }
}
