using UnityEngine;


public class AmmoBox : BaseUsable
{
    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerInventory.MaxPistolAmmo != _playerInventory.CurrentPistolAmmo)
            {
                _playerMain.IsCanUse = true;
                _intaractionText.SetText(_intaractionText.AmmoBoxTxt);
            }
            else
            {
                _intaractionText.SetText(_intaractionText.AmmoBoxMaxTxt);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _playerMain.IsCanUse = false;
        _intaractionText.SetEmptyTxt();
    }

    #endregion


    #region Methods

    public override void Use()
    {
        _playerMain.IsCanUse = false;
        _playerInventory.RestockPistolAmmo();
        _intaractionText.SetEmptyTxt();
        Destroy(gameObject);
    }

    #endregion
}
