using UnityEngine;


public class HealthBox : BaseUsable
{

    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (_player.Health != _player.MaxHealth)
            {
                _player.IsCanUse = true;
                _intaractionText.SetText(_intaractionText.HealthBoxTxt);
            }
            else
            {
                _intaractionText.SetText(_intaractionText.HealthBoxMaxHPTxt);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _player.IsCanUse = false;
        _intaractionText.SetEmptyTxt();
    }

    #endregion


    #region Methods

    public override void Use()
    {
        _player.IsCanUse = false;
        _player.FullHealth();
        _intaractionText.SetEmptyTxt();
        Destroy(gameObject);
    }

    #endregion

}
