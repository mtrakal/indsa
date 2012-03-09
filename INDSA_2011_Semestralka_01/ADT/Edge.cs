using System;

namespace ADT
{
    public class Edge
    {
        //Event
        public event EventHandler<EventArgs> EdgeChangedEvent;
        
        //Properties
        public Node Node1 { get; private set; }
        public Node Node2 { get; private set; }
        public int Cost { get; set; }

        private EdgeStates _state;
        public EdgeStates State 
        {
            get { return _state; }
            set
            {
                _state = value;
                OnStateChanged();
            }
        }

        //Constructor
        public Edge(Node n1, Node n2, int cost)
        {
            Node1 = n1;
            Node2 = n2;
            Cost = cost;
            State = EdgeStates.Unactive;
        }

        //Raise event
        public void OnStateChanged()
        {
            if (EdgeChangedEvent != null)
            {
                EdgeChangedEvent(this, EventArgs.Empty);
            }
        }

    }

    public enum EdgeStates
    {
        Unactive = 0,
        Active = 1,
        Selected = 2,
        Chosen = 3
    }
}
