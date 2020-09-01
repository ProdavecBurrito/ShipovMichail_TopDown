using UnityEngine;


public class EnemyBullet : MonoBehaviour
{

    #region Constants

    private const float SPEED = 10f;
    private const float DESTROY_TIME = 1f;

    #endregion


    #region Fields

    [SerializeField] private int _damage;
    private float _currentLiveTime = 0;

    #endregion


    #region Properties

    public int Damage => _damage;

    #endregion


    #region UnityMethods

    private void Update()
    {
        Fly();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            SetDamage(other.GetComponent<IGetDamageable>());
        }
        Destroy(gameObject);
    }

    #endregion


    #region Methods

    protected void SetDamage(IGetDamageable obj)
    {
        if (obj != null)
        {
            obj.GetDamage(_damage);
        }
    }

    private void Fly()
    {
        if (_currentLiveTime < DESTROY_TIME)
        {
            transform.Translate(Vector3.forward * SPEED * Time.deltaTime);
            _currentLiveTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

}
