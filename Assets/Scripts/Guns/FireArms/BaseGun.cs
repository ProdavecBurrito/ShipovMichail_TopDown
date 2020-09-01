using UnityEngine;


public abstract class BaseGun : MonoBehaviour
{
    #region Fields

    [SerializeField] protected Transform _fireStartPos;
    [SerializeField] protected ParticleSystem _muzzleFlash;
    [SerializeField] protected GameObject _hitParticle;
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected int _ammoInMag;
    [SerializeField] protected int _maxMag;
    [SerializeField] protected float _reloadTime;

    protected PlayerInventory _playerInventory;
    protected UIManager _uiManager;
    protected InputManager _inputManager;

    protected bool _isCanReload;
    protected bool _isCanShoot;
    protected bool _isReloading;

    #endregion


    #region Properties

    public int AmmoInMag => _ammoInMag;
    public bool IsCanReload => _isCanReload;
    public int MaxMag => _maxMag;
    public bool IsReloading => _isReloading;

    #endregion


    #region UnityMethods

    protected virtual void Start()
    {
        _playerInventory = FindObjectOfType<PlayerInventory>();
        _inputManager = GetComponent<InputManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    #endregion


    #region Methods

    public abstract void Fire();
    public abstract void Reload();

    #endregion
}
