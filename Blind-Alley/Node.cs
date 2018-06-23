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
    class Node : Map
    {
        private int[] coords;
        private int[][] connections;
        private int minCostToStart;
        private double distToEnd;

        public int[] Coords { get => coords; }
        public int MinCostToStart { get => minCostToStart; set => minCostToStart = value; }

        public bool getMapVal()
        {
            return getValAt(coords);
        }

        public double calcDistToEnd()
        {
            if(distToEnd != -1)
            {
                return distToEnd;
            }

            //√((x2 - x1)2 + (y2 - y1)2
            distToEnd = Math.Sqrt(Math.Pow(Player.PlayerCoords[0] - coords[0], 2) + Math.Pow(Player.PlayerCoords[1] - coords[1], 2));
            return distToEnd;
        }

        public int[][] getWalkableNeighbours()
        {
            if(connections != null)
            {
                return connections;
            }

            List<int[]> neighbours = getNeighbours(coords, true).ToList();

            for(int i = 0; i < neighbours.Count; i++)
            {
                if(!getValAt(neighbours[i]))
                {
                    neighbours.RemoveAt(i);
                }
            }

            connections = neighbours.ToArray();

            return connections;
        }

        public Node(int[] nCoords)
        {
            coords = (int[])nCoords.Clone();
        }
    }
}