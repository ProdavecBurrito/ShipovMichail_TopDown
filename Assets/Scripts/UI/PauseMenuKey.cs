using UnityEngine;

public class PauseMenuKey : MonoBehaviour
{

    #region PrivateFields

    [SerializeField] private KeyCode _pauseBtn = KeyCode.Escape;
    [SerializeField] private GameObject _pauseMenu;

    #endregion


    #region UnityMethods

    private void Update()
    {
        if (Input.GetKeyDown(_pauseBtn))
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
