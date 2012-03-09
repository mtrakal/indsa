using System;

namespace ADT
{
    public class Node
    {
        //Event
        public event EventHandler<EventArgs> NodeChangedEvent;

        //Properties
        public String Value {get;private set;}
        public double X { get; set; }
        public double Y { get; set; }

        private NodeStates _state;
        public NodeStates State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnStateChanged();
            }
        }

        //Constructor
        public Node(String value, double x, double y)
        {
            Value = value;
            X = x;
            Y = y;
            State = NodeStates.Unactive;
        }

        //Raise Event
        protected void OnStateChanged()
        {
            if (NodeChangedEvent != null)
            {
                NodeChangedEvent(this, EventArgs.Empty);
            }
        }
    }

    public enum NodeStates
    {
        Unactive = 0,
        Active = 1,
        Neighbour = 2,
        Selected = 3,
        Chosen = 4
    }
}
