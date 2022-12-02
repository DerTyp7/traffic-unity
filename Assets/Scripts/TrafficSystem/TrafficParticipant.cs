using UnityEngine;

public class TrafficParticipant : MonoBehaviour
{
    [SerializeField]
    private TrafficNode nextNode;

    [SerializeField]
    private TrafficNode currentTrafficNode;

    [SerializeField]
    private float speed = 5f;

    private void Start()
    {
        if (nextNode == null)
        {
            ChooseNextNode();
        }
    }
    private void Update()
    {
        if (nextNode != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, speed * Time.deltaTime); if (transform.position == nextNode.transform.position)
            {
                Arrived();
                ChooseNextNode();
            }
        }
        else
        {
            ChooseNextNode();
        }



    }

    private void Arrived()
    {
        Debug.Log("Arrived");
        currentTrafficNode = nextNode;
        nextNode = null;
    }

    private void ChooseNextNode()
    {
        Debug.Log("Choose next node");
        if (nextNode == null)
        {
            nextNode = currentTrafficNode.GetNextTrafficNodes()[Random.Range(0, (currentTrafficNode.GetNextTrafficNodes().Count))];
        }
    }

    private void OnDrawGizmos()
    {
        if (nextNode != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(transform.position, nextNode.transform.position);
        }
    }
}
