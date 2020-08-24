using UnityEngine;


public class Pistol : BaseGun
{

    #region Constants

    private const int MAX_MAG = 7;
    private const float RANDOM_RANGE = 0.3f;

    #endregion


    #region PrivateFields

    private UIController _uIController;
    private Bullet _pistolBulletCopy;
    private Vector3 _randomBulletX;
    private float _randomX;
    private float _currentTime;
    private Timer _timer;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _timer = new Timer();
        _isReloading = false;
        _currentTime = 0;
        _uIController = FindObjectOfType<UIController>();
        _ammoInMag = MAX_MAG;
        _currentAmmoCount = MaxAmmo;
        _isCanShoot = true;
        _isCanReload = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isCanShoot)
        {
            Fire();
        }

        if (Input.GetKeyDown(ReloadingBtn) && _isCanReload )
        {
            _timer.InitTimer(_reloadTime);
            _isReloading = true;
        }

        if (_isReloading)
        {
            _isCanReload = false;
            _isCanShoot = false;
            if (_currentTime < _reloadTime)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                _currentTime = 0;
                Reload();
            }
        }

        if (_ammoInMag != MAX_MAG && _currentAmmoCount != 0)
        {
            _isCanReload = true;
        }

    }

    #endregion


    #region Methods

    public override void Fire()
    {
        if (_ammoInMag > 0 && _isCanShoot)
        {
            _muzzleFlash.Play();
            _ammoInMag--;
            _pistolBulletCopy = Instantiate(_bullet, CalculateFireVector(), _fireStartPos.rotation);
            _uIController.ChangeAmmoInMag();
        }
    }

    public override void Reload()
    {

        if (_currentAmmoCount >= MAX_MAG || _ammoInMag + _currentAmmoCount > MAX_MAG)
        {
            _currentAmmoCount -= MaxMag - _ammoInMag;
            _ammoInMag = MAX_MAG;
        }
        else if (_ammoInMag + _currentAmmoCount < MAX_MAG)
        {
            _ammoInMag += _currentAmmoCount;
            _currentAmmoCount = 0;
        }
        else
        {
            _ammoInMag = _currentAmmoCount + _ammoInMag;
            _currentAmmoCount = 0;
        }

        _isReloading = false;
        _uIController.ChangeAmmoInMag();
        _uIController.ChangeMaxAmmo();
        _isCanShoot = true;
        
    }

    private Vector3 CalculateFireVector()
    {
        _randomX = Random.Range(-RANDOM_RANGE, RANDOM_RANGE);
        _randomBulletX = new Vector3(_fireStartPos.position.x + _randomX, _fireStartPos.position.y, _fireStartPos.position.z);
        return _randomBulletX;
    }

    public override void RestockAmmo()
    {
        _currentAmmoCount = MaxAmmo;
        _uIController.ChangeMaxAmmo();
    }

    #endregion

}
