using UnityEngine;


public class Mine : BaseExplosive
{
    #region Fields

    private Animator _animator;

    #endregion


    #region UnityMethods

    protected override void Start()
    {
        base.Start();
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Jump();
        }
    }

    #endregion


    #region Methods

    private void Jump()
    {
        _animator.SetTrigger("Jump");
    }

    protected override void Explode()
    {
        base.Explode();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    #endregion
}
