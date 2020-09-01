using UnityEngine;


public class Key : BaseUsable
{

    #region Fields

    [SerializeField] private bool _isBlueKey;
    [SerializeField] private bool _isYellowKey;

    #endregion


    #region Properties

    public bool IsBlueKey => _isBlueKey;
    public bool IsYellowKey => _isYellowKey;

    #endregion


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

    public override void Use()
    {
        if (IsYellowKey)
        {
            GrabYellowKey();
        }

        if (IsBlueKey)
        {
            GrabBlueKey();
        }
        _intaractionText.SetEmptyTxt();
        Destroy(gameObject);
    }

    private void CheckWhatPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerMain.IsCanUse = true;

            _intaractionText.SetText(_intaractionText.KeyTxt);
        }
    }

    public void GrabYellowKey()
    {
        _playerInventory.IsGotYellowKey = true;
    }

    public void GrabBlueKey()
    {
        _playerInventory.IsGotBlueKey = true;
    }

    #endregion 

}
