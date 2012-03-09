using System;
using System.Collections.Generic;
using System.Linq;

namespace ADT
{
    public class Graph
    {
        //Event
        public event EventHandler<GraphStateChangeEventArgs> GraphChangedEvent;

        //Properties
        public List<Node> Nodes { get; private set; }
        public List<Edge> Edges { get; private set; }

        //Constructors
        public Graph() : this(null, null) { }

        //pre: nodes & edges contain no duplicates
        public Graph(List<Node> nodes, List<Edge> edges)
        {
            this.Nodes = (nodes == null) ? new List<Node>() : nodes;
            this.Edges = (edges == null) ? new List<Edge>() : edges;
        }

        //Public methods
        public void AddNode(Node n)
        {
            n.NodeChangedEvent += new EventHandler<EventArgs>(NodeChangedEvent);
            Nodes.Add(n);
        }

        public void AddEdge(Edge e)
        {
            //check the edge doesn't alreay exist.
            if (!Edges.Contains(e, new EdgeComparer()))
            {
                e.EdgeChangedEvent += new EventHandler<EventArgs>(EdgeChangedEvent);
                Edges.Add(e);
            }
        }

        //Raise Events
        private void NodeChangedEvent(object sender, EventArgs e)
        {
            Node n = sender as Node;
            if (n != null)
            {
                OnGraphChanged(n);
            }
        }

        private void OnGraphChanged(Node n)
        {
            if (GraphChangedEvent != null)
            {
                GraphChangedEvent(this, new GraphStateChangeEventArgs() { ChangedNode = n, ChangedEdge = null });
            }
        }

        private void EdgeChangedEvent(object sender, EventArgs e)
        {
            Edge edge = sender as Edge;
            if (edge != null)
            {
                OnGraphChanged(edge);
            }
        }

        private void OnGraphChanged(Edge edge)
        {
            if (GraphChangedEvent != null)
            {
                GraphChangedEvent(this, new GraphStateChangeEventArgs() { ChangedNode = null, ChangedEdge = edge });
            }
        }
        
    }

    public class GraphStateChangeEventArgs : EventArgs
    {
        public Node ChangedNode { get; set; }
        public Edge ChangedEdge { get; set; }
    }

    public class EdgeComparer : IEqualityComparer<Edge>
    {

        #region IEqualityComparer<Edge> Members

        public bool Equals(Edge x, Edge y)
        {
            return ((x.Node1.Value.Equals(y.Node1.Value) && x.Node2.Value.Equals(y.Node2.Value)) || (x.Node1.Value.Equals(y.Node2.Value) && x.Node2.Value.Equals(y.Node1.Value)));
        }

        public int GetHashCode(Edge obj)
        {
            return Int32.Parse(obj.Node1.Value).ToString().GetHashCode() + Int32.Parse(obj.Node2.Value).ToString().GetHashCode();
        }

        #endregion
    }
}
