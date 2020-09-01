using UnityEngine;


public class Pause : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject[] _playerScripts;

    #endregion


    #region UnityMethods


    private void Start()
    {
        _playerScripts = GameObject.FindGameObjectsWithTag("Player");
    }

    private void OnEnable()
    {
        foreach (GameObject gameObjects in _playerScripts)
        {
            gameObjects.SetActive(false);
        }
        Time.timeScale = 0;
        //Cursor.visible = true; // Оставленно для корректировки в будущем
    }

    private void OnDisable()
    {
        foreach (GameObject gameObjects in _playerScripts)
        {
            gameObjects.SetActive(true);
        }
        //Cursor.visible = false;
        Time.timeScale = 1;
    }

    #endregion
}
