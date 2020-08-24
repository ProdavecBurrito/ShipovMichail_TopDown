using UnityEngine;


public class Bullet : MonoBehaviour
{

    #region Constants

    private const float SPEED = 12f;
    private const float DESTROY_TIME = 1f;

    #endregion

    
    #region PrivateFields

    [SerializeField] private int _dmg;
    private float _currentLiveTime = 0;

    #endregion


    #region Preferences

    public int Dmg => _dmg;

    #endregion


    #region UnityMethods

    private void Update()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            SetDmg(other.GetComponent<IGetDamage>());
        }
        Destroy(gameObject);
    }

    #endregion


    #region Methods

    protected virtual void SetDmg(IGetDamage obj)
    {
        if (obj != null)
        {
            obj.GetDamage(_dmg);
        }
    }

    #endregion

}
