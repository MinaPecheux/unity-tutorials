using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class HeroController : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;

    private DefaultPlayerActions _defaultPlayerActions;

    private InputAction _moveAction;

    private Rigidbody _rigidbody;

    private float _speed = 6f;
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

        _defaultPlayerActions.Player.Jump.performed += OnJump;
        _defaultPlayerActions.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();

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
    }

}
