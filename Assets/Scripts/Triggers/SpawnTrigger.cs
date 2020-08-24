using UnityEngine;
using System.Collections.Generic;


public class SpawnTrigger : MonoBehaviour
{

    #region PrivateFields

    [SerializeField] private List<Spawner> _spawnList;

    #endregion


    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Spawner T in _spawnList)
            {
                T.Spawn();
            }
            Destroy(gameObject);
        }
    }

    #endregion

}
