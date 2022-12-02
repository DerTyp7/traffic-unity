using UnityEngine;
public class PrefabDictionary : MonoBehaviour
{
    public static PrefabDictionary instance;

    public GameObject nodePrefab;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
