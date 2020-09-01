using UnityEngine;
using System.Collections.Generic;


public class WayPointDebug : MonoBehaviour
{
#if UNITY_EDITOR // For debug

    #region Fields

    public List<Transform> Nodes = new List<Transform>();
    public Vector3 CurrentNode;
    public Vector3 PrevNode;
    public int NodesCount;

    #endregion


    #region UnityMethods

    private void OnDrawGizmos()
    {
        CheckWhatNeedRecountNodes();
        AddNodes();
        DrawLine();
    }

    #endregion


    #region Methods

    private void AddNodes()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform T in transform)
            {
                if (!Nodes.Contains(T))
                {
                    Nodes.Add(T);
                }
                NodesCount++;
            }
        }
    }

    private void DrawLine()
    {
        if (Nodes.Count >= 1)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                CurrentNode = Nodes[i].position;
                if (i > 0)
                {
                    PrevNode = Nodes[i - 1].position;
                }
                else if (i == 0)
                {
                    PrevNode = Nodes[Nodes.Count - 1].position;
                }
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(PrevNode, CurrentNode);
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(CurrentNode, Vector3.one / 2);
            }
        }
    }

    private void CheckWhatNeedRecountNodes()
    {
        if (transform.childCount != NodesCount)
        {
            Nodes.Clear();
            NodesCount = 0;
        }
    }

    #endregion

#endif
}

