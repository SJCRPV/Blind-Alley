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

    class MapGen : Map
    {
        int[] hunterCoords;
        bool notDone;

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
            int[][] neighbours = getNeighbours(coords, false);
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
            int? direction = getRandomDirection(hunterCoords, false);
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
            return map;
        }

        public MapGen()
        {
            rand = new Random();
            map = new bool[mapWidth, mapHeight];
            hunterCoords = getRandomCoord();
        }
    }
}