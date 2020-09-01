using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class BigGhost : MovingEnemy
{
    #region Fields

    [SerializeField] private bool _isCanCharge;
    [SerializeField] private bool _isCharging;
    [SerializeField] private bool _isCanReloadCharge;
    [SerializeField] private float _chargeSpeed;
    [SerializeField] private float _chargeRangeMultiplier;

    private float _chargeCoolDown;
    private float _currentChargeCoolDown;
    private float _currentChargeTime;
    private float _chargeTime;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        _isCanCharge = true;
        _chargeCoolDown = 5f;
        _chargeTime = 1f;
        _currentChargeTime = 0f;
        _currentChargeCoolDown = 0f;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        CheckCurrentCharge();
        CheckChargeReload();
    }

    #endregion


    #region Methods

    private void CheckChargeReload()
    {
        if (!_isCanCharge && _isCanReloadCharge)
        {
            _currentChargeCoolDown += Time.deltaTime;
            if (_currentChargeCoolDown >= _chargeCoolDown)
            {
                _isCanReloadCharge = false;
                _isCanCharge = true;
                _currentChargeCoolDown = 0f;
            }
        }
    }

    private void CheckCurrentCharge()
    {
        if (_isCharging)
        {
            _currentChargeTime += Time.deltaTime;
            if (_currentChargeTime >= _chargeTime)
            {
                _isCharging = false;
                _isCanReloadCharge = true;
                _currentChargeTime = 0;
            }
        }
    }

    protected override void PursueTarget(Transform target)
    {
        base.PursueTarget(target);

        CheckDistanceToCharge();
    }

    private void CheckDistanceToCharge()
    {
        if ((transform.position - _agrTarget.position).sqrMagnitude * _chargeRangeMultiplier > _pursuingStopDistance * _pursuingStopDistance)
        {
            if (_isCanCharge)
            {
                Charge();
            }
        }
    }

    private void Charge()
    {
        _agentNavMesh.speed = _chargeSpeed;
        _isCanCharge = false;
        _isCharging = true;
    }

    #endregion
}
