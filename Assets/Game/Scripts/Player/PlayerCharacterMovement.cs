using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _sprintSpeed = 2f;
    [SerializeField] private float _acceleration = 0.5f;

    [SerializeField] private float _gravityScale = 1f;
    [SerializeField] private CharacterController _characterController;

    private float _currentSpeed = 1f;
    private Vector3 _velocityXZ;
    private float _velocityY;

    private Vector3 _movementDirection;

    private bool _isGrounded;
    private bool _isSprinting;

    private void CheckIsGrounded()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        _isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundLayer);
    }

    public void SetMoveDirection(Vector2 inputDirection) {
        _movementDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    public void CalculateVelocityXZ() {
        Transform cameraTransform = Camera.main.transform;

        Vector3 xDirection = _movementDirection.x * cameraTransform.right;
        Vector3 zDirection = _movementDirection.z * cameraTransform.forward;
        Vector3 direction = xDirection + zDirection;
        direction.y = 0;

        if(_movementDirection.magnitude >= 0.01) {
            _velocityXZ = direction.normalized * _currentSpeed * Time.deltaTime;
        }
        else {
            _velocityXZ = Vector3.zero;
        }

    }

    public void CalculateVelocityY()
    {
        _velocityY = _velocityY + Physics.gravity.y * _gravityScale * Time.deltaTime;
    }

    private void ResetVelocityY()
    {
        if (_isGrounded && _velocityY < 0)
        {
            _velocityY = -2f;
        }
    }

    private void CalculateAcceleration()
    {
        if(_movementDirection.magnitude > 0.01)
        {
            if (_isSprinting)
            {
                _currentSpeed += _acceleration * Time.deltaTime;
            }
            else
            {
                _currentSpeed -= _acceleration * Time.deltaTime;
            }

            _currentSpeed = Mathf.Clamp(_currentSpeed, _walkSpeed, _sprintSpeed);
        }
        else
        {
            _currentSpeed = 0;
        }
    }

    public void SetSprint(bool isSprinting)
    {
        _isSprinting = isSprinting;
    }

    public void Move() {
        CalculateVelocityXZ();
        CalculateVelocityY();

        Vector3 velocity = new Vector3(_velocityXZ.x, _velocityY, _velocityXZ.z);

        _characterController.Move(velocity);
    }

    private void Update() {
        CheckIsGrounded();
        CalculateAcceleration();
        ResetVelocityY();
        Move();
    }
}
