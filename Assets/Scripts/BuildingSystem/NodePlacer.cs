using UnityEngine;

public class NodePlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject nodeObject;

    [SerializeField]
    private TrafficNode selectedPlacedNode;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("H2213123IT");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("HIT");
                selectedPlacedNode = hit.collider.transform.gameObject.GetComponent<TrafficNode>();
            }
            else
            {
                selectedPlacedNode = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            TrafficNode newNode = Instantiate(nodeObject, newPosition, Quaternion.identity).GetComponent<TrafficNode>();

            if (selectedPlacedNode != null)
            {
                selectedPlacedNode.AddNextNode(newNode);

            }

        }
    }
}
