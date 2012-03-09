using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ADT {
    public class Dijkstra {
        private Graph graph;
        private Dictionary<String, int> dist;
        private Dictionary<String, Node> previous;
        private List<Node> shortestPath;

        public Dijkstra(Graph g) {
            graph = g;
            dist = new Dictionary<string, int>();
            previous = new Dictionary<string, Node>();
            shortestPath = new List<Node>();
        }

        public List<Node> FindShortestPath(String source, String target) {
            foreach (Node n in graph.Nodes) {
                dist.Add(n.Value, Int32.MaxValue);
                previous.Add(n.Value, null);
                n.State = NodeStates.Unactive;
            }
            dist[source] = 0;

            foreach (Edge e in graph.Edges) {
                e.State = EdgeStates.Unactive;
            }

            List<Node> Q = new List<Node>(graph.Nodes);

            while (Q.Count > 0) {
                Node u = GetSmallestDist(Q);
                u.State = NodeStates.Active;

                if (dist[u.Value] == Int32.MaxValue)
                    break;

                Q.Remove(u);

                if (u.Value == target)
                    break;

                List<Node> neighbours = GetNeighbouringNodes(u);

                foreach (Node v in neighbours) {
                    Edge e = graph.Edges.Where(p => (p.Node1.Value == u.Value && p.Node2.Value == v.Value) || (p.Node2.Value == u.Value && p.Node1.Value == v.Value)).FirstOrDefault();

                    e.State = EdgeStates.Active;
                    v.State = NodeStates.Neighbour;

                    int pathLength = dist[u.Value] + e.Cost;
                    if (pathLength < dist[v.Value]) {
                        dist[v.Value] = pathLength;
                        previous[v.Value] = u;

                        e.State = EdgeStates.Selected;
                        v.State = NodeStates.Selected;
                    }

                    e.State = EdgeStates.Unactive;
                    v.State = NodeStates.Unactive;
                }

                u.State = NodeStates.Unactive;
            }

            Node targetNode = graph.Nodes.Where(p => p.Value == target).FirstOrDefault();
            while (previous[targetNode.Value] != null) {
                shortestPath.Insert(0, targetNode);
                targetNode.State = NodeStates.Chosen;
                targetNode = previous[targetNode.Value];
            }
            Node sourceNode = graph.Nodes.Where(p => p.Value == source).FirstOrDefault();
            sourceNode.State = NodeStates.Chosen;
            shortestPath.Insert(0, sourceNode);

            //Choose edges
            for (int i = 0; i < shortestPath.Count - 1; i++) {
                Edge edge = graph.Edges.Where(p => (p.Node1.Value.Equals(shortestPath[i].Value) && p.Node2.Value.Equals(shortestPath[i + 1].Value)) ||
                   (p.Node2.Value.Equals(shortestPath[i].Value) && p.Node1.Value.Equals(shortestPath[i + 1].Value))).FirstOrDefault();
                edge.State = EdgeStates.Chosen;
            }

            return shortestPath;
        }

        private List<Node> GetNeighbouringNodes(Node n) {
            List<Node> neighbours = new List<Node>();
            foreach (Edge e in graph.Edges) {
                if (e.Node1 == n) {
                    neighbours.Add(e.Node2);
                } else if (e.Node2 == n) {
                    neighbours.Add(e.Node1);
                }
            }

            return neighbours;
        }

        private Node GetSmallestDist(List<Node> Q) {
            int minDist = Int32.MaxValue;
            Node minNode = null;

            foreach (Node n in Q) {
                if (dist[n.Value] <= minDist) {
                    minDist = dist[n.Value];
                    minNode = n;
                }
            }

            return minNode;
        }
    }
}


