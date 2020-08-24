using UnityEngine;


public class Key : BaseUsable
{

    #region PrivateFields

    [SerializeField] private bool _isBlueKey;
    [SerializeField] private bool _isYellowKey;

    #endregion


    #region Preferences

    public bool IsBlueKey => _isBlueKey;
    public bool IsYellowKey => _isYellowKey;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.IsCanUse = true;

            _intaractionText.SetText(_intaractionText.KeyTxt);
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

    public void GrabYellowKey()
    {
        _player.IsGotYellowKey = true;
    }

    public void GrabBlueKey()
    {
        _player.IsGotBlueKey = true;
    }

    #endregion 

}
