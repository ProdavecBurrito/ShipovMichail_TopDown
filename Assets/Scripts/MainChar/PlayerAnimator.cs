using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    #region Fields

    private Animator _animator;
    private InputManager _inputManager;

    private float _playerSpeed;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {

        _playerSpeed = Mathf.Abs(_inputManager.Movement.z) + Mathf.Abs(_inputManager.Movement.x);
        CheckSpeed();
    }

    #endregion


    #region Methods

    private void CheckSpeed()
    {
        if (_playerSpeed == 0)
        {
            _animator.SetBool("is_walk", false);
        }
        else
        {
            _animator.SetBool("is_walk", true);
        }
    }

    #endregion
}
