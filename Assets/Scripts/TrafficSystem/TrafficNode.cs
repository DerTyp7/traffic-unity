using System.Collections.Generic;
using UnityEngine;

public class TrafficNode : MonoBehaviour
{
    [SerializeField]
    private List<TrafficNode> nextTrafficNodes;

    public List<TrafficNode> GetNextTrafficNodes() { return nextTrafficNodes; }

    public void AddNextNode(TrafficNode node)
    {
        nextTrafficNodes.Add(node);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f);
        foreach (TrafficNode node in nextTrafficNodes)
        {
            if (node != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}
