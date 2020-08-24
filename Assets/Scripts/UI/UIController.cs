using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{

    #region PrivateFields

    [SerializeField] private Text _mineTxt;
    [SerializeField] private Text _healthTxt;
    [SerializeField] private Text _ammoTxt;
    [SerializeField] private Text _maxAmmoTxt;
    [SerializeField] private Text _killCountTxt;

    private PlayerController _player;
    private BaseGun _gun;

    private int _currentMines;
    private int _currentHealth;
    private int _currentAmmoInMag;
    private int _currentMaxAmmo;
    private int _currentKills;
    private int _maxMag;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _gun = FindObjectOfType<BaseGun>();
        _player = FindObjectOfType<PlayerController>();

        _currentMines = _player.CurrentMines;
        _currentHealth = _player.Health;
        _currentKills = 0;
        _currentAmmoInMag = _gun.AmmoInMag;
        _currentMaxAmmo = _gun.CurrentAmmoCount;
        _maxMag = _gun.MaxMag;

        _mineTxt.text = _currentMines.ToString();
        _healthTxt.text = _currentHealth.ToString();
        _killCountTxt.text = _currentKills.ToString();
        _ammoTxt.text = _currentAmmoInMag.ToString();
        _maxAmmoTxt.text = _currentMaxAmmo.ToString();

    }

    #endregion


    #region Methods

    public void ChangeHealth()
    {
        _currentHealth = _player.Health;
        _healthTxt.text = _currentHealth.ToString();
    }

    public void ChangeAmmoInMag()
    {
        _currentAmmoInMag = _gun.AmmoInMag;
        _ammoTxt.text = _currentAmmoInMag.ToString();
    }

    public void ChangeMaxAmmo()
    {
        _currentMaxAmmo = _gun.CurrentAmmoCount;
        _maxAmmoTxt.text = _currentMaxAmmo.ToString();
    }

    public void SetMaxAmmo()
    {
        _currentMaxAmmo = _gun.MaxAmmo;
        _maxAmmoTxt.text = _currentMaxAmmo.ToString();
    }

    public void ChangeKills()
    {
        _currentKills += 1;
        _killCountTxt.text = _currentKills.ToString();
    }

    public void ChangeMines()
    {
        _currentMines = _player.CurrentMines;
        _mineTxt.text = _currentMines.ToString();
    }

    #endregion

}
