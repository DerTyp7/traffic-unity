using UnityEngine;
public class PrefabDictionary : MonoBehaviour
{
    public static PrefabDictionary instance;

    public GameObject nodePrefab;
    public GameObject oneWayStreetPSO;
    public GameObject oneWayStreet;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
