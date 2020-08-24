using UnityEngine;


public abstract class BaseUsable : MonoBehaviour
{

    #region ProtectedFields

    protected IntaractionTxt _intaractionText;
    protected PlayerController _player;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _intaractionText = FindObjectOfType<IntaractionTxt>();
    }

    #endregion


    #region Methods

    public virtual void Use()
    {
    }

    #endregion

}
