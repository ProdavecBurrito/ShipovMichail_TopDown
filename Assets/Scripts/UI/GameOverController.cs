using UnityEngine;


public class GameOverController : MonoBehaviour
{
    #region Constants

    private const float EXIT_TIME = 2.0f;

    #endregion


    #region Fields

    private PlayerMain _player;
    private GameObject _mainChar;
    private float _currentTime;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _mainChar = GameObject.FindGameObjectWithTag("Player");
        _player = FindObjectOfType<PlayerMain>();
    }

    private void FixedUpdate()
    {
        CheckThatGameOverProcessIsOn();
    }

    #endregion


    #region Methods

    private void CheckThatGameOverProcessIsOn()
    {
        if (_player._isGameOver)
        {
            _mainChar.SetActive(false);
            if (_currentTime < EXIT_TIME)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= EXIT_TIME)
                {
                    Application.Quit();
                }
            }
        }
    }

    #endregion
}
