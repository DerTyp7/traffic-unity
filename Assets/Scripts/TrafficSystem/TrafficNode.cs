using System.Collections.Generic;
using UnityEngine;

public class TrafficNode : MonoBehaviour
{
    [SerializeField]
    private List<TrafficNode> nextTrafficNodes = new List<TrafficNode>();

    [SerializeField]
    private float speed = 5.0f;

    public float GetSpeed() { return speed; }
    public List<TrafficNode> GetNextTrafficNodes() { return nextTrafficNodes; }

    public void AddNextNode(TrafficNode newNode)
    {
        nextTrafficNodes.Add(newNode);
    }

    public void RemoveNextNode(TrafficNode node)
    {
        nextTrafficNodes.Remove(node);
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
