using System;
using System.Collections.Generic;
using System.Linq;
using MSAGL.Frame.Models;
using MSAGL.Frame.Utility;
using Microsoft.Msagl.Drawing;


namespace MSAGL.Frame.Algorithms
{
    public class MaxFlowAlgorithm : Algorithm<MaxFlowAlgorithm>, IAlgorithm
    {
        public AlgorithmGraphType GraphType => AlgorithmGraphType.Directed;
        public string Name => "Максимальный поток методом проталкивания";

        private Dictionary<Edge, double> edgeFullness;
        private Dictionary<Node, double> nodeFullness;
        private Dictionary<Node, double> nodeHeight;


        private MaxFlowAlgorithm()
        {
            edgeFullness = new Dictionary<Edge, double>();
            nodeFullness = new Dictionary<Node, double>();
            nodeHeight = new Dictionary<Node, double>();
        }
        public Dictionary<Edge, double> Execute(DGraph graph, Node source, Node sink)
        {
            Init(graph, source);
            bool hasOveflowedNodes = true;
            while(hasOveflowedNodes)
            {
                foreach (Node n in graph.Nodes)
                    if (!(n == source || n == sink) && this.nodeFullness[n] > 0)
                    {
                        if (!TryFlowPush(n))
                            LiftNode(n);
                    }
                if (nodeFullness.Count(pair => !(pair.Key == source || pair.Key == sink) && pair.Value != 0) == 0)
                    hasOveflowedNodes = false;
            }
            return this.edgeFullness;

        }
        private void Init(DGraph graph, Node source)
        {
            this.edgeFullness.Clear();
            this.nodeFullness.Clear();
            this.nodeHeight.Clear();

            foreach (Node n in graph.Nodes)
            {
                nodeHeight.Add(n, 0);
                nodeFullness.Add(n, 0);
            }
            nodeHeight[source] = graph.Nodes.Count();


            foreach (Edge e in graph.Edges)
                edgeFullness.Add(e, 0);

            //переполняем нахер
            foreach (Edge e in source.Edges)
            {
                Node outNode = e.TargetNode == source ? e.SourceNode : e.TargetNode;
                this.nodeFullness[outNode] = e.Weight();
                edgeFullness[e] = e.Weight();
            }
        }

        private bool isSubNetEdge(Edge e) { return e.Weight() > this.edgeFullness[e]; }

        private bool TryFlowPush(Node n)
        {
            bool result = false;
            foreach(Edge e in n.OutEdges)
                if (isSubNetEdge(e) && this.nodeHeight[n] - this.nodeHeight[e.TargetNode] == 1)
                {
                    result = true;
                    double excessFlow = Math.Min(this.nodeFullness[n], e.Weight());
                    this.edgeFullness[e] += excessFlow;
                    this.nodeFullness[n] -= excessFlow;
                    this.nodeFullness[e.TargetNode] += excessFlow;
                }
            foreach(Edge e in n.Edges)
            {
                Node outNode = e.SourceNode == n ? e.TargetNode : e.SourceNode;
                if (this.edgeFullness[e] != 0 && this.nodeHeight[n] - this.nodeHeight[outNode] == 1)
                {
                    result = true;
                    double excessFlow = Math.Min(this.nodeFullness[n], e.Weight());
                    this.edgeFullness[e] -= excessFlow;
                    this.nodeFullness[outNode] += excessFlow;
                    this.nodeFullness[n] -= excessFlow;
                }
            }
            return result;
        }
        private bool LiftNode(Node n)
        {
            double minDelta = double.PositiveInfinity;
            double delta;

            foreach(Edge e in n.OutEdges)
            {
                delta = this.nodeHeight[e.TargetNode] - this.nodeHeight[n];
                if (delta >= 0 && delta < minDelta)
                    minDelta = delta;
            }

            foreach (Edge e in n.Edges)
            {
                Node outNode = e.SourceNode == n ? e.TargetNode : e.SourceNode;
                delta = this.nodeHeight[outNode] - this.nodeHeight[n];
                if (delta >= 0 && delta < minDelta)
                    minDelta = delta;
            }

            if (minDelta == double.PositiveInfinity)
                return false;
            else
            {
                this.nodeHeight[n] += minDelta + 1;
                return true;
            }
        }
    }
}
