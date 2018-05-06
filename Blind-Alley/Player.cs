using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Blind_Alley
{
    public struct Directions
    {
        public const int NORTH = 0;
        public const int WEST = 1;
        public const int SOUTH = 2;
        public const int EAST = 3;
    }

    class Player
    {
        private int[] playerCoords;
        private int direction;

        public void interact()
        {

        }

        public void turnLeft()
        {
            if(direction == Directions.EAST)
            {
                direction = Directions.NORTH;
            }
            else
            {
                direction++;
            }
        }

        public void turnRight()
        {
            if (direction == Directions.WEST)
            {
                direction = Directions.NORTH;
            }
            else
            {
                direction--;
            }
        }

        public void move(bool forward)
        {
            int[] potentialPlayerCoords = playerCoords;
            if((direction == Directions.NORTH && forward) || (direction == Directions.SOUTH && !forward))
            {
                potentialPlayerCoords[1]--;
                if(Map.getAt(potentialPlayerCoords))
                {
                    playerCoords[1]--;
                }
                else
                {
                    //Thud against a wall
                }
            }
            else if((direction == Directions.SOUTH && forward) || (direction == Directions.NORTH && !forward))
            {
                potentialPlayerCoords[1]++;
                if(Map.getAt(potentialPlayerCoords))
                {
                    playerCoords[1]++;
                }
                else
                {
                    //Thud against a wall
                }
            }
            else if((direction == Directions.WEST && forward) || (direction == Directions.EAST && !forward))
            {
                potentialPlayerCoords[0]--;
                if(Map.getAt(potentialPlayerCoords))
                {
                    playerCoords[0]--;
                }
                else
                {
                    //Thud against a wall
                }
            }
            else if((direction == Directions.EAST && forward) || (direction == Directions.WEST && !forward))
            {
                potentialPlayerCoords[0]++;
                if(Map.getAt(potentialPlayerCoords))
                {
                    playerCoords[0]++;
                }
                else
                {
                    //Thud against a wall
                }
            }
        }

        public Player()
        {
            playerCoords = new int[] { 0, 0 };
        }
    }
}