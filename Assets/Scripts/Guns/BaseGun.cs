using UnityEngine;


public abstract class BaseGun : MonoBehaviour
{

    #region ProtectedFields

    [SerializeField] protected Transform _fireStartPos;
    [SerializeField] protected ParticleSystem _muzzleFlash;
    [SerializeField] protected GameObject _hitParticle;
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected int _ammoInMag;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _currentAmmoCount;
    [SerializeField] protected int _maxMag;
    [SerializeField] protected float _reloadTime;

    protected bool _isCanReload;
    protected bool _isCanShoot;
    protected bool _isReloading;

    protected KeyCode _reload = KeyCode.R;

    #endregion


    #region Preferences

    public int AmmoInMag => _ammoInMag;
    public bool IsCanReload => _isCanReload;
    public int MaxAmmo => _maxAmmo;
    public int MaxMag => _maxMag;
    public KeyCode ReloadingBtn => _reload;
    public int CurrentAmmoCount => _currentAmmoCount;
    public bool IsReloading => _isReloading;

    #endregion


    #region Methods

    public abstract void Fire();

    public abstract void Reload();
    public abstract void RestockAmmo();

    #endregion

}
