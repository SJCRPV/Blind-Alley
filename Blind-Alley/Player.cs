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
        private static int[] playerCoords;
        private int direction;

        public static int[] PlayerCoords
        {
            get => playerCoords;
        }

        private void makeSound()
        {

        }

        private string countSteps()
        {
            int stepCounter = 0;
            int[] testCoords;
            string retStr = "";

            for(int i = 0; i <= Directions.EAST; i++)
            {
                switch(i)
                {
                    case Directions.NORTH:
                        testCoords = (int[])playerCoords.Clone(); 

                        do
                        {
                            stepCounter++;
                            testCoords[1]--;
                        } while (Map.getValAt(testCoords));

                        if(direction == i)
                        {
                            retStr += "You are facing North and can take " + (stepCounter - 1) + " steps in that direction, ";
                        }
                        else
                        {
                            retStr += "You can take " + (stepCounter - 1) + " steps North, ";
                        }

                        stepCounter = 0;
                        break;

                    case Directions.WEST:
                        testCoords = (int[])playerCoords.Clone();

                        do
                        {
                            stepCounter++;
                            testCoords[0]--;
                        } while (Map.getValAt(testCoords));

                        if (direction == i)
                        {
                            retStr += "You are facing West and can take " + (stepCounter - 1) + " steps in that direction, ";
                        }
                        else
                        {
                            retStr += "You can take " + (stepCounter - 1) + " steps West, ";
                        }

                        stepCounter = 0;
                        break;

                    case Directions.SOUTH:
                        testCoords = (int[])playerCoords.Clone();

                        do
                        {
                            stepCounter++;
                            testCoords[1]++;
                        } while (Map.getValAt(testCoords));

                        if (direction == i)
                        {
                            retStr += "You are facing South and can take " + (stepCounter - 1) + " steps in that direction, ";
                        }
                        else
                        {
                            retStr += "You can take " + (stepCounter - 1) + " steps South, ";
                        }

                        stepCounter = 0;
                        break;

                    case Directions.EAST:
                        testCoords = (int[])playerCoords.Clone();

                        do
                        {
                            stepCounter++;
                            testCoords[0]++;
                        } while (Map.getValAt(testCoords));

                        if (direction == i)
                        {
                            retStr += "You are facing East and can take " + (stepCounter - 1) + " steps in that direction.";
                        }
                        else
                        {
                            retStr += "You can take " + (stepCounter - 1) + " steps East.";
                        }

                        stepCounter = 0;
                        break;

                    default:
                        Console.WriteLine("ERROR: I got a direction I do not understand. It's: " + i);
                        break;
                }
            }

            return retStr;
        }

        private string checkYourSurroundings()
        {
            string retStr = "";

            //TODO: if there's something on the ground or ahead of you, take that over all else
            if(false)
            {
                //Check what's at these coordinates
            }
            else
            {
                retStr += "You tap your stick on the ground and listen.\n";
                retStr += countSteps();
                makeSound();
            }

            return retStr;
        }

        public string interact()
        {
            return checkYourSurroundings();
        }

        public void turnLeft()
        {
            direction++;
            if(direction > Directions.EAST)
            {
                direction = Directions.NORTH;
            }
        }

        public void turnRight()
        {
            direction--;
            if (direction < Directions.NORTH)
            {
                direction = Directions.EAST;
            }
        }

        public void move(bool forward)
        {
            int[] potentialPlayerCoords = (int[])playerCoords.Clone();
            if((direction == Directions.NORTH && forward) || (direction == Directions.SOUTH && !forward))
            {
                potentialPlayerCoords[1]--;
                if(Map.getValAt(potentialPlayerCoords))
                {
                    playerCoords[1]--;
                }
                else
                {
                    //Thud against a wall
                    Console.WriteLine("Thud");
                }
            }
            else if((direction == Directions.SOUTH && forward) || (direction == Directions.NORTH && !forward))
            {
                potentialPlayerCoords[1]++;
                if(Map.getValAt(potentialPlayerCoords))
                {
                    playerCoords[1]++;
                }
                else
                {
                    //Thud against a wall
                    Console.WriteLine("Thud");
                }
            }
            else if((direction == Directions.WEST && forward) || (direction == Directions.EAST && !forward))
            {
                potentialPlayerCoords[0]--;
                if(Map.getValAt(potentialPlayerCoords))
                {
                    playerCoords[0]--;
                }
                else
                {
                    //Thud against a wall
                    Console.WriteLine("Thud");
                }
            }
            else if((direction == Directions.EAST && forward) || (direction == Directions.WEST && !forward))
            {
                potentialPlayerCoords[0]++;
                if(Map.getValAt(potentialPlayerCoords))
                {
                    playerCoords[0]++;
                }
                else
                {
                    //Thud against a wall
                    Console.WriteLine("Thud");
                }
            }
        }

        public Player(int xStart, int yStart)
        {
            playerCoords = new int[] { xStart, yStart };
            direction = Directions.SOUTH;
        }
    }
}