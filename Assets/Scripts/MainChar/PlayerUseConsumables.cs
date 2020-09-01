using UnityEngine;


public class PlayerUseConsumables : MonoBehaviour
{
    #region Fields

    [SerializeField] private Grenade _grenade;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _throwPos;
    [SerializeField] private Transform _throwGrenadePos;

    private PlayerInventory _playerInventory;
    private InputManager _inputManager;
    private Grenade _grenadeCopy;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _playerInventory = GetComponent<PlayerInventory>();
        _inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        CheckMineUsing();
        CheckGrenadeUsing();
    }

    private void CheckGrenadeUsing()
    {
        if (_inputManager.PressNeededButton(_inputManager.ThrowGrenade) && _playerInventory.CurrentGrenades != 0)
        {
            ThrowGrenade();
        }
    }

    private void CheckMineUsing()
    {
        if (_inputManager.PressNeededButton(_inputManager.SetMine) && _playerInventory.CurrentMines != 0)
        {
            PlaceMine();
        }
    }

    #endregion


    #region Methods

    private void PlaceMine()
    {
        Instantiate(_mine, _throwPos.position, Quaternion.identity);
        _playerInventory.SpendMine();
    }

    private void ThrowGrenade()
    {
        _grenadeCopy = Instantiate(_grenade, _throwGrenadePos.position, _throwGrenadePos.rotation);
        _grenadeCopy.GetThrowPosition(_throwGrenadePos, transform);
        _playerInventory.SpendGrenade();
    }

    #endregion
}
