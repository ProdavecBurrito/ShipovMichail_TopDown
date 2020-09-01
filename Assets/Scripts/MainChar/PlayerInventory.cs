using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    #region Fields

    public bool IsGotYellowKey;
    public bool IsGotBlueKey;

    [SerializeField] private int _maxMines;
    [SerializeField] private int _maxGrenades;
    [SerializeField] private int _currentMines;
    [SerializeField] private int _currentGrenades;
    [SerializeField] private int _maxPistolAmmo;
    [SerializeField] private int _currentPistolAmmo;

    private UIManager _uiManager;

    #endregion

    #region Preferences

    public int MaxPistolAmmo => _maxPistolAmmo;
    public int CurrentPistolAmmo => _currentPistolAmmo;
    public int MaxMines => _maxMines;
    public int CurrentMines => _currentMines;
    public int MaxGrenades => _maxGrenades;
    public int CurrentGrenades => _currentGrenades;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    #endregion


    #region Methods

    public void GetMine()
    {
        _currentMines++;
        _uiManager.ChangeMines();
    }

    public void GetGrenage()
    {
        _currentGrenades++;
        _uiManager.ChangeGrenades();
    }
    public void SpendMine()
    {
        _currentMines--;
        _uiManager.ChangeMines();
    }

    public void SpendGrenade()
    {
        _currentGrenades--;
        _uiManager.ChangeGrenades();
    }

    public void RestockPistolAmmo()
    {
        _currentPistolAmmo = _maxPistolAmmo;
        _uiManager.ChangeMaxAmmo();
    }

    public void ChangePistolAmmo(int ammo)
    {
        _currentPistolAmmo = ammo;
        _uiManager.ChangeMaxAmmo();
    }

    #endregion
}
