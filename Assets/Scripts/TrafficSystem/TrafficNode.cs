using System.Collections.Generic;
using UnityEngine;

public class TrafficNode : MonoBehaviour
{
    [SerializeField]
    private List<TrafficNode> nextTrafficNodes = new List<TrafficNode>();

    [SerializeField]
    private float speed = 5.0f;


    public Color gizmoSphereColor = Color.green;

    private Street parentStreet;

    public float GetSpeed() { return speed; }
    public List<TrafficNode> GetNextTrafficNodes() { return nextTrafficNodes; }
    public void SetParentStreet(Street newParentStreet) { parentStreet = newParentStreet; }
    public Street GetParentStreet() { return parentStreet; }

    public void AddNextNode(TrafficNode newNode)
    {
        nextTrafficNodes.Add(newNode);
    }

    public void RemoveNextNode(TrafficNode node)
    {
        nextTrafficNodes.Remove(node);
    }

    public void LateUpdate()
    {
        if (transform.parent != null)
        {
            Vector3 parentScale = transform.parent.lossyScale;
            transform.localScale = new Vector3(1f / parentScale.x, 1f / parentScale.y,
                    1f / parentScale.z);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoSphereColor;
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
