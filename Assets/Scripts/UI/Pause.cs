using UnityEngine;


public class Pause : MonoBehaviour
{

    #region PrivateFields

    [SerializeField] private PlayerController _player;
    [SerializeField] private KeyCode _exitBtn = KeyCode.Escape;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        _player.enabled = false;
        //Cursor.visible = true; // Оставленно для корректировки в будущем
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        _player.enabled = true;
        //Cursor.visible = false;
        Time.timeScale = 1;
    }

    #endregion

}
