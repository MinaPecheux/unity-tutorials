using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
using AStar;

public class TaskWalk : Node
{
    private const float _REACH_THRESHOLD = 0.01f;

    private Transform _transform;
    private Pathfinder2D _pathfinder;
    private float _speed;
    private System.Action<Vector2> _onReachTile;

    private List<Vector3> _pathToTarget;

    public TaskWalk(
        Transform transform, Pathfinder2D pathfinder, float speed, System.Action<Vector2> onReachTile)
    {
        _transform = transform;
        _pathfinder = pathfinder;
        _speed = speed;
        _onReachTile = onReachTile;
    }

    public override NodeState Evaluate()
    {
        _state = NodeState.RUNNING;

        // if no path yet, compute it
        if (_pathToTarget == null)
        {
            Vector3 targetWorldPos = (Vector3)GetData("target");

            if (Vector3.Distance(_transform.position, targetWorldPos) < _REACH_THRESHOLD)
            {
                _state = NodeState.SUCCESS;
            }
            else
            {
                _pathToTarget = _pathfinder.FindPath(_transform.position, targetWorldPos);
                if (_pathToTarget == null)
                    _state = NodeState.FAILURE;
                else
                    _onReachTile(_GetNextDir());
            }
        }

        // else walk through the path
        else if (_pathToTarget.Count > 0)
        {
            Vector3 t = _pathToTarget[0];

            // if reached tile
            if (Vector3.Distance(_transform.position, t) < _REACH_THRESHOLD)
            {
                _transform.position = t;
                _pathToTarget.RemoveAt(0);

                // special case: reached the end
                if (_pathToTarget.Count == 0)
                {
                    _state = NodeState.SUCCESS;
                    _pathToTarget = null;
                }
                else
                {
                    _onReachTile(_GetNextDir());
                }
            }
            // else move towards the tile
            else
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, t, _speed * Time.deltaTime);
            }
        }

        return _state;
    }

    private Vector2 _GetNextDir()
    {
        Vector3 nextPoint = _pathToTarget[0];
        return (nextPoint - _transform.position).normalized;
    }

}
