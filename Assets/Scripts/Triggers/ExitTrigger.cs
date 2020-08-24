using UnityEngine;
using UnityEngine.UI;


public class ExitTrigger : MonoBehaviour
{

    #region Constants

    private const float _exitTime = 2f;

    #endregion


    #region PrivateFields

    [SerializeField] private Image _exitImage;
    private bool _isExit;
    private float _curTime;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _isExit = false;
        _curTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            ExitGame();
        }
    }

    private void Update()
    {
        if (_isExit)
        {
            if (_curTime < _exitTime)
            {
                _curTime += Time.deltaTime;
                if (_curTime >= _exitTime)
                {
                    Application.Quit();
                }
            }
        }
    }

    #endregion


    #region Methods

    private void ExitGame()
    {
        _exitImage.enabled = true;

        _isExit = true;

    }

    #endregion

}
