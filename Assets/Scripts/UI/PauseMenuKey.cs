using UnityEngine;

public class PauseMenuKey : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _pauseMenu;

    private InputManager _inputManager;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        CheckPauseBtn();
    }

    #endregion


    #region Methods

    private void CheckPauseBtn()
    {
        if (Input.GetKeyDown(_inputManager.PauseBtn))
        {
            if (_pauseMenu.activeSelf == false)
            {
                _pauseMenu.SetActive(true);
            }
            else
            {
                _pauseMenu.SetActive(false);
            }
        }
    }

    #endregion
}
