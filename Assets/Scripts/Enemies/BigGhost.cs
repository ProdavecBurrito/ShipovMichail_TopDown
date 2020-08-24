using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEditor;


[RequireComponent(typeof(NavMeshAgent))]
public class BigGhost : Ghost
{

    [SerializeField] private float _chargeSpeed;
    private float _chargeCoolDown;
    private float _currentChargeCoolDown;
    private float _currentChargeTime;
    private float _chargeTime;
    [SerializeField] private bool _isCanCharge;
    [SerializeField] private bool _isCharging;
    [SerializeField] private bool _isCanReloadCharge;

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

    protected override void Attack(Transform target)
    {
        _isPatroling = false;
        _isAggressive = true;
        if (!_isCharging)
        {
            _agentNavMesh.speed = _pursueMoveSpeed;
        }
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

        if ((transform.position - _agrTarget.position).sqrMagnitude * 2 > _pursuingStopDistance * _pursuingStopDistance)
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

}
