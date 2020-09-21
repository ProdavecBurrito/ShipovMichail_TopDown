using UnityEngine;
using UnityEngine.SceneManagement;


public class DestroyMainMenuMusic : MonoBehaviour
{
    #region Fields

    private const int NEEDED_LVL = 2;

    #endregion


    #region UnityMethods

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == NEEDED_LVL)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
