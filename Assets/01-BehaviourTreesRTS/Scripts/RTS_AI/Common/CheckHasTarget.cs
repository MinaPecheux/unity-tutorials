using BehaviorTree;

public class CheckHasTarget : Node
{
    public override NodeState Evaluate()
    {
        _state = Root.GetData("target") == null
            ? NodeState.FAILURE : NodeState.SUCCESS;
        return _state;
    }
}
