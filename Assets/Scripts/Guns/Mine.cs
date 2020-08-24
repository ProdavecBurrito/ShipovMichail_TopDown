using UnityEngine;
using System.Collections.Generic;


public class Mine : MonoBehaviour
{

    #region PrivateFields

    [SerializeField] private int _damage;

    private Animator _animator;
    private ParticleSystem _particleSystem;
    private List<IGetDamage> _targets = new List<IGetDamage>();

    #endregion


    #region UnityMethods

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            _targets.Add(other.GetComponent<IGetDamage>());

            Jump();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            _targets.Remove(other.GetComponent<IGetDamage>());

        }
    }

    #endregion


    #region Methods

    private void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    private void SetDamage(IGetDamage target)
    {
        if (target != null)
        {
            target.GetDamage(_damage);
        }
    }

    private void Explode()
    {
        _particleSystem.Play();
        foreach (IGetDamage T in _targets)
        {
            SetDamage(T);
        }
        Destroy(gameObject, 0.1f);
    }

    #endregion

}
