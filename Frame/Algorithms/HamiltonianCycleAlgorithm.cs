using MSAGL.Frame.Models;
using Microsoft.Msagl.Drawing;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MSAGL.Frame.Algorithms
{
    public class HamiltonianCycleAlgorithm : Algorithm<HamiltonianCycleAlgorithm>, IAlgorithm
    {
        public AlgorithmGraphType GraphType => AlgorithmGraphType.Undirected;

        public string Name => "Гамильтонов цикл";
        private bool? isHamiltonian = null;
        private void DFS(Node current, Dictionary<Node, bool> nodesMet, ref Stack<Node> path, ref Stack<Edge> pathEdges)
        {
            nodesMet[current] = true;
            foreach (Edge e in current.Edges)
            {
                var nextNode = e.Source == current.Id ? e.TargetNode : e.SourceNode;
                if (!nodesMet[nextNode])
                {
                    pathEdges.Push(e);
                    path.Push(nextNode);
                    DFS(nextNode, nodesMet, ref path, ref pathEdges);
                    if (this.isHamiltonian != null)
                        return;
                }
            }
            if (nodesMet.All(n => n.Value == true))
            {
                foreach (Edge e in current.Edges)
                    if (!pathEdges.Contains(e))
                        if (e.Target == path.ElementAt(path.Count - 1).Id || e.Source == path.ElementAt(path.Count - 1).Id)
                        {
                            pathEdges.Push(e);
                            this.isHamiltonian = true;
                            return;
                        }
                this.isHamiltonian = false;
            }
            if (nodesMet[current] && path.Count > 0)
            {
                path.Pop();
                if(pathEdges.Count > 0)
                    pathEdges.Pop();
                nodesMet[current] = false;
                return;
            }
        }
        public Tuple<Stack<Node>, Stack<Edge>> Execute(DGraph graph)
        {
            Dictionary<Node, bool> nodesMet = new Dictionary<Node, bool>(graph.Nodes.Count());
            Stack<Node> path = new Stack<Node>(graph.Nodes.Count());
            Stack<Edge> pathEdges = new Stack<Edge>();
            foreach (Node n in graph.Nodes)
                nodesMet.Add(n, false);
            foreach (Node node in graph.Nodes)
            {
                foreach (Node n in graph.Nodes)
                    nodesMet[n] = false;
                this.isHamiltonian = null;
                path.Clear();
                path.Push(node);
                DFS(node, nodesMet, ref path, ref pathEdges);
                if (isHamiltonian != null && isHamiltonian == true)
                    return new Tuple<Stack<Node>, Stack<Edge>>(path, pathEdges);
            }
            return null;
        }
    }
}
