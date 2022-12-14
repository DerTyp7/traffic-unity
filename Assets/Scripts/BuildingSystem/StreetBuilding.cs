using UnityEngine;

public class StreetBuilding : MonoBehaviour
{

    GameObject currentStreetPSO;
    Vector3 startPosition;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (currentStreetPSO == null)
            {
                StartBuilding();
            }
            else
            {
                StopBuilding();
            }
        }

        if (currentStreetPSO != null)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (currentStreetPSO.GetComponent<PlaceableStreetObject>().placeable)
                {
                    PlaceBuilding();
                }
            }
        }
    }


    private void StartBuilding()
    {
        startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition.z = 0;

        currentStreetPSO = Instantiate(PrefabDictionary.instance.oneWayStreetPSO, startPosition, Quaternion.identity);
    }

    private void StopBuilding()
    {
        Destroy(currentStreetPSO);
        currentStreetPSO = null;
        startPosition = Vector3.zero;
    }

    private void PlaceBuilding()
    {
        Transform placedTransform = currentStreetPSO.transform;

        GameObject newStreet = Instantiate(PrefabDictionary.instance.oneWayStreet, placedTransform.position, placedTransform.rotation);
        newStreet.transform.localScale = placedTransform.localScale;
        newStreet.GetComponent<StreetContainer>().Place();

        StopBuilding();
    }
}
