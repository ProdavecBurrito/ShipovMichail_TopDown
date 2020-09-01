using UnityEngine;


public sealed class InputManager : MonoBehaviour
{
    #region fields

    public Vector3 Movement;
    public KeyCode Use = KeyCode.F;
    public KeyCode SetMine = KeyCode.G;
    public KeyCode ThrowGrenade = KeyCode.V;
    public KeyCode PauseBtn = KeyCode.Escape;
    public KeyCode Reload = KeyCode.R;
    public KeyCode Fire = KeyCode.Mouse0;

    #endregion


    #region UnityMethods

    private void Update()
    {
        Movement.x = Input.GetAxis("Horizontal");
        Movement.z = Input.GetAxis("Vertical");
    }

    #endregion


    #region Methods

    public bool PressNeededButton(KeyCode needed)
    {
        if (Input.GetKeyDown(needed))
        {
            return true;
        }
        return false;
    }

    #endregion
}
