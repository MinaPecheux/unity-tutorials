using BehaviorTree;

public class TaskDeliver : Node
{
    private string _resourceType;

    public TaskDeliver(string resourceType)
    {
        _resourceType = resourceType;
    }

    public override NodeState Evaluate()
    {
        // send global event with amount/type of resource collected
        int resourceAmount = (int)Root.GetData("current_resource_amount");
        EventManager.TriggerEvent("ResourceCollected", new object[]
        {
            _resourceType, resourceAmount
        });

        // reset local storage of unit
        Root.SetData("current_resource_amount", 0);

        // clear target
        Root.ClearData("target");
        Root.ClearData("target_cell");
        Root.ClearData("target_is_resource");

        _state = NodeState.RUNNING;
        return _state;
    }
}
