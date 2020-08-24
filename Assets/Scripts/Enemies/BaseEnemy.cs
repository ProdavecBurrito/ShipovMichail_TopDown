using UnityEngine;
using UnityEngine.AI;


public abstract class BaseEnemy : MonoBehaviour, IGetDamage
{

    #region ProtectedFields

    [SerializeField] protected int _health;
    [SerializeField] protected float _maxAngle;
    [SerializeField] protected float _maxRadius;
    [SerializeField] protected float _dieTime;
    [SerializeField] protected float _timeWait;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected bool _isPatroling;
    [SerializeField] protected bool _isAggressive;
    [SerializeField] protected Transform _visibleTarget;
    [SerializeField] protected LayerMask _targerMask;
    [SerializeField] protected LayerMask _obstacleMask;
    [SerializeField] protected int _attackDmg;

    protected bool _isAlive;
    protected Transform _playerTransform;
    protected NavMeshAgent _agentNavMesh;
    protected float _timeOut;
    protected float _targerAngle;
    protected float _distToTarget;
    protected float _currentReload;
    protected Transform _target;
    protected Vector3 _dirToTarget;
    protected Transform _agrTarget;

    #endregion


    #region Methods

    protected virtual void Die()
    {

    }

    protected virtual void ChangeHealth(int damage)
    {

    }

    protected virtual void Patrol()
    {

    }

    protected virtual void SetDamage(IGetDamage obj)
    {

    }

    protected virtual void Attack(Transform target)
    {

    }

    protected virtual void FindVisibleTargets()
    {

    }

    public virtual void GetDamage(int damage)
    {

    }

    public virtual void GetWayPoints(Transform points)
    {
        
    }

    #endregion

}
