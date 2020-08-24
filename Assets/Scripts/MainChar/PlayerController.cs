using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IGetDamage
{

    #region Constants

    private const float MAX_ROTATION_MAGNITUDE = 0.0f;

    #endregion


    #region Fields

    public bool IsGotYellowKey;
    public bool IsGotBlueKey;
    public bool IsCanUse;
    public Image GameOverImage;

    #endregion


    #region PrivateFields

    [SerializeField] private Camera _cam;
    [SerializeField] private int _health;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _reloadWalkSpeed;
    [SerializeField] private int _maxHealth;
    [SerializeField] private BaseGun _gun;
    [SerializeField] private KeyCode _use = KeyCode.F;
    [SerializeField] private KeyCode _setMine = KeyCode.G;
    [SerializeField] private int _mineCounter;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _throwPos;
    [SerializeField] private int _maxMines;
    [SerializeField] private bool _isGameOver;

    private bool _isAlive;
    private float _overTime;
    private float _currentOverTime;
    private float _deltaX;
    private float _deltaY;
    private float _deltaZ;
    private float _gravity;
    private float _currentWalkSpeed;

    private Vector3 _newDirection;
    private Vector3 _movement;
    private Vector3 _pointToLook;
    private Vector3 _projection;
    private Vector3 _pos;

    private RaycastHit _hit;
    private Ray _ray;

    private GameObject _mineCopy;
    private BaseUsable _baseUsable;
    private UIController _uIController;

    #endregion


    #region Preferences

    public KeyCode Use => _use;
    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public int MaxMines => _maxMines;
    public int CurrentMines => _mineCounter;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _overTime = 2f;
        _isAlive = true;
        _currentOverTime = 0;
        _isGameOver = false;
        _uIController = FindObjectOfType<UIController>();
        _baseUsable = null;
        _health = _maxHealth;
        _gravity = 0f;
        IsCanUse = false;
        IsGotBlueKey = false;
        IsGotYellowKey = false;
    }

    private void Update()
    {
        if (_isAlive)
        {
            _deltaX = Input.GetAxis("Horizontal");
            _deltaZ = Input.GetAxis("Vertical");
            _deltaY = _gravity;

            if (_deltaX != 0 || _deltaZ != 0)
            {
                MoveChar();
            }


            RotateChar();

            if (Input.GetKeyDown(_use))
            {
                if (_baseUsable && IsCanUse)
                {
                    _baseUsable.Use();
                }
            }

            if (Input.GetKeyDown(_setMine) && _mineCounter != 0)
            {
                PlaceMine();
            }

            if (_gun.IsReloading)
            {
                _currentWalkSpeed = _reloadWalkSpeed;
            }
            else
            {
                _currentWalkSpeed = _walkSpeed;
            }
        }
    }

    private void LateUpdate()
    {
        if (_isGameOver)
        {
            _currentOverTime += Time.deltaTime;
            if (_currentOverTime >= _overTime)
            {
                Application.Quit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _baseUsable = other.GetComponent<BaseUsable>();
    }

    private void OnTriggerExit(Collider other)
    {
        IsCanUse = false;
        _baseUsable = null;
    }

    #endregion


    #region Methods

    public void GetDamage(int dmg)
    {
        _health -= dmg;
        _uIController.ChangeHealth();

        if (_health <= 0)
        {
            _isAlive = false;
            GameOverImage.enabled = true;
            _isGameOver = true;
        }
    }

    Vector3 VectorUp()
    {
        return transform.position + Vector3.up ;
    }

    public void FullHealth()
    {
        _health = _maxHealth;
        _uIController.ChangeHealth();
    }

    private void MoveChar()
    {
        _movement = new Vector3(_deltaX, _deltaY, _deltaZ);
        _movement *= _currentWalkSpeed * Time.deltaTime;
        _movement = Vector3.ClampMagnitude(_movement, _currentWalkSpeed);

        transform.position += _movement;
    }

    private void RotateChar()
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

    public void GetMine()
    {
        _mineCounter += 1;
    }

    private void PlaceMine()
    {
        _mineCopy = Instantiate(_mine, _throwPos.position, Quaternion.identity);
        _mineCounter--;
        _uIController.ChangeMines();
    }

    #endregion

}
