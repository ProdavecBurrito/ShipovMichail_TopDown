using UnityEngine;


public class Grenade : BaseExplosive
{
    #region Constants

    private const float SPEED = 15f;
    private const float  TIME_TILL_EXPLODE = 3.0f;

    #endregion


    #region Fields

    private Transform _startPosition;
    private Transform _playerPosition;
    private Rigidbody _rigidbody;



    private float _currentTimeTillExplode;

    #endregion


    #region Preferences

    public float GrenadeSpeed => SPEED;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        _currentTimeTillExplode = 0f;
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Awake()
    {
        _rigidbody.AddForce((_startPosition.position - _playerPosition.position) * GrenadeSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void Update()
    {
        UpdateGrenadeTimer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    #endregion


    #region Methods

    protected override void Explode()
    {
        base.Explode();
        TurnOffGrenadeMeshParts();
    }

    private void TurnOffGrenadeMeshParts()
    {
        foreach (MeshRenderer grenageParts in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            grenageParts.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    private void UpdateGrenadeTimer()
    {
        _currentTimeTillExplode += Time.deltaTime;
        if (_currentTimeTillExplode >= TIME_TILL_EXPLODE)
        {
            Explode();
        }
    }

    public void GetThrowPosition(Transform throwPosition, Transform playerPosition)
    {
        _startPosition = throwPosition;
        _playerPosition = playerPosition;
    }

    #endregion
}
