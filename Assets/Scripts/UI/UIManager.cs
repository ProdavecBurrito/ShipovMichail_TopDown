using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private Text _mineTxt;
    [SerializeField] private Text _healthTxt;
    [SerializeField] private Text _ammoInMagTxt;
    [SerializeField] private Text _maxAmmoTxt;
    [SerializeField] private Text _killCountTxt;
    [SerializeField] private Text _grenageTxt;

    private PlayerInventory _playerInventory;
    private PlayerMain _playerController;
    private BaseGun _gun;

    private int _currentGrenades;
    private int _currentMines;
    private int _currentHealth;
    private int _currentAmmoInMag;
    private int _currentMaxAmmo;
    private int _currentKills;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _gun = FindObjectOfType<BaseGun>();
        _playerInventory = FindObjectOfType<PlayerInventory>();
        _playerController = FindObjectOfType<PlayerMain>();

        _currentGrenades = _playerInventory.CurrentGrenades;
        _currentMines = _playerInventory.CurrentMines;
        _currentHealth = _playerController.Health;
        _currentKills = 0;
        _currentAmmoInMag = _gun.AmmoInMag;
        _currentMaxAmmo = _playerInventory.MaxPistolAmmo;

        _grenageTxt.text = _currentGrenades.ToString();
        _mineTxt.text = _currentMines.ToString();
        _healthTxt.text = _currentHealth.ToString();
        _killCountTxt.text = _currentKills.ToString();
        _ammoInMagTxt.text = _currentAmmoInMag.ToString();
        _maxAmmoTxt.text = _currentMaxAmmo.ToString();

    }

    #endregion


    #region Methods

    public void ChangeHealth()
    {
        _currentHealth = _playerController.Health;
        _healthTxt.text = _currentHealth.ToString();
    }

    public void ChangeAmmoInMag()
    {
        _currentAmmoInMag = _gun.AmmoInMag;
        _ammoInMagTxt.text = _currentAmmoInMag.ToString();
    }

    public void ChangeMaxAmmo()
    {
        _currentMaxAmmo = _playerInventory.CurrentPistolAmmo;
        _maxAmmoTxt.text = _currentMaxAmmo.ToString();
    }

    public void SetMaxAmmo()
    {
        _currentMaxAmmo = _playerInventory.MaxPistolAmmo;
        _maxAmmoTxt.text = _currentMaxAmmo.ToString();
    }

    public void AddPlayerKills()
    {
        _currentKills += 1;
        _killCountTxt.text = _currentKills.ToString();
    }

    public void ChangeMines()
    {
        _currentMines = _playerInventory.CurrentMines;
        _mineTxt.text = _currentMines.ToString();
    }

    public void ChangeGrenades()
    {
        _currentGrenades = _playerInventory.CurrentGrenades;
        _grenageTxt.text = _currentGrenades.ToString();
    }

    #endregion
}
