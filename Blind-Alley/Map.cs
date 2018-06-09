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
    class Map
    {
        static bool[,] map;
        static float minDistanceToPlayer;
        MapGen mapGen;
        int[] objectiveCoords;
        int[] monsterCoords;

        public static bool getAt(int[] coords)
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

        public void moveMonster()
        {

        }

        private bool farEnoughFromPlayer(int[] coords)
        {
            return Math.Abs(Player.PlayerCoords[0] - coords[0]) + Math.Abs(Player.PlayerCoords[1] - coords[1]) > minDistanceToPlayer;
        }

        public void placeRelevantPieces()
        {
            while(!farEnoughFromPlayer(objectiveCoords))
            {
                objectiveCoords = mapGen.getRandomCoord();
            }
        }

        public Map(int nMapWidth, int nMapHeight)
        {
            mapGen = new MapGen(nMapWidth, nMapHeight);
            map = mapGen.generateMap();
            objectiveCoords = mapGen.getRandomCoord();
            monsterCoords = (int[])objectiveCoords.Clone();
            placeRelevantPieces();
            minDistanceToPlayer = (nMapWidth / 2) + (nMapHeight / 2);
        }
    }
}