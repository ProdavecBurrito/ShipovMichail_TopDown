using UnityEngine;
using UnityEngine.UI;


public class PlayerMain : MonoBehaviour, IGetDamageable
{
    #region Fields

    public bool IsCanUse;
    public Image GameOverImage;
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] public bool _isGameOver;

    private InputManager _inputManager;
    private BaseUsable _baseUsable;
    private UIManager _uiManager;

    #endregion


    #region Properties

    public int Health => _health;
    public int MaxHealth => _maxHealth;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _isGameOver = false;
        _uiManager = FindObjectOfType<UIManager>();
        _baseUsable = null;
        _health = _maxHealth;
        IsCanUse = false;
    }

    private void Update()
    {
        CheckUsing();
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


    public void MaximizeHealth()
    {
        _health = _maxHealth;
        _uiManager.ChangeHealth();
    }

    private void CheckUsing()
    {
        if (Input.GetKeyDown(_inputManager.Use))
        {
            if (_baseUsable && IsCanUse)
            {
                _baseUsable.Use();
            }
        }
    }


    #endregion


    #region IGetDamage

    public void GetDamage(int dmg)
    {
        _health -= dmg;
        _uiManager.ChangeHealth();

        if (_health <= 0)
        {
            GameOverImage.enabled = true;
            _isGameOver = true;
        }
    }

    #endregion
}
