using UnityEngine;
using UnityEngine.UI;

public class GraphicsQuality : MonoBehaviour
{
    #region Fields

    private Dropdown _qualityDropdown;

    #endregion


    #region UnityMethods

    private void Awake()
    {
        _qualityDropdown = GetComponent<Dropdown>();
    }

    #endregion

    #region Methods

    public void ChangeGraphics()
    {
        QualitySettings.SetQualityLevel(_qualityDropdown.value, true);
    }

    #endregion
}
