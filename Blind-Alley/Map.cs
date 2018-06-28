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
    abstract class Map
    {
        protected static Random rand;
        protected static bool[,] map;
        protected static List<Node> nodeMap;
        protected static int mapHeight;
        protected static int mapWidth;
        protected static int[] objectiveCoords;

        public static int[] ObjectiveCoords
        {
            get => objectiveCoords;
        }

        public static bool getValAt(int[] coords)
        {
            try
            {
                return map[coords[0], coords[1]];
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
        
        public static Node getNodeAt(int[] coords)
        {
            //(Number of rows - 1) * (the width of each) + the remainder 
            return nodeMap[(coords[1] - 1) * mapWidth + coords[0]];
        }

        public static int[] getRandomCoord()
        {
            return new int[] { rand.Next(mapWidth), rand.Next(mapHeight) };
        }

        protected static int[][] getNeighbours(int[] coords, bool wantClosest)
        {
            int distance = wantClosest ? 1 : 2;
            return new int[][] { new int[] { coords[0], coords[1] - distance }, new int[] { coords[0] - distance, coords[1] }, new int[] { coords[0], coords[1] + distance }, new int[] { coords[0] + distance, coords[1] } };
        }

        public static int? getRandomDirection(int[] coords, bool isItWalkable)
        {
            int[][] neighbours;
            if(isItWalkable)
            {
                neighbours = getNeighbours(coords, true);
            }
            else
            {
                neighbours = getNeighbours(coords, false);
            }
            List<int> possibleDirections = new List<int>();
            for (int i = 0; i < neighbours.Length; i++)
            {
                try
                {
                    int[] neighbour = neighbours[i];
                    if (map[neighbour[0], neighbour[1]] == isItWalkable)
                    {
                        possibleDirections.Add(i);
                    }
                }
                catch (IndexOutOfRangeException) { }
            }

            if (possibleDirections.Count() == 0)
            {
                return null;
            }
            return possibleDirections[rand.Next(possibleDirections.Count())];
        }
    }
}