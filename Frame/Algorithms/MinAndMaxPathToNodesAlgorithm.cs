using MSAGL.Frame.Models;
using MSAGL.Frame.Utility;
using Microsoft.Msagl.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace MSAGL.Frame.Algorithms
{
    class MinAndMaxPathToNodesAlgorithm : Algorithm<MinAndMaxPathToNodesAlgorithm>, IAlgorithm
    {
        public AlgorithmGraphType GraphType => AlgorithmGraphType.Directed;

        public string Name => "Минимальный и максимальный путь до всех вершин";

        private Dictionary<Node, double> BellmanFord(DGraph graph, Node start, bool minPath)
        {
            Dictionary<Node, double> distance = new Dictionary<Node, double>();
            Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
            distance.Add(start, 0);
            for(int i = 0; i < graph.Nodes.Count(); ++i)
            {
                if (graph.Nodes.ElementAt(i).Id != start.Id)
                {
                    distance.Add(graph.Nodes.ElementAt(i), double.PositiveInfinity);
                    previous.Add(graph.Nodes.ElementAt(i), null);
                }
            }
            for(int i = 0; i < graph.Nodes.Count() - 1; ++i)
                for(int j = 0; j < graph.Edges.Count(); ++j)
                {
                    if(distance[graph.Edges.ElementAt(j).SourceNode] +(
                        graph.Edges.ElementAt(j).Weight() * (minPath ? 1 : -1)) < 
                        distance[graph.Edges.ElementAt(j).TargetNode])
                    {
                        distance[graph.Edges.ElementAt(j).TargetNode] =
                            distance[graph.Edges.ElementAt(j).SourceNode] + (
                        graph.Edges.ElementAt(j).Weight() * (minPath ? 1 : -1));
                        previous[graph.Edges.ElementAt(j).TargetNode] = graph.Edges.ElementAt(j).SourceNode;
                    }
                }
            return distance;
        }
        private bool hasNegativeCycle(Dictionary<Node, double> distances, DGraph graph, bool minPath)
        {
            for (int j = 0; j < graph.Edges.Count(); ++j)
                if (distances[graph.Edges.ElementAt(j).SourceNode] + 
                    (graph.Edges.ElementAt(j).Weight() * (minPath ? 1 : -1)) <
                    distances[graph.Edges.ElementAt(j).TargetNode])
                        return false;
            return true;
        }
        public Dictionary<Node, double> Execute(DGraph graph, Node start, bool minPath = true)
        {
            var result = BellmanFord(graph, start, minPath);
            if (!hasNegativeCycle(result, graph, minPath))
                return null;
            else
            {
                if (!minPath)
                    foreach (Node n in graph.Nodes)
                        result[n] *= -1;
                return result;
            }
        }
    }
}
