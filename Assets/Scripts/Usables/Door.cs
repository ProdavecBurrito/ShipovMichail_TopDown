using UnityEngine;


public class Door : BaseUsable
{

    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (gameObject.CompareTag("BlueDoor") && _player.IsGotBlueKey)
            {
                _player.IsCanUse = true;
                _intaractionText.SetText(_intaractionText.DoorText);
            }
            else if (gameObject.CompareTag("YellowDoor") && _player.IsGotYellowKey)
            {
                _player.IsCanUse = true;
                _intaractionText.SetText(_intaractionText.DoorText);
            }
            else
            {
                _intaractionText.SetText(_intaractionText.DoorTextDeny);
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
        _intaractionText.SetEmptyTxt();
        Destroy(gameObject);
    }

    #endregion 

}
