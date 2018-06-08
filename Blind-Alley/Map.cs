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

        public Map(int nMapWidth, int nMapHeight)
        {
            mapGen = new MapGen(nMapWidth, nMapHeight);
            map = mapGen.generateMap();
            mapGen.placeRelevantPieces();
            objectiveCoords = mapGen.getRandomCoord();
            monsterCoords = (int[])objectiveCoords.Clone();
        }
    }
}