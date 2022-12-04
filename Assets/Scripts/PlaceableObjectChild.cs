using UnityEngine;

public class PlaceableObjectChild : MonoBehaviour
{

    public bool placeable = true;

    private void Update()
    {
        transform.localPosition = new Vector3(0.5f, 0f, 0f);
        if (placeable)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        placeable = false;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        placeable = true;
    }
}
