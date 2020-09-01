using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    #region Constants

    private const float SMOOTH = 0.2f;

    #endregion


    #region Fields

    [SerializeField] private Transform _followThis;
    private Vector3 _offset;
    private Vector3 _velosity = Vector3.zero;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _offset = transform.position - _followThis.transform.position;
        transform.position = _followThis.transform.position + _offset;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _followThis.transform.position + _offset, ref _velosity, SMOOTH);
       
    }

    #endregion
}
