using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    #region Constance

    private const float MAX_ROTATION_MAGNITUDE = 0.0f;

    #endregion


    #region Fields

    [SerializeField] private Camera _cam;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _reloadWalkSpeed;

    private InputManager _inputManager;
    private BaseGun _gun;

    private RaycastHit _hit;
    private Ray _ray;
    private Vector3 _newDirection;
    private Vector3 _pointToLook;
    private Vector3 _projection;

    private float _currentWalkSpeed;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _gun = FindObjectOfType<BaseGun>();
        _cam = FindObjectOfType<Camera>();
        _inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        CheckPlayerMovementValue();
        RotatePlayer();
        CheckCurrentMovementSpeed();
    }

    #endregion


    #region Methods

    private void CheckCurrentMovementSpeed()
    {
        if (_gun.IsReloading)
        {
            _currentWalkSpeed = _reloadWalkSpeed;
        }
        else
        {
            _currentWalkSpeed = _walkSpeed;
        }
    }

    private void CheckPlayerMovementValue()
    {
        if (_inputManager.Movement.x != 0 || _inputManager.Movement.z != 0)
        {
            _inputManager.Movement.Normalize();
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        _inputManager.Movement *= _currentWalkSpeed * Time.deltaTime;
        transform.position += _inputManager.Movement;
    }

    private void RotatePlayer()
    {
        _ray = _cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit))
        {
            _pointToLook = _hit.point;
            _projection = Vector3.ProjectOnPlane(transform.position - _pointToLook, Vector3.up);
            _newDirection = Vector3.RotateTowards(transform.forward, -_projection, _rotationSpeed * Time.deltaTime, MAX_ROTATION_MAGNITUDE);

            transform.rotation = Quaternion.LookRotation(_newDirection);
        }
    }

    #endregion
}
