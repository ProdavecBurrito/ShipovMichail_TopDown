using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public abstract class BaseEnemy : MonoBehaviour, IGetDamageable
{
    #region Constants

    protected float START_SEASRCH_DISTANCE = 15.0f;

    #endregion


    #region Fields

    [SerializeField] protected Transform _visibleTarget;
    [SerializeField] protected LayerMask _targerMask;

    [SerializeField] protected int _health;
    [SerializeField] protected float _maxAngle;
    [SerializeField] protected float _maxRadius;
    [SerializeField] protected float _dieTime;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected bool _isPatroling;
    [SerializeField] protected bool _isAggressive;

    protected Collider[] _targetsInViewRadius;
    protected UIManager _uiKillCount;
    protected NavMeshAgent _agentNavMesh;
    protected Transform _playerTransform;
    protected Transform _target;
    protected Transform _agrTarget;

    protected TextMesh _healthTxt;
    protected Vector3 _dirToTarget;
    protected RaycastHit _hit;
    protected Vector3 _dirToPlayer;

    protected bool _canAttack;
    protected bool _canSeePlayer;
    protected float _findTargetsDelay;
    protected float _currentTimeToWait;
    protected float _targerAngle;
    protected float _distToTarget;
    protected float _currentReload;

    #endregion


    #region Properties

    public float MaxRadius => _maxRadius;
    public float MaxAngle => _maxAngle;

    #endregion


    #region UnityMethods

    protected virtual void Start()
    {
        _healthTxt = GetComponentInChildren<TextMesh>();
        _isPatroling = true;
        _uiKillCount = FindObjectOfType<UIManager>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        CheckinMinDistanceToPlayer();
    }

    #endregion


    #region Methods

    protected abstract void CheckVisibleTargets();

    protected abstract void ReloadAttack();

    protected virtual void PursueTarget(Transform target)
    {
        _isPatroling = false;
        _isAggressive = true;

        CheckTarget();
    }

    protected void ChangeHealth(int damage)
    {
        _health -= damage;
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
        _uiKillCount.AddPlayerKills();
        Destroy(gameObject, _dieTime);
    }

    protected void DrawRay()
    {
        _dirToPlayer = (_playerTransform.position + Vector3.up) - (transform.position + Vector3.up);
        var ray = Physics.Raycast(transform.position + Vector3.up, _dirToPlayer, out _hit, _dirToPlayer.magnitude, _targerMask);
        if (ray)
        {
            if (_hit.collider.gameObject.CompareTag("Player"))
            {
                _canSeePlayer = true;
            }
            else
            {
                _canSeePlayer = false;
            }
        }
    }

    protected void FindVisibleTargets()
    {
        _targetsInViewRadius = Physics.OverlapSphere(transform.position, _maxRadius, _targerMask);
        for (int i = 0; i < _targetsInViewRadius.Length; i++)
        {
            _target = _targetsInViewRadius[i].transform;
            _dirToTarget = (_target.position - transform.position).normalized;
            _targerAngle = Vector3.Angle(transform.forward, _dirToTarget);

            if (_targerAngle > -_maxAngle && _targerAngle < _maxAngle && _canSeePlayer && _target.gameObject.CompareTag("Player"))
            {
                _visibleTarget = _target;
            }
        }
    }

    protected void CheckinMinDistanceToPlayer()
    {
        if ((transform.position - _playerTransform.position).sqrMagnitude < START_SEASRCH_DISTANCE * START_SEASRCH_DISTANCE)
        {
            DrawRay();
            StartCoroutine(nameof(FindTargets), _findTargetsDelay);
        }
    }

    private IEnumerator FindTargets(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void CheckTarget()
    {
        if (!_visibleTarget)
        {
            _agrTarget = _playerTransform;
        }
        else
        {
            _agrTarget = _visibleTarget;
        }
    }

    #endregion


    #region IGetDamage

    public void GetDamage(int damage)
    {
        ChangeHealth(damage);
        _healthTxt.text = _health.ToString();

        if (_health > 0)
        {
            PursueTarget(_playerTransform);
        }
        else
        {
            Die();
        }
    }

    #endregion
}
