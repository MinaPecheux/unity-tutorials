using UnityEngine;

using BehaviorTree;

public class CheckIsVisible : Node
{
    private SpriteRenderer _spriteRenderer;

    public CheckIsVisible(SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
    }

    public override NodeState Evaluate()
    {
        _state = _spriteRenderer.enabled ? NodeState.SUCCESS : NodeState.FAILURE;
        return _state;
    }
}
