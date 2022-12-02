using UnityEngine;

public class NodePlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject nodeObject;

    [SerializeField]
    private TrafficNode prevPlacedNode;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            TrafficNode newNode = Instantiate(nodeObject, newPosition, Quaternion.identity).GetComponent<TrafficNode>();

            if (prevPlacedNode != null)
            {
                prevPlacedNode.AddNextNode(newNode);

            }
            prevPlacedNode = newNode;

        }
    }
}
