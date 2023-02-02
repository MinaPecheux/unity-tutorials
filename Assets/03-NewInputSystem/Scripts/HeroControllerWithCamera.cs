using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class HeroControllerWithCamera : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _camera;

    private DefaultPlayerActions _defaultPlayerActions;

    private InputAction _moveAction;
    private InputAction _lookAction;

    private Rigidbody _rigidbody;

    private float _speed = 6f;
    private float _cameraSpeed = 80f;
    private float _jumpForce = 6f;
    private bool _isGrounded;

    private LayerMask _groundLayerMask;

    private void Awake()
    {
        _defaultPlayerActions = new DefaultPlayerActions();
        _rigidbody = GetComponent<Rigidbody>();

        _groundLayerMask = LayerMask.GetMask("Ground");
    }

    private void OnEnable()
    {
        _moveAction = _defaultPlayerActions.Player.Move;
        _moveAction.Enable();
        _lookAction = _defaultPlayerActions.Player.Look;
        _lookAction.Enable();

        _defaultPlayerActions.Player.Jump.performed += OnJump;
        _defaultPlayerActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();

        _defaultPlayerActions.Player.Jump.performed -= OnJump;
        _defaultPlayerActions.Player.Jump.Disable();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.Raycast(
            _groundCheck.position, Vector3.down, 0.05f, _groundLayerMask);

        Vector2 moveDir = _moveAction.ReadValue<Vector2>();
        Vector3 vel = _rigidbody.velocity;
        vel.x = _speed * moveDir.x;
        vel.z = _speed * moveDir.y;
        _rigidbody.velocity = vel;

        Vector2 lookDir = _lookAction.ReadValue<Vector2>();
        _camera.Rotate(Vector3.up * lookDir.x * Time.deltaTime * _cameraSpeed, Space.World);
        _camera.eulerAngles = new Vector3(
            _camera.eulerAngles.x,
            _ClampAngle(_camera.eulerAngles.y, -45f, 45f),
            _camera.eulerAngles.z);
    }

    private float _ClampAngle(float angle, float min, float max)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + min);
        return Mathf.Min(angle, max);
    }

}
