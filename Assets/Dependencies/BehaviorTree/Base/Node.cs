using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node
    {
        public static uint LastId = 0;

        protected NodeState _state;
        public NodeState State { get => _state; }

        private uint _id;
        public uint Id => _id;

        private Node _root;
        protected Node Root => _root;

        private Node _parent;
        protected List<Node> children = new List<Node>();
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        public Dictionary<string, object> Data => _dataContext;
        protected Dictionary<string, System.Action> _callbacks;
        public Dictionary<string, System.Action> Callbacks => _callbacks;

        public Node Parent => _parent;
        public List<Node> Children => children;
        public bool HasChildren => children.Count > 0;

        public Node()
        {
            _id = LastId++;
            _parent = null;
            _root = this;
        }
        public Node(List<Node> children) : this()
        {
            SetChildren(children);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetChildren(List<Node> children, bool forceRoot = false)
        {
            foreach (Node c in children)
                Attach(c);
            if (forceRoot)
                SetRoot(this);
        }

        public void Attach(Node child)
        {
            children.Add(child);
            child._parent = this;
            child._root = _root;
        }

        public void Detach(Node child)
        {
            children.Remove(child);
            child._parent = null;
            child._root = null;
        }

        public void SetRoot(Node root)
        {
            _root = root;
            foreach (Node child in children)
                child.SetRoot(root);
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = _parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node._parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = _parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node._parent;
            }
            return false;
        }

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public void SetCallbacks(Dictionary<string, System.Action> callbacks)
        {
            _callbacks = callbacks;
        }

        public void AddCallbacks(Dictionary<string, System.Action> callbacks)
        {
            if (_callbacks == null)
                _callbacks = new Dictionary<string, System.Action>();
            foreach (var p in callbacks) _callbacks.Add(p.Key, p.Value);
        }
    }
}
