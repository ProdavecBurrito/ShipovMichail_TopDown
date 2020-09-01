using UnityEngine;


public abstract class BaseUsable : MonoBehaviour
{
    #region Fields

    protected IntaractionTxt _intaractionText;
    protected PlayerMain _playerMain;
    protected PlayerInventory _playerInventory;

    #endregion


    #region UnityMethods

     protected virtual void Start()
    {
        _playerInventory = FindObjectOfType<PlayerInventory>();
        _playerMain = FindObjectOfType<PlayerMain>();
        _intaractionText = FindObjectOfType<IntaractionTxt>();
    }

    #endregion


    #region Methods

    public abstract void Use();

    #endregion
}
