using System.Collections.Generic;

namespace BehaviorTree
{
    public abstract class Subtree
    {
        public abstract Node GetTree(object[] data);

        public virtual Dictionary<string, System.Action> GetCallbacks(object[] data)
        {
            return new Dictionary<string, System.Action>();
        }
    }
}
