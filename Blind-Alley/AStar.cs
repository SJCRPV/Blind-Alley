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
    class AStar : Map
    {
        // Taking from here https://www.codeproject.com/Articles/1221034/Pathfinding-Algorithms-in-Csharp

        //private void BuildShortestPath(List<Node> list, Node node)
        //{
        //    if (node.NearestToStart == null)
        //        return;
        //    list.Add(node.NearestToStart);
        //    BuildShortestPath(list, node.NearestToStart);
        //}

        //public List<Node> GetShortestPathAstar()
        //{
        //    foreach (var node in Map.Nodes)
        //        node.StraightLineDistanceToEnd = node.StraightLineDistanceTo(End);
        //    AstarSearch();
        //    var shortestPath = new List<Node>();
        //    shortestPath.Add(End);
        //    BuildShortestPath(shortestPath, End);
        //    shortestPath.Reverse();
        //    return shortestPath;
        //}

        private void search()
        {
            Node start = getNodeAt(Player.PlayerCoords);
            start.MinCostToStart = 0;
            List<Node> processQueue = new List<Node>();
            processQueue.Add(start);

            do
            {
                processQueue = processQueue.OrderBy(n => n.MinCostToStart + n.calcDistToEnd()).ToList();
                Node node = processQueue.First();
                foreach( int[] connection in node.getWalkableNeighbours())
                {
                    Node childNode = 
                }
            } while (processQueue.Any());
        }

        //private void AstarSearch()
        //{
        //    Start.MinCostToStart = 0;
        //    var prioQueue = new List<Node>();
        //    prioQueue.Add(Start);
        //    do
        //    {
        //        prioQueue = prioQueue.OrderBy(x => x.MinCostToStart + x.StraightLineDistanceToEnd).ToList();
        //        var node = prioQueue.First();
        //        prioQueue.Remove(node);
        //        NodeVisits++;
        //        foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
        //        {
        //            var childNode = cnn.ConnectedNode;
        //            if (childNode.Visited)
        //                continue;
        //            if (childNode.MinCostToStart == null || node.MinCostToStart + cnn.Cost < childNode.MinCostToStart)
        //            {
        //                childNode.MinCostToStart = node.MinCostToStart + cnn.Cost;
        //                childNode.NearestToStart = node;
        //                if (!prioQueue.Contains(childNode))
        //                    prioQueue.Add(childNode);
        //            }
        //        }
        //        node.Visited = true;
        //        if (node == End)
        //            return;
        //    } while (prioQueue.Any());
        //}
    }
}