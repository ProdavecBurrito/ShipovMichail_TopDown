using UnityEditor;
using UnityEngine;


public class Vision : MonoBehaviour
{
#if UNITY_EDITOR

    private BaseEnemy _enemy;
    private float _maxAngle;
    private float _maxRadius;

    private void Start()
    {
        _enemy = GetComponent<BaseEnemy>();
        _maxAngle = _enemy.MaxAngle;
        _maxRadius = _enemy.MaxRadius;
    }

    protected void OnDrawGizmos()
    {
        Vector3 pos = transform.position + Vector3.up;

        Handles.color = new Color(1.0f, 0.0f, 1.0f, 0.3f);
        Handles.DrawSolidArc(pos, Vector3.up, transform.forward, _maxAngle, _maxRadius);
        Handles.DrawSolidArc(pos, Vector3.up, transform.forward, -_maxAngle, _maxRadius);
    }

#endif
}
