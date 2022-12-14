using UnityEngine;

public class PlaceableStreetObject : MonoBehaviour
{
    public GameObject childObject;
    public bool placeable = true;
    TrafficNode snappingNode;

    private void Update()
    {
        UpdateSnappingNode();
        ScaleOnMousePosition();
        RotateToMousePosition();
        placeable = childObject.GetComponent<PlaceableStreetObjectChild>().placeable;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void UpdateSnappingNode()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);


        // Snapping
        TrafficNode foundNode = null;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "TrafficNode")
                {
                    foundNode = hit.collider.gameObject.GetComponent<TrafficNode>();
                }
            }
        }

        if (snappingNode != null)
        {
            snappingNode.gizmoSphereColor = Color.green;
        }

        snappingNode = foundNode;

        if (snappingNode != null)
        {
            snappingNode.gizmoSphereColor = Color.red;
        }

        childObject.GetComponent<PlaceableStreetObjectChild>().snappingNode = snappingNode;
    }


    Vector3 GetEndPosition()
    {
        PlaceableStreetObjectChild child = childObject.GetComponent<PlaceableStreetObjectChild>();
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 endPosition = mousePosition;


        // Snapping
        if (snappingNode != null)
        {
            endPosition.x = snappingNode.gameObject.transform.position.x;
            endPosition.y = snappingNode.gameObject.transform.position.y;
        }

        endPosition.z = 0;
        return endPosition;
    }

    void RotateToMousePosition()
    {
        Vector3 endPosition = GetEndPosition();
        float angle = AngleBetweenTwoPoints(transform.position, endPosition);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    void ScaleOnMousePosition()
    {
        Vector3 endPosition = GetEndPosition();
        transform.localScale = new Vector3(-Vector3.Distance(endPosition, transform.position), 1, 1);
    }


}
