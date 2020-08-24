using UnityEngine;

public class Laver : BaseUsable
{

    #region Constants

    private const float DESTROY_DOOR_TIME = 1f;

    #endregion


    #region PrivateFields

    [SerializeField] private GameObject _redDoor;

    private bool _isCanUse = true;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isCanUse)
            {
                _player.IsCanUse = true;

                _intaractionText.SetText(_intaractionText.LeverTxt);
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
        _isCanUse = false;
        _intaractionText.SetEmptyTxt();
        Destroy(_redDoor, DESTROY_DOOR_TIME);
    }

    #endregion

}
