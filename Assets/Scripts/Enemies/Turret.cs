using System.Collections;
using UnityEditor;
using UnityEngine;


public class Turret : BaseEnemy
{

    #region Constants

    private float MAX_ROTATION_MAGNITUDE = 0f;

    #endregion


    #region PrivateFields

    [SerializeField] private UIController _killCount;
    [SerializeField] private Transform _fireStartPos;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private EnemyBullet _bullet;
    [SerializeField] private float _attackDist;

    private Quaternion _startRotation;
    private Vector3 _newDirection;
    private Vector3 _projection;
    private Collider[] _targetsInViewRadius;
    private Collider _ghostCollider;
    private EnemyBullet _bulletCopy;
    private bool _isCanAttack;

    #endregion


    #region UnityMethods


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position + Vector3.up;

        Handles.color = new Color(1, 0, 1, 0.3f);
        Handles.DrawSolidArc(pos, Vector3.up, transform.forward, _maxAngle, _maxRadius);
        Handles.DrawSolidArc(pos, Vector3.up, transform.forward, -_maxAngle, _maxRadius);
    }

#endif


    private void Start()
    {
        _currentReload = 0;
        _startRotation = transform.rotation;
        _killCount = FindObjectOfType<UIController>();
        _isAlive = true;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _ghostCollider = GetComponent<Collider>();


        _isPatroling = true;

    }

    private void Update()
    {
        if (_isAlive)
        {
            if (_visibleTarget != null)
            {
                Attack(_agrTarget);
                if (_isCanAttack)
                {
                    Fire();
                }
                else
                {
                    _currentReload += Time.deltaTime;
                    if (_currentReload >= _attackSpeed)
                    {
                        _currentReload = 0;
                        _isCanAttack = true;
                    }
                }
            }

            if (_isPatroling)
            {
                StartCoroutine("FindTargets", 0.1f);
            }
        }

    }
    #endregion


    #region Methods

    public override void GetDamage(int damage)
    {
        ChangeHealth(damage);

        if (_health > 0)
        {
            Attack(_playerTransform);
        }
        else
        {
            Die();
        }
    }

    protected override void Die()
    {
        _killCount.ChangeKills();
        _isAlive = false;
        _ghostCollider.enabled = false;
        Destroy(gameObject, _dieTime);
    }

    protected override void ChangeHealth(int damage)
    {
        _health -= damage;
    }

    protected override void SetDamage(IGetDamage obj)
    {
        if (obj != null)
        {
            obj.GetDamage(_attackDmg);
        }
    }

    protected override void Attack(Transform target)
    {
        _isPatroling = false;
        _isAggressive = true;
        _agrTarget = _visibleTarget;
        _projection = Vector3.ProjectOnPlane(transform.position - _agrTarget.position, Vector3.up);
        _newDirection = Vector3.RotateTowards(transform.forward, -_projection, _rotationSpeed * Time.deltaTime, MAX_ROTATION_MAGNITUDE);

        transform.rotation = Quaternion.LookRotation(_newDirection);

        if ((transform.position - _agrTarget.position).sqrMagnitude > _attackDist * _attackDist)
        {
            StopAttack();
        }
        
    }

    protected override void FindVisibleTargets()
    {
        _targetsInViewRadius = Physics.OverlapSphere(transform.position, _maxRadius, _targerMask);
        for (int i = 0; i < _targetsInViewRadius.Length; i++)
        {
            _target = _targetsInViewRadius[i].transform;
            _dirToTarget = (_target.position - transform.position).normalized;
            _targerAngle = Vector3.Angle(transform.forward, _dirToTarget);

            if (_targerAngle > -_maxAngle && _targerAngle < _maxAngle)
            {
                _visibleTarget = _target;
            }
        }
    }

    private void StopAttack()
    {
        transform.rotation = _startRotation;
        _isAggressive = false;
        _isPatroling = true;
        _visibleTarget = null;
    }

    private void Fire()
    {
        _bulletCopy = Instantiate(_bullet, _fireStartPos.position, _fireStartPos.rotation);
        _isCanAttack = false;
    }

    #endregion


    #region Caroutines

    IEnumerator FindTargets(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    #endregion

}
