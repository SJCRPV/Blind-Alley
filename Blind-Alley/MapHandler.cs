using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Blind_Alley
{
    class MapHandler : Map
    {
        static float minDistanceToPlayer;
        static string mapStr;
        MapGen mapGen;
        int[] objectiveCoords;
        int[] monsterCoords;

        private void printMap()
        {
            mapStr = "";
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if(i == monsterCoords[0] && j == monsterCoords[1])
                    {
                        mapStr += 'M';
                    }
                    else if(i == objectiveCoords[0] && j == objectiveCoords[1])
                    {
                        mapStr += 'O';
                    }
                    else if (map[i, j])
                    {
                        mapStr += '.';
                    }
                    else
                    {
                        mapStr += '=';
                    }
                }
                mapStr += "\n";
            }
        }

        public string getMapStr()
        {
            printMap();
            return mapStr;
        }

        private void walk()
        {
            int? direction = getRandomDirection(monsterCoords, true);

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

        public void moveMonster()
        {
            TimeSpan startTime = TimeSpan.Zero;
            TimeSpan delayBetweenMovement = TimeSpan.FromSeconds(1);

            Timer timer = new Timer((e) =>
            {
                walk();
                Console.WriteLine("X: " + monsterCoords[0] + " Y: " + monsterCoords[1]);
            }, null, startTime, delayBetweenMovement);
        }

        private int[] placeNearObjective()
        {
            try
            {
                int[] retCoords;
                int x = objectiveCoords[0];
                int y = objectiveCoords[1];

                if(map[x + 1, y])
                {
                    retCoords = new int[] { x + 1, y };
                }
                else if(map[x - 1, y])
                {
                    retCoords = new int[] { x - 1, y };
                }
                else if(map[x, y + 1])
                {
                    retCoords = new int[] { x, y + 1 };
                }
                else if(map[x, y - 1])
                {
                    retCoords = new int[] { x, y - 1 };
                }
                else
                {
                    throw new Exception("The objective is blocked by walls on all sides. Something is wrong");
                }
                return retCoords;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new int[] { -1, -1 };
            }
        }

        private bool farEnoughFromPlayer(int[] coords)
        {
            return Math.Abs(Player.PlayerCoords[0] - coords[0]) + Math.Abs(Player.PlayerCoords[1] - coords[1]) > minDistanceToPlayer;
        }

        public void placeRelevantPieces()
        {
            objectiveCoords = mapGen.getRandomCoord();
            while (!farEnoughFromPlayer(objectiveCoords))
            {
                objectiveCoords = mapGen.getRandomCoord();
            }
            monsterCoords = placeNearObjective();
        }

        public MapHandler(int nMapWidth, int nMapHeight)
        {
            rand = new Random();
            mapWidth = nMapWidth;
            mapHeight = nMapHeight;
            mapGen = new MapGen();
            minDistanceToPlayer = (nMapWidth / 2) + (nMapHeight / 2);
            map = mapGen.generateMap();
            placeRelevantPieces();
        }
    }
}