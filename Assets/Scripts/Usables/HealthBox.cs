using UnityEngine;


public class HealthBox : BaseUsable
{
    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        CheckWhatPlayer(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _playerMain.IsCanUse = false;
        _intaractionText.SetEmptyTxt();
    }

    #endregion


    #region Methods

    private void CheckWhatPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (_playerMain.Health != _playerMain.MaxHealth)
            {
                _playerMain.IsCanUse = true;
                _intaractionText.SetText(_intaractionText.HealthBoxTxt);
            }
            else
            {
                _intaractionText.SetText(_intaractionText.HealthBoxMaxHPTxt);
            }
        }
    }

    public override void Use()
    {
        _playerMain.IsCanUse = false;
        _playerMain.MaximizeHealth();
        _intaractionText.SetEmptyTxt();
        Destroy(gameObject);
    }

    #endregion
}
