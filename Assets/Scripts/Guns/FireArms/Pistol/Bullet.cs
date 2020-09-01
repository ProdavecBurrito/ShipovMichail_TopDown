using UnityEngine;


public class Bullet : MonoBehaviour
{
    #region Constants

    private const float SPEED = 12f;
    private const float DESTROY_TIME = 1f;

    #endregion

    
    #region Fields

    [SerializeField] private int _damage;
    private float _currentLiveTime;

    #endregion


    #region Properties

    public int Dmg => _damage;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _currentLiveTime = 0;
    }

    private void Update()
    {
        Fly();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            SetDmg(other.GetComponent<IGetDamageable>());
        }
        Destroy(gameObject, 0f);
    }

    #endregion


    #region Methods

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

    private void SetDmg(IGetDamageable obj)
    {
        if (obj != null)
        {
            obj.GetDamage(_damage);
        }
    }

    #endregion
}
