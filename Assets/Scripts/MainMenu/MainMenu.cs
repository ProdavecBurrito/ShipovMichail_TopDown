using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    #region Constants

    private const int FIRST_LEVEL = 2;

    #endregion

    #region Fields

    [SerializeField] private Button startGame;
    [SerializeField] private Button exitGame;
    [SerializeField] private AudioSource _musicSource;

    #endregion


    #region Methods

    public void Play(int index)
    {
        DontDestroyOnLoad(_musicSource);
        PlayerPrefs.SetInt("LvlIndex", FIRST_LEVEL);
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion
}
