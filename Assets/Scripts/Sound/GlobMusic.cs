using UnityEngine;


public class GlobMusic : MonoBehaviour
{
    #region Fields

    private AudioSource _music;
    private int _sceneCount;

    #endregion


    #region UnityMethods

    void Awake()
    {
        _music = GetComponent<AudioSource>();
    }

    #endregion


    #region Methods

    public void Music()
    {
        if (_music.isPlaying)
        {
            _music.Stop();
        }
        else
        {
            _music.Play();
        }
    }

    #endregion
}
