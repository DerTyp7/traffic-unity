using UnityEngine;

public class StreetBuilding : MonoBehaviour
{

    GameObject currentStreetPO;
    Vector3 startPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentStreetPO == null)
            {
                StartBuilding();
            }
            else
            {
                StopBuilding();
            }
        }

        if (currentStreetPO != null && Input.GetKeyDown(KeyCode.J))
        {
            if (currentStreetPO.GetComponent<PlaceableObject>().placeable)
            {
                PlaceBuilding();
            }

        }
    }

    private void StartBuilding()
    {
        startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition.z = 0;

        currentStreetPO = Instantiate(PrefabDictionary.instance.oneWayStreetPO, startPosition, Quaternion.identity);
    }

    private void StopBuilding()
    {
        Destroy(currentStreetPO);
        currentStreetPO = null;
        startPosition = Vector3.zero;
    }

    private void PlaceBuilding()
    {
        Transform placedTransform = currentStreetPO.transform;

        Instantiate(PrefabDictionary.instance.oneWayStreet, placedTransform.position, placedTransform.rotation).transform.localScale = placedTransform.localScale;

        StopBuilding();
    }
}
