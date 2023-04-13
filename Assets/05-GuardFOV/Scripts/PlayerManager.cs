using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Camera _cam;

    private const int _groundLayerMask = 1 << 6;
    private Ray _ray;
    private RaycastHit _hit;

    private bool _running;
    private float _speed = 5f;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _cam = Camera.main;
        _targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000, _groundLayerMask))
            {
                _targetPosition = _hit.point;
                transform.LookAt(_targetPosition);
                _running = true;
            }
        }

        float d = Vector3.Distance(transform.position, _targetPosition);
        if (d < 0.1f && _running)
        {
            transform.position = _targetPosition;
            _running = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _speed);
        }
    }
}
