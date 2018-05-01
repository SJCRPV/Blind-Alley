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
        MapGen mapGen;
        bool[,] map;
        int[] objectiveCoords;
        int[] playerCoords;
        int[] monsterCoords;

        public void moveMonster()
        {

        }

        public Map(int nMapWidth, int nMapHeight)
        {
            mapGen = new MapGen(nMapWidth, nMapHeight);
            map = mapGen.generateMap();
            playerCoords = new int[] { 0, 0 };
            objectiveCoords = mapGen.getRandomCoord();
            monsterCoords = (int[])objectiveCoords.Clone();
        }
    }
}