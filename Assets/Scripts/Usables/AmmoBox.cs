using UnityEngine;


public class AmmoBox : BaseUsable
{

    #region PrivateMethods

    private Pistol _pistol;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _pistol = FindObjectOfType<Pistol>();
        _intaractionText = FindObjectOfType<IntaractionTxt>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_pistol.MaxAmmo != _pistol.CurrentAmmoCount)
            {
                _player.IsCanUse = true;
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
        _player.IsCanUse = false;
        _intaractionText.SetEmptyTxt();
    }

    #endregion


    #region Methods

    public override void Use()
    {
        _player.IsCanUse = false;
        _pistol.RestockAmmo();
        _intaractionText.SetEmptyTxt();
        Destroy(gameObject);
    }

    #endregion

}
