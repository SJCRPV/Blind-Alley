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
    // Maze Generator - Hunter-Kill - http://weblog.jamisbuck.org/2011/1/24/maze-generation-hunt-and-kill-algorithm

    class MapGen
    {
        Random rand;
        bool[,] map;
        string mapStr;
        int[] hunterCoords;
        int mapWidth;
        int mapHeight;
        bool notDone;

        public string getMapStr()
        {
            return mapStr;
        }

        private void printMap()
        {
            mapStr = "";
            for(int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i, j])
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

        public int[] getRandomCoord()
        {
            return new int[] { rand.Next(mapWidth), rand.Next(mapHeight) };
        }

        private int[][] getNeighbours(int[] coords)
        {
            return new int[][] { new int[] { coords[0], coords[1] - 2 }, new int[] { coords[0] - 2, coords[1] }, new int[] { coords[0], coords[1] + 2 }, new int[] { coords[0] + 2, coords[1] } };
        }

        private int? getRandomDirection()
        {
            int[][] neighbours = getNeighbours(hunterCoords);
            List<int> possibleDirections = new List<int>();
            for(int i = 0; i < neighbours.Length; i++)
            {
                try
                {
                    int[] neighbour = neighbours[i];
                    if(!map[neighbour[0], neighbour[1]])
                    {
                        possibleDirections.Add(i);
                    }
                }
                catch (IndexOutOfRangeException){}
            }

            if(possibleDirections.Count() == 0)
            {
                return null;
            }
            return possibleDirections[rand.Next(possibleDirections.Count())];
        }

        private void carvePassage(int[] startingCoords, int[] endCoords)
        {
            for (int x = startingCoords[0], y = startingCoords[1]; x != endCoords[0] || y != endCoords[1];)
            {
                map[x, y] = true;

                if(x != endCoords[0])
                {
                    x = x < endCoords[0] ? ++x : --x;
                }
                if(y != endCoords[1])
                {
                    y = y < endCoords[1] ? ++y : --y;
                }
            }
        }

        private bool itHasVisitedNeighbours(int[] coords)
        {
            int[][] neighbours = getNeighbours(coords);
            for (int i = 0; i < neighbours.Length; i++)
            {
                try
                {
                    int[] neighbour = neighbours[i];
                    if (map[neighbour[0], neighbour[1]])
                    {
                        carvePassage(coords, neighbours[i]);
                        return true;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }
            return false;
        } 

        private void hunt()
        {
            for(int i = 0; i < mapHeight; i += 2)
            {
                for(int j = 0; j < mapWidth; j += 2)
                {
                    if (map[i, j])
                    {
                        continue;
                    }
                    else
                    {
                        int[] potentialCoords = new int[] { i, j };
                        if(itHasVisitedNeighbours(potentialCoords))
                        {
                            hunterCoords = potentialCoords;
                            return;
                        }
                    }
                }
            }
            
            notDone = false;
        }

        private void walk()
        {
            int hunXCoord = hunterCoords[0];
            int hunYCoord = hunterCoords[1];
            int? direction = getRandomDirection();
            if(direction == null)
            {
                hunt();
                return;
            }
            else
            {
                switch (direction)
                {
                    // NORTH
                    case 0:
                        Console.WriteLine("Look in " + hunXCoord + ", " + hunYCoord + " going North");
                        map[hunXCoord, hunYCoord] = true;
                        map[hunXCoord, hunYCoord - 1] = true;
                        map[hunXCoord, hunYCoord - 2] = true;
                        hunterCoords[1] -= 2;
                        break;

                    // WEST
                    case 1:
                        Console.WriteLine("Look in " + hunXCoord + ", " + hunYCoord + " going West");
                        map[hunXCoord, hunYCoord] = true;
                        map[hunXCoord - 1, hunYCoord] = true;
                        map[hunXCoord - 2, hunYCoord] = true;
                        hunterCoords[0] -= 2;
                        break;

                    // SOUTH
                    case 2:
                        Console.WriteLine("Look in " + hunXCoord + ", " + hunYCoord + " going South");
                        map[hunXCoord, hunYCoord] = true;
                        map[hunXCoord, hunYCoord + 1] = true;
                        map[hunXCoord, hunYCoord + 2] = true;
                        hunterCoords[1] += 2;
                        break;

                    // EAST
                    case 3:
                        Console.WriteLine("Look in " + hunXCoord + ", " + hunYCoord + " going East");
                        map[hunXCoord, hunYCoord] = true;
                        map[hunXCoord + 1, hunYCoord] = true;
                        map[hunXCoord + 2, hunYCoord] = true;
                        hunterCoords[0] += 2;
                        break;

                    // ERROR
                    default:
                        Console.WriteLine("ERROR: I got the value " + direction + " and I don't know what to do with it.");
                        break;
                }
            }
        }

        public bool[,] generateMap()
        {
            notDone = true;
            while (notDone)
            {
                walk();
            }
            printMap();
            return map;
        }

        public MapGen(int nMapWidth, int nMapHeight)
        {
            rand = new Random();
            mapWidth = nMapWidth;
            mapHeight = nMapHeight;
            map = new bool[mapWidth, mapHeight];
            hunterCoords = getRandomCoord();
            generateMap();
        }
    }
}