using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    private GameObject _objectCopy;
    private int a = 0;
    private int b = 10000;
    private List<GameObject> _go_s = new List<GameObject>();

    void Update()
    {
        while(a < b)
        {
            StartCoroutine(Spawn());
            a++;
        }
    }

    private void LateUpdate()
    {
        foreach (GameObject T in _go_s)
        {
            T.transform.Rotate(0, 1f, 0);
        }
    }

    IEnumerator Spawn()
    {
        _objectCopy = Instantiate(_object);
        _go_s.Add(_objectCopy);
        yield return new WaitForEndOfFrame();
    }
}
