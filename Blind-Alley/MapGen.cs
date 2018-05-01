﻿using System;
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
        int[] hunterCoords;
        int mapWidth;
        int mapHeight;

        private void printMap()
        {
            for(int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    if (map[i, j])
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write('=');
                    }
                }
                Console.WriteLine();
            }
        }

        public int[] getRandomCoord()
        {
            return new int[] { rand.Next(mapWidth), rand.Next(mapHeight) };
        }
        private int getRandomDirection()
        {
            return rand.Next(3);
        }

        private bool doesItHaveVisitedNeighbours(int[] coords)
        {
            //TODO: Check if this has any neighbours that have been visited
            return false;
        }

        private void hunt()
        {
            //TODO: Scan each row to see if there are cells that haven't been visited AND have neighbours that have. Set the hunter's coords to that when it happens and go back to walking
            for(int i = 0, j = 0; i < mapHeight && j < mapWidth; j++)
            {
                if (map[i, j])
                {
                    continue;
                }
                else
                {

                }

                if(j == mapWidth)
                {
                    i++;
                    j = 0;
                }
            }
        }

        private void walk(int[] startCoords)
        {
            int direction = getRandomDirection();
            int hunXCoord = hunterCoords[0];
            int hunYCoord = hunterCoords[1];
            bool hasToHunt = false;
            switch (direction)
            {
                // NORTH
                case 0:
                    hasToHunt = (map[hunXCoord, hunYCoord - 2] || hunYCoord - 2 < 0);

                    if(!hasToHunt)
                    {
                        map[hunXCoord, hunYCoord - 2] = true;
                    }
                    break;

                // WEST
                case 1:
                    hasToHunt = (map[hunXCoord - 2, hunYCoord] || hunXCoord - 2 < 0);

                    if(!hasToHunt)
                    {
                        map[hunXCoord - 2, hunYCoord] = true;
                    }
                    break;

                // SOUTH
                case 2:
                    hasToHunt = (map[hunXCoord, hunYCoord + 2] || hunYCoord + 2 > mapHeight);
                    
                    if(!hasToHunt)
                    {
                        map[hunXCoord, hunYCoord + 2] = true;
                    }
                    break;

                // EAST
                case 3:
                    hasToHunt = (map[hunXCoord + 2, hunYCoord] || hunXCoord + 2 > mapWidth);

                    if(!hasToHunt)
                    {
                        map[hunXCoord + 2, hunYCoord] = true;
                    }
                    break;

                // ERROR
                default:
                    Console.WriteLine("ERROR: I got the value " + direction + " and I don't know what to do with it.");
                    break;
            }

            if (hasToHunt)
            {
                hunt();
            }
        }

        public bool[,] generateMap()
        {
            bool running = true;
            while (running)
            {
                walk(hunterCoords);
            }

            return map;
        }

        public MapGen(int nMapWidth, int nMapHeight)
        {
            rand = new Random();
            mapWidth = nMapWidth;
            mapHeight = nMapHeight;
            map = new bool[mapWidth, mapHeight];
            int[] hunterCoords = getRandomCoord();
        }
    }
}