using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEditor;


[RequireComponent(typeof(NavMeshAgent))]
public class Ghost : BaseEnemy
{

    #region ProtectedFields

    [SerializeField] protected float _patrolStopDistance;
    [SerializeField] protected float _pursuingStopDistance;
    [SerializeField] protected float _patrolMoveSpeed;
    [SerializeField] protected float _pursueMoveSpeed;
    protected bool _isCanAttack;

    #endregion

    #region PrivateFields

    [SerializeField] private UIController _killCount;
    [SerializeField] private List<Vector3> _wayPoints;
    [SerializeField] private Transform _wayPointMain;

    private Collider[] _targetsInViewRadius;
    private CapsuleCollider _ghostCollider;

    private int _poinCounter;

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


    protected virtual void Start()
    {
        _killCount = FindObjectOfType<UIController>();
        _wayPoints = new List<Vector3>();
        _isAlive = true;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _ghostCollider = GetComponent<CapsuleCollider>();
        _agentNavMesh = GetComponent<NavMeshAgent>();
        _agentNavMesh.updatePosition = true;
        _agentNavMesh.updateRotation = true;
        _agentNavMesh.stoppingDistance = _patrolStopDistance;


        foreach (Transform T in _wayPointMain)
        {
            _wayPoints.Add(T.position);
        }

        _isPatroling = true;

    }

    protected virtual void Update()
    {
        if (_agentNavMesh)
        {
            if (_isAlive)
            {
                if (_visibleTarget != null)
                {
                    Attack(_agrTarget);
                }

                if (_isPatroling)
                {
                    Patrol();
                    StartCoroutine("FindTargets", 0.1f);
                }
            }
        }
    }

    protected virtual void LateUpdate()
    {
        if (!_isCanAttack)
        {
            _currentReload += Time.deltaTime;
            if (_currentReload >= _attackSpeed)
            {
                _isCanAttack = true;
                _currentReload = 0;
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
        _agentNavMesh.ResetPath();
        _agentNavMesh.enabled = false;
        _killCount.ChangeKills();
        _isAlive = false;
        //_ghostCollider.enabled = false;
        Destroy(gameObject, _dieTime);
    }

    protected override void ChangeHealth(int damage)
    {
        _health -= damage;
    }

    protected override void Patrol()
    {
        _agentNavMesh.speed = _patrolMoveSpeed;
        _agentNavMesh.stoppingDistance = _patrolStopDistance;
        if (_wayPoints.Count > 1)
        {
            _agentNavMesh.SetDestination(_wayPoints[_poinCounter]);

            if (!_agentNavMesh.hasPath)
            {
                _timeOut += Time.deltaTime;
                if (_timeOut > _timeWait)
                {
                    _timeOut = 0;
                    if (_poinCounter < _wayPoints.Count - 1)
                    {
                        _poinCounter++;
                    }
                    else
                    {
                        _poinCounter = 0;
                    }
                }
            }
        }
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
        _agentNavMesh.speed = _pursueMoveSpeed;
        _agrTarget = _visibleTarget;
        _agentNavMesh.stoppingDistance = _pursuingStopDistance;
        _agentNavMesh.SetDestination(target.position);

        if ((transform.position - _agrTarget.position).sqrMagnitude < _pursuingStopDistance * _pursuingStopDistance)
        {
            if (_isCanAttack)
            {
                AttackChar();
            }
        }
    }

    public override void GetWayPoints (Transform wayPoints)
    {
        _wayPointMain = wayPoints;
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
                _distToTarget = Vector3.Distance(transform.position, _target.position);
                _visibleTarget = _target;
            }
        }
    }

    protected void AttackChar()
    {
        SetDamage(_playerTransform.GetComponent<IGetDamage>());
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
