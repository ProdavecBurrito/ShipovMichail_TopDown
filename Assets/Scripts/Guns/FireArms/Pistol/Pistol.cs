using UnityEngine;


public sealed class Pistol : BaseGun
{
    #region Constants

    private const int MAX_MAG = 7;
    private const float RANDOM_BULLER_DIRECTION = 0.2f;

    #endregion


    #region Fields

    private Bullet _pistolBulletCopy;

    private Vector3 _randomBulletX;

    private int _calculateAmmo;
    private float _randomX;
    private float _currentReloadTime;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        _isReloading = false;
        _currentReloadTime = 0;
        _ammoInMag = MAX_MAG;
        _isCanShoot = true;
        _isCanReload = false;
    }

    private void Update()
    {
        CheckThatCanFire();
        CheckReloading();
        CheckReloadingProcess();
        CheckThatCanReload();
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
            _uiManager.ChangeAmmoInMag();
        }
    }

    public override void Reload()
    {
        if (_playerInventory.CurrentPistolAmmo >= MAX_MAG || _ammoInMag + _playerInventory.CurrentPistolAmmo > MAX_MAG)
        {
            _calculateAmmo = _playerInventory.CurrentPistolAmmo - (MaxMag - _ammoInMag);
            _playerInventory.ChangePistolAmmo(_calculateAmmo);
            _ammoInMag = MAX_MAG;
        }
        else if (_ammoInMag + _playerInventory.CurrentPistolAmmo < MAX_MAG)
        {
            _ammoInMag += _playerInventory.CurrentPistolAmmo;
            _playerInventory.ChangePistolAmmo(0);
        }
        else
        {
            _ammoInMag = _playerInventory.CurrentPistolAmmo + _ammoInMag;
            _playerInventory.ChangePistolAmmo(0);
        }

        _uiManager.ChangeAmmoInMag();
        _isReloading = false;
        _isCanShoot = true;
    }

    private Vector3 CalculateFireVector()
    {
        _randomX = Random.Range(-RANDOM_BULLER_DIRECTION, RANDOM_BULLER_DIRECTION);
        _randomBulletX = new Vector3(_fireStartPos.position.x + _randomX, _fireStartPos.position.y, _fireStartPos.position.z);
        return _randomBulletX;
    }

    private void CheckThatCanReload()
    {
        if (_ammoInMag != MAX_MAG && _playerInventory.CurrentPistolAmmo != 0)
        {
            _isCanReload = true;
        }
    }

    private void CheckReloadingProcess()
    {
        if (_isReloading)
        {
            _isCanReload = false;
            _isCanShoot = false;
            if (_currentReloadTime < _reloadTime)
            {
                _currentReloadTime += Time.deltaTime;
            }
            else
            {
                _currentReloadTime = 0;
                Reload();
            }
        }
    }

    private void CheckReloading()
    {
        if (Input.GetKeyDown(_inputManager.Reload) && _isCanReload)
        {
            _isReloading = true;
        }
    }

    private void CheckThatCanFire()
    {
        if (Input.GetKeyDown(_inputManager.Fire) && _isCanShoot)
        {
            Fire();
        }
    }

    #endregion
}
