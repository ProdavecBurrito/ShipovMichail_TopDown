using UnityEngine;


public abstract class MovelessEnemy : BaseEnemy
{
    #region Constants

    protected float MAX_ROTATION_MAGNITUDE = 0.0f;

    #endregion


    #region Fields

    [SerializeField] private EnemyBullet _bullet;
    [SerializeField] private Transform _fireStartPos;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _attackDist;

    private EnemyBullet _bulletCopy;

    private Quaternion _startRotation;
    private Vector3 _newDirection;
    private Vector3 _projection;

    private float _currentTimeToLoseTarget;
    private float _timeToLoseTarget;
    private bool _isTargerLost;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        _timeToLoseTarget = 3.0f;
        _currentTimeToLoseTarget = 0f;
        _currentReload = 0f;
        _startRotation = transform.rotation;
    }

    protected override void Update()
    {
        base.Update();
        CheckVisibleTargets();
        CheckWhatTargetIsNotLost();
    }

    #endregion


    #region Methods

    private void CheckWhatTargetIsNotLost()
    {
        if (_isTargerLost)
        {
            _currentTimeToLoseTarget += Time.deltaTime;
            if (_currentTimeToLoseTarget >= _timeToLoseTarget)
            {
                ReturnToPatrole();
            }
        }
    }

    protected override void PursueTarget(Transform target)
    {
        base.PursueTarget(target);
        RotateForTarget();
        CheckWhatTargetIsVisible();
        CheckDistanceToLoseTarget();
    }

    private void CheckDistanceToLoseTarget()
    {
        if ((transform.position - _agrTarget.position).sqrMagnitude > _attackDist * _attackDist)
        {
            _isTargerLost = true;
            StopAttack();
        }
    }

    private void CheckWhatTargetIsVisible()
    {
        if (!_canSeePlayer)
        {
            _isTargerLost = true;
            StopAttack();
        }
    }

    private void RotateForTarget()
    {
        _projection = Vector3.ProjectOnPlane(transform.position - _agrTarget.position, Vector3.up);
        _newDirection = Vector3.RotateTowards(transform.forward, -_projection, _rotationSpeed * Time.deltaTime, MAX_ROTATION_MAGNITUDE);
        transform.rotation = Quaternion.LookRotation(_newDirection);
    }

    protected void StopAttack()
    {
        _visibleTarget = null;
    }

    protected void AttackTarget()
    {
        _bulletCopy = Instantiate(_bullet, _fireStartPos.position, _fireStartPos.rotation);
        _canAttack = false;
    }

    protected void ReturnToPatrole()
    {
        _isTargerLost = false;
        transform.rotation = _startRotation;
        _isAggressive = false;
        _isPatroling = true;
        _currentTimeToLoseTarget = 0f;
    }

    protected override void CheckVisibleTargets()
    {
        if (_visibleTarget != null)
        {
            CheckAttackPossibility();
            PursueTarget(_agrTarget);
        }
    }

    protected void CheckAttackPossibility()
    {
        if (_canAttack)
        {
            AttackTarget();
        }
        else
        {
            ReloadAttack();
        }
    }

    protected override void ReloadAttack()
    {
        _currentReload += Time.deltaTime;
        if (_currentReload >= _attackSpeed)
        {
            _currentReload = 0f;
            _canAttack = true;
        }
    }

    #endregion
}
