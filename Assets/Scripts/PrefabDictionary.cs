using UnityEngine;
public class PrefabDictionary : MonoBehaviour
{
    public static PrefabDictionary instance;

    public GameObject nodePrefab;
    public GameObject oneWayStreetPO;
    public GameObject oneWayStreet;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
