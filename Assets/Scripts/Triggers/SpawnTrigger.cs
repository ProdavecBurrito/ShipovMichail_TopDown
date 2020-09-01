using UnityEngine;
using System.Collections.Generic;


public class SpawnTrigger : MonoBehaviour
{

    #region Fields

    [SerializeField] private List<Spawner> _spawnList;

    #endregion


    #region Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Spawner entity in _spawnList)
            {
                entity.Spawn();
            }
            Destroy(gameObject);
        }
    }

    #endregion

}
