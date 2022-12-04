using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public GameObject childObject;
    public bool placeable = true;


    private void Update()
    {
        ScaleOnMousePosition();
        RotateToMousePosition();

        placeable = childObject.GetComponent<PlaceableObjectChild>().placeable;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void RotateToMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float angle = AngleBetweenTwoPoints(transform.position, mousePosition);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
    void ScaleOnMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        transform.localScale = new Vector3(-Vector3.Distance(mousePosition, transform.position), 1, 1);
    }


}
