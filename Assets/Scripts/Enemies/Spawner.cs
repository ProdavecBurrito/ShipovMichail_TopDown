using UnityEngine;
using System.Collections.Generic;


public class Spawner : MonoBehaviour
{

    #region PrivateFields

    [SerializeField] private BaseEnemy _enemy;
    [SerializeField] private Transform _wayPoint;

    private List<Transform> _points;
    private BaseEnemy enemyCopy;

    #endregion


    #region UnityMethods

    #endregion


    #region Methods

    public void Spawn()
    {
        enemyCopy = Instantiate(_enemy, transform.position, Quaternion.identity);
        enemyCopy.GetWayPoints(_wayPoint);
        Destroy(gameObject, 0.5f);
    }

    #endregion

}
