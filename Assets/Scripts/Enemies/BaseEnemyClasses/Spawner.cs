using UnityEngine;


public class Spawner : MonoBehaviour
{
    #region Fields

    [SerializeField] private MovingEnemy _enemy;
    [SerializeField] private Transform _wayPoint;

    private MovingEnemy enemyCopy;

    #endregion


    #region Methods

    public void Spawn()
    {
        enemyCopy = Instantiate(_enemy, transform.position, Quaternion.identity);
        enemyCopy.GetWayPoint(_wayPoint);
        Destroy(gameObject);
    }

    #endregion
}
