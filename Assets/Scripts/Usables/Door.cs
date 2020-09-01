using UnityEngine;


public class Door : BaseUsable
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

            if (gameObject.CompareTag("BlueDoor") && _playerInventory.IsGotBlueKey)
            {
                _playerMain.IsCanUse = true;
                _intaractionText.SetText(_intaractionText.DoorText);
            }
            else if (gameObject.CompareTag("YellowDoor") && _playerInventory.IsGotYellowKey)
            {
                _playerMain.IsCanUse = true;
                _intaractionText.SetText(_intaractionText.DoorText);
            }
            else
            {
                _intaractionText.SetText(_intaractionText.DoorTextDeny);
            }
        }
    }

    public override void Use()
    {
        _intaractionText.SetEmptyTxt();
        Destroy(gameObject);
    }

    #endregion 
}
