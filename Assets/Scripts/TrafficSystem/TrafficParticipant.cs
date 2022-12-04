using UnityEngine;

public class TrafficParticipant : MonoBehaviour
{
    [SerializeField]
    private TrafficNode nextNode;

    [SerializeField]
    private TrafficNode currentNode;

    private bool onNodeNetwork = false;


    private float speed = 1f;

    private void Update()
    {
        if (currentNode)
        {
            speed = currentNode.GetSpeed();

            if (onNodeNetwork)
            {
                if (nextNode != null)
                {
                    transform.position = Vector3.MoveTowards(transform.position, nextNode.transform.position, speed * Time.deltaTime);
                    if (transform.position == nextNode.transform.position)
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
            else // Align to node network
            {
                transform.position = Vector3.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);
                if (transform.position == currentNode.transform.position)
                {
                    onNodeNetwork = true;
                    ChooseNextNode();
                }
            }

        }
        else
        {
            currentNode = GetNearestNode();
        }

    }

    private TrafficNode GetNearestNode()
    {
        GameObject[] allTrafficNodes = GameObject.FindGameObjectsWithTag("TrafficNode");

        if (allTrafficNodes.Length > 0)
        {
            GameObject nearestNodeObject = allTrafficNodes[0];

            foreach (GameObject nodeObject in GameObject.FindGameObjectsWithTag("TrafficNode"))
            {
                if ((transform.position - nodeObject.transform.position).sqrMagnitude < (transform.position - nearestNodeObject.transform.position).sqrMagnitude)
                {
                    nearestNodeObject = nodeObject;
                }
            }
            return nearestNodeObject.GetComponent<TrafficNode>();
        }
        return null;
    }

    private void Arrived()
    {
        currentNode = nextNode;
        nextNode = null;
    }

    private void ChooseNextNode()
    {
        if (nextNode == null)
        {
            nextNode = currentNode.GetNextTrafficNodes()[Random.Range(0, (currentNode.GetNextTrafficNodes().Count))];
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
