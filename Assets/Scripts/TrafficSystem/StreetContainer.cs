using UnityEngine;

public class StreetContainer : MonoBehaviour
{
    public GameObject streetObject;

    public void Place()
    {
        streetObject.GetComponent<Street>().Place();
    }
}
