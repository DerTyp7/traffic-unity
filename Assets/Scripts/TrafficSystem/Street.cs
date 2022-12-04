using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    private List<TrafficNode> nodes = new List<TrafficNode>();

    private void Start()
    {
        nodes.Add(Instantiate(PrefabDictionary.instance.nodePrefab, transform).GetComponent<TrafficNode>());
        nodes.Add(Instantiate(PrefabDictionary.instance.nodePrefab, transform).GetComponent<TrafficNode>());
        nodes[0].transform.localPosition = new Vector3(-0.5f, 0, 0);
        nodes[1].transform.localPosition = new Vector3(0.5f, 0, 0);

        nodes[0].AddNextNode(nodes[1]);

        /*CreateNode(new Vector3(0, 0, 0));
        CreateNode(new Vector3(-0.25f, 0, 0));
        RemoveNode(CreateNode(new Vector3(0.33f, 0, 0)));
        CreateNode(new Vector3(-0.12f, 0, 0));*/
    }

    private TrafficNode[] getNearestNodes(float positionX)
    {
        TrafficNode[] nearestNodes = new TrafficNode[2];
        nearestNodes[0] = nodes[0];
        nearestNodes[1] = nodes[1];
        foreach (TrafficNode node in nodes)
        {
            float distance = Mathf.Abs(node.transform.localPosition.x - positionX);

            if (distance < Mathf.Abs(nearestNodes[0].transform.localPosition.x - positionX) && node.transform.localPosition.x < positionX)
            {
                nearestNodes[0] = node;
            }

            if (distance < Mathf.Abs(nearestNodes[1].transform.localPosition.x - positionX) && node.transform.localPosition.x > positionX)
            {
                nearestNodes[1] = node;
            }
        }
        return nearestNodes;
    }

    public TrafficNode CreateNode(Vector3 localPosition)
    {
        TrafficNode[] nearestNodes = getNearestNodes(localPosition.x);

        TrafficNode newNode = Instantiate(PrefabDictionary.instance.nodePrefab, transform).GetComponent<TrafficNode>();
        newNode.transform.localPosition = localPosition;

        nearestNodes[0].RemoveNextNode(nearestNodes[1]);
        nearestNodes[0].AddNextNode(newNode);
        newNode.AddNextNode(nearestNodes[1]);
        nodes.Insert(nodes.IndexOf(nearestNodes[1]), newNode);
        return newNode;
    }

    public void RemoveNode(TrafficNode node)
    {
        TrafficNode[] nearestNodes = getNearestNodes(node.transform.localPosition.x);

        nearestNodes[0].RemoveNextNode(node);

        foreach (TrafficNode n in node.GetNextTrafficNodes())
        {
            if (nodes.Contains(n))
            {
                nearestNodes[0].AddNextNode(n);
            }
        }

        nodes.Remove(node);
        Destroy(node.gameObject);
    }
}
