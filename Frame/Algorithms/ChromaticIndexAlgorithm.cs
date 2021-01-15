using MSAGL.Frame.Models;
using Microsoft.Msagl.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MSAGLColor = Microsoft.Msagl.Drawing.Color;
using System;

namespace MSAGL.Frame.Algorithms
{
    class ChromaticIndexAlgorithm : Algorithm<ChromaticIndexAlgorithm>, IAlgorithm
    {
        public AlgorithmGraphType GraphType => AlgorithmGraphType.Any;

        public string Name => "Хроматический индекс";

        public Tuple<int, Dictionary<Edge, MSAGLColor>> Execute(DGraph graph)
        {
            Tuple<int, Dictionary<Edge, MSAGLColor>> result;
            Dictionary<Edge, MSAGLColor> edgeColorMap = new Dictionary<Edge, MSAGLColor>();
            var independentSets = FindIndependentSets(graph);
            List<Tuple<byte, byte, byte>> usedColors = new List<Tuple<byte, byte, byte>>();
            Random rand = new Random();
            Tuple<byte, byte, byte> randomColor;
            foreach (List<Node> set in independentSets)
            {
                foreach(Node n in set)
                {
                    foreach(Edge nEdge in n.Edges)
                    {
                        do
                        {
                            randomColor =
                                new Tuple<byte, byte, byte>((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));
                        } while (usedColors.Contains(randomColor));
                        
                        MSAGLColor currentColor = new MSAGLColor(randomColor.Item1, randomColor.Item2, randomColor.Item3);
                        if (!edgeColorMap.ContainsKey(nEdge))
                        {
                            edgeColorMap.Add(nEdge, currentColor);
                            usedColors.Add(randomColor);
                        }
                        else
                            currentColor = edgeColorMap[nEdge];
                        foreach (Node m in set)
                        {
                            if(n != m)
                            {
                                Edge mEdge = null;
                                try
                                {
                                    mEdge = m.Edges.First(edge => !edgeColorMap.ContainsKey(edge) && !nEdge.isAdjasentTo(edge));
                                }
                                catch { }
                                finally
                                {
                                    if (mEdge != null && edgeColorMap.Count(pair => nEdge.isAdjasentTo(mEdge)) == 0 &&
                                        edgeColorMap.Count(pair => pair.Key.isAdjasentTo(mEdge) && pair.Value == currentColor) == 0)
                                        edgeColorMap.Add(mEdge, currentColor);
                                }
                            }
                        }
                    }
                }
            }
            var remainingEdges = graph.Edges.Where(edge => !edgeColorMap.ContainsKey(edge));
            foreach (Edge e in remainingEdges)
            {
                do
                {
                    randomColor =
                        new Tuple<byte, byte, byte>((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));
                } while (usedColors.Contains(randomColor));
                usedColors.Add(randomColor);
                edgeColorMap.Add(e, new MSAGLColor(randomColor.Item1, randomColor.Item2, randomColor.Item3));
            }
            result = new Tuple<int, Dictionary<Edge, MSAGLColor>>(usedColors.Count, edgeColorMap);
            return result;
        }

        private List<List<Node>> DistinctResult(List<List<Node>> oldResult)
        {
            for (int i = 0; i < oldResult.Count; i++)
            {
                for (int k = 0; k < oldResult.Count; k++)
                {
                    if (i != k)
                        if (oldResult.ElementAt(i).Intersect(oldResult.ElementAt(k)).SequenceEqual(oldResult.ElementAt(i)))
                        {
                            oldResult.RemoveAt(k);
                                k--;
                            if(i > 0)
                                i--;
                        }
                }
            }
            return oldResult.OrderByDescending(r => r.Count).ToList();
        }

        public List<List<Node>> FindIndependentSets(DGraph graph)
        {
            List<List<Node>> result = new List<List<Node>>();
            foreach(Node n in graph.Nodes)
            {
                List<Node> independent = new List<Node>();
                independent.Add(n);
                foreach (Node m in graph.Nodes)
                    if (n != m )
                        if (!n.isAdjacentTo(m))
                        {
                            bool isIndependent = true;
                            foreach(Node k in independent)
                                if(m.isAdjacentTo(k))
                                {
                                    isIndependent = false;
                                    break;
                                }
                            if(isIndependent)
                                independent.Add(m);
                        }
                if(independent.Count > 1)
                    result.Add(independent);
            }
            return DistinctResult(result);
        }
    }
}
