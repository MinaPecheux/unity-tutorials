using UnityEngine;

using BehaviorTree;

public class CheckInTargetRange : Node
{
    private const float _REACH_THRESHOLD = 0.01f;

    private Transform _transform;

    public CheckInTargetRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Vector3 t = (Vector3)Root.GetData("target");
        _state = Vector2.Distance(_transform.position, t) < _REACH_THRESHOLD
            ? NodeState.SUCCESS : NodeState.FAILURE;
        return _state;
    }
}
