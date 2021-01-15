using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MSAGL.Frame.Models
{
    public class DGraph : Graph
    {
        public DGraph() { }
        public DGraph(ArrowStyle edgeArrowStyle)
        {
            this.EdgeArrowStyle = edgeArrowStyle;
        }
        public void AddEdge(string source, string target, object weight)
        {
            /*Node a = new Node(source), b = new Node(target);
            a.Attr.Shape = b.Attr.Shape = Shape.Circle;
            base.AddNode(a);
            base.AddNode(b);*/
            //var e = base.AddEdge(source, weight.ToString(), target);
            Node n1 = base.Nodes.First(n => n.Id == source), n2 = base.Nodes.First(n => n.Id == target);
            n1.Attr.Shape = n2.Attr.Shape = Shape.Circle;
            Edge e = new Edge(n1, n2, ConnectionToGraph.Connected);
            e.UserData = e.LabelText = weight.ToString();
            e.Attr.ArrowheadAtTarget = this.EdgeArrowStyle;
            e.Label.FontSize = 8;
            base.AddPrecalculatedEdge(e);
        }
        public void AddVertice(string nodeID)
        {
            Node n = base.AddNode(nodeID);
            n.Attr.Shape = Shape.Circle;
            n.UserData = nodeID;
        }
        public void ClearGraph()
        {
            while (base.Edges.Count() > 0)
                base.RemoveEdge(base.Edges.Last());
            while (base.Nodes.Count() > 0)
                base.RemoveNode(base.Nodes.ElementAt(base.Nodes.Count() - 1));
        }
        public bool isEmpty() { return base.Nodes.Count() + base.Edges.Count() == 0 ? true : false; }
        public ArrowStyle EdgeArrowStyle { get; set; }
    }
}
