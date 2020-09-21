using UnityEngine;
using UnityEngine.UI;


public class Sound : MonoBehaviour
{
    #region Fields

    [SerializeField] private Slider _volume;
    private AudioSource _audioSource;

    #endregion

    #region UnityMethods

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 1;
    }

    #endregion


    #region Methods

    public void ChengeVolume()
    {
        _audioSource.volume = _volume.value;
    }

    #endregion
}
