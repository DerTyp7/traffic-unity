using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    [SerializeField]
    GameObject nodePrefab;

    private List<TrafficNode> nodes = new List<TrafficNode>();

    public void Place()
    {
        CreateStartEndNodes();


        float parentScaleX = Mathf.Abs(transform.parent.transform.localScale.x);
        float nodeSize = 0.5f;

        int nodeCount = Mathf.RoundToInt(parentScaleX / nodeSize);

        int i = 1; // starts at 1 so the first gets ignored (start node already exists)
        while (i < nodeCount) // NOT <= so endnode does not get created twice ^
        {
            float xPosition = ((i * nodeSize) / parentScaleX) - 0.5f;// weird calc because street is child of object and always has scale of 1f ....
            CreateNode(new Vector3(xPosition, 0, 0));
            i++;
        }
    }

    private void CreateStartEndNodes()
    {
        nodes.Add(Instantiate(nodePrefab, transform).GetComponent<TrafficNode>());
        nodes.Add(Instantiate(nodePrefab, transform).GetComponent<TrafficNode>());
        nodes[0].transform.localPosition = new Vector3(-0.5f, 0, 0);
        nodes[1].transform.localPosition = new Vector3(0.5f, 0, 0);
        nodes[0].SetParentStreet(this);
        nodes[1].SetParentStreet(this);
        nodes[0].AddNextNode(nodes[1]);
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

    // Dont create anything behind the endnode or before the startnode
    public TrafficNode CreateNode(Vector3 localPosition)
    {
        TrafficNode[] nearestNodes = getNearestNodes(localPosition.x);

        TrafficNode newNode = Instantiate(nodePrefab, transform).GetComponent<TrafficNode>();
        newNode.SetParentStreet(this);
        newNode.transform.localPosition = localPosition;

        nearestNodes[0].RemoveNextNode(nearestNodes[1]);

        if (nearestNodes[0] != nodes[nodes.Count - 1])
        {
            nearestNodes[0].AddNextNode(newNode);
        }

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
