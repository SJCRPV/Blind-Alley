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
        Random rand;
        bool[,] map;
        int[] objectiveCoords;
        int[] playerCoords;
        int[] monsterCoords;
        int mapWidth;
        int mapHeight;

        private int[] getRandomCoord()
        {
            return new int[] { rand.Next(mapWidth), rand.Next(mapHeight) };
        }

        private void createDoor()
        {

        }

        private void createWall(int startIndex, int endIndex)
        {

        }

        private void divideArea(int[] topLeft, int[] botRight)
        {
            createWall();
            createDoor();
        }

        private void generateMap()
        {
            divideArea();
        }

        public Map(int nMapWidth, int nMapHeight)
        {
            rand = new Random();
            map = new bool[nMapWidth, nMapHeight];
            generateMap();
            playerCoords = new int[] { 0, 0 };
            objectiveCoords = getRandomCoord();
            monsterCoords = (int[])objectiveCoords.Clone();
        }
    }
}