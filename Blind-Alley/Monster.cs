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
    class Monster
    {
        private static int[] monsterCoords;
        private AStar aStar;
        private int numOfPlayerTrackingTicks;

        public static int[] MonsterCoords { get => monsterCoords; set => monsterCoords = value; }

        public int NumOfPlayerTrackingTicks { get => numOfPlayerTrackingTicks; set => numOfPlayerTrackingTicks = value; }

        private void findPathToPlayer()
        {

        }

        public void trackingWalk()
        {
            findPathToPlayer();
        }

        public void normalWalk()
        {
            //TODO: If you have the time, take into consideration the last direction it moved in and avoid letting him go back. Permit null values on the last direction so it doesn't get stuck on dead-ends.
            int? direction = Map.getRandomDirection(monsterCoords, true);

            switch (direction) 
            {
                // NORTH
                case 0:
                    Console.WriteLine("Moving North");
                    monsterCoords[1] -= 1;
                    break;

                // WEST
                case 1:
                    Console.WriteLine("Moving West");
                    monsterCoords[0] -= 1;
                    break;

                // SOUTH
                case 2:
                    Console.WriteLine("Moving South");
                    monsterCoords[1] += 1;
                    break;

                // EAST
                case 3:
                    Console.WriteLine("Moving East");
                    monsterCoords[0] += 1;
                    break;

                // ERROR
                default:
                    Console.WriteLine("ERROR: I got the value " + direction + " and I don't know what to do with it.");
                    break;
            }
        }

        public void walk()
        {
            if(numOfPlayerTrackingTicks == 0)
            {
                normalWalk();
            }
            else
            {
                trackingWalk();
                numOfPlayerTrackingTicks--;
            }
        }
    }
}