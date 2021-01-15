using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Drawing;
using MSAGL.Frame.Models;

namespace MSAGL.Frame.Algorithms
{
    class MaxCliqueAlgorithm : Algorithm<MaxCliqueAlgorithm>, IAlgorithm
    {
        public AlgorithmGraphType GraphType => AlgorithmGraphType.Undirected;

        public string Name => "Максимальная клика";

        public List<List<Node>> Execute(DGraph graph)
        {
            List<Node> currentClique;
            List<List<Node>> result = new List<List<Node>>();
            //foreach(Node maxDegreeNode in graph.Nodes.Where(n => n.InEdges.Count() 
            //== graph.Nodes.Max(n => n.InEdges.Count())))
            foreach(Node maxDegreeNode in graph.Nodes.OrderBy(n => n.Edges.Count()))
            {
                currentClique = new List<Node>();
                currentClique.Add(maxDegreeNode);
                //foreach (Node current in graph.Nodes.Where(n => n != maxDegreeNode))
                foreach(Node current in graph.Nodes.Where(n => n != maxDegreeNode))
                    if (maxDegreeNode.isAdjacentTo(current) &&
                        maxDegreeNode.AdjacentNodes.Intersect(current.AdjacentNodes).Count() > 0)
                    {
                        int adjacencyCount = 0;
                        foreach (Node n in currentClique)
                            if (current.isAdjacentTo(n))
                                adjacencyCount++;
                        if(adjacencyCount == currentClique.Count)
                            currentClique.Add(current);
                    }
                if(currentClique.Count > 2)
                    result.Add(currentClique);
            }
            return result;
        }
    }
}
