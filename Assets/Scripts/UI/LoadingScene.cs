using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadingScene : MonoBehaviour
{
    #region Constants

    private const float LOADING_INACCURACY = 0.9f;
    private const int LOADING_SCREEN = 0;
    private const int MAIN_MENU = 1;

    #endregion


    #region Fields

    private Image _loadingCircle;
    private AsyncOperation _loading;

    private int _levelIndex;
    private float _loadingProgress;

    #endregion


    #region Methods

    private void Start()
    {
        _levelIndex = PlayerPrefs.GetInt("LvlIndex");
        if (_levelIndex == LOADING_SCREEN)
        {
            _levelIndex = MAIN_MENU;
        }
        _loadingCircle = GetComponent<Image>();
        _loading = SceneManager.LoadSceneAsync(_levelIndex);
        StartCoroutine(nameof(Load));
    }

    private IEnumerator Load()
    {
        while(!_loading.isDone)
        {
            _loadingProgress = _loading.progress / LOADING_INACCURACY;
            _loadingCircle.fillAmount = _loadingProgress;
            yield return null;
        }
    }

    #endregion
}
