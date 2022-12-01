using BehaviorTree;

public class CheckTargetIsResource : Node
{
    public override NodeState Evaluate()
    {
        bool targetIsResource = (bool)Root.GetData("target_is_resource");
        _state = targetIsResource ? NodeState.SUCCESS : NodeState.FAILURE;
        return _state;
    }
}
