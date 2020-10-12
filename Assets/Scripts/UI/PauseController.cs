using UnityEngine;


public class PauseController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Pause _pauseMenu;
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private Pistol _gun;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerUseConsumables _playerUseConsumables;

    private InputManager _inputManager;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _gun = FindObjectOfType<Pistol>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerUseConsumables = FindObjectOfType<PlayerUseConsumables>();
    }

    private void Update()
    {
        CheckStartMenuActive();
        CheckPauseBtn();
        CheckPauseMenuExit();
    }

    #endregion


    #region Methods

    private void CheckPauseBtn()
    {
        if (_inputManager.PressNeededButton(_inputManager.PauseBtn))
        {
            if (_startMenu.activeSelf.Equals(true))
            {
                _startMenu.SetActive(false);
                PauseOff();
            }
            else
            {
                if (_pauseMenu.gameObject.activeSelf.Equals(false) && _optionsMenu.activeSelf.Equals(false))
                {
                    _pauseMenu.gameObject.SetActive(true);
                    PauseOn();
                }

                else
                {
                    _startMenu.SetActive(false);
                    _optionsMenu.SetActive(false);
                    _pauseMenu.gameObject.SetActive(false);
                    PauseOff();
                }
            }
        }
    }

    private void PauseOff()
    {
        _gun.enabled = true;
        _playerMovement.enabled = true;
        _playerUseConsumables.enabled = true;
        Time.timeScale = 1;
        //Cursor.visible = true; // Оставленно для корректировки в будущем
    }

    private void PauseOn()
    {
        _gun.enabled = false;
        _playerMovement.enabled = false;
        _playerUseConsumables.enabled = false;
        Time.timeScale = 0;
        //Cursor.visible = false; // Оставленно для корректировки в будущем
    }

    private void CheckPauseMenuExit()
    {
        if (/*!_pauseMenu.IsPaused &&*/ _optionsMenu.activeSelf.Equals(false))
        {
            PauseOff();
        }
    }

    private void CheckStartMenuActive()
    {
        if (_startMenu.activeSelf.Equals(true))
        {
            PauseOn();
        }
    }

    #endregion
}
