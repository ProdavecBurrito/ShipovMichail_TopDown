using UnityEngine;


public abstract class BaseExplosive : MonoBehaviour
{
    #region Constants

    protected const float TIME_TILL_OBJECT_DESTROY = 0.15f;

    #endregion


    #region Fields

    [SerializeField] private float _force;
    [SerializeField] private int _damage;
    [SerializeField] private float _explosionRadius;

    protected IGetDamageable _enemy;
    protected Rigidbody _enemyRigidBody;
    protected Collider[] _collidersInRange;
    protected ParticleSystem _particleSystem;

    #endregion


    #region UnityMethods

    protected virtual void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    #endregion


    #region Methods

    protected virtual void Explode()
    {
        _collidersInRange = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider colliders in _collidersInRange)
        {
            if (colliders.CompareTag("Enemy"))
            {
                _enemyRigidBody = colliders.GetComponent<Rigidbody>();
                _enemy = colliders.gameObject.GetComponent<IGetDamageable>();
                _enemy.GetDamage(_damage);
                _enemyRigidBody.AddForce((_enemyRigidBody.transform.position - transform.position).normalized * _force, ForceMode.Impulse);
            }
        }
        _particleSystem.Play();
        Destroy(gameObject, TIME_TILL_OBJECT_DESTROY);
    }

    protected void SetDamage(IGetDamageable target)
    {
        target?.GetDamage(_damage);
    }

    #endregion
}
