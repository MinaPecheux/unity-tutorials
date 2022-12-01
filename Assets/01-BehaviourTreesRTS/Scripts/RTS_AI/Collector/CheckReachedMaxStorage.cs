using BehaviorTree;

public class CheckReachedMaxStorage : Node
{
    private int _maxStorage;

    public CheckReachedMaxStorage(int maxStorage)
    {
        _maxStorage = maxStorage;
    }

    public override NodeState Evaluate()
    {
        int a = (int)Root.GetData("current_resource_amount");
        _state = a == _maxStorage ? NodeState.SUCCESS : NodeState.FAILURE;
        return _state;
    }
}
