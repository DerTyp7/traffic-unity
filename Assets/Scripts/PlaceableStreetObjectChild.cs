using System.Collections.Generic;
using UnityEngine;

public class PlaceableStreetObjectChild : MonoBehaviour
{

    public bool placeable = true;
    public List<Collision2D> currentCollisions = new List<Collision2D>();
    public TrafficNode snappingNode = null;

    private void Update()
    {
        transform.localPosition = new Vector3(0.5f, 0f, 0f);
        CheckPlaceable();
    }

    private void LateUpdate()
    {
        if (placeable)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void CheckPlaceable()
    {
        // DEBUG LOG
        Debug.Log("--------------------------------------");
        foreach (Collision2D collision in currentCollisions)
        {
            Debug.Log(collision.gameObject.name);
        }



        // If a snapping node is present -> its allowed to collide with all node of the SAME street
        if (snappingNode != null)
        {
            bool foundBadCollision = false;
            foreach (Collision2D collision in currentCollisions)
            {
                if (collision.collider)
                {
                    if (collision.collider.transform.tag == "TrafficNode")
                    {
                        if (collision.collider.GetComponent<TrafficNode>().GetParentStreet() != snappingNode.GetParentStreet())
                        {
                            foundBadCollision = true;
                        }
                    }
                    else if (collision.collider.transform.tag == "Street")
                    {
                        if (collision.collider.GetComponent<Street>() != snappingNode.GetParentStreet())
                        {
                            foundBadCollision = true;
                        }
                    }
                    else
                    {
                        Debug.Log("No Street or node");
                        foundBadCollision = true;

                    }
                }
                else
                {
                    Debug.Log("No collider");
                    foundBadCollision = true;
                }
            }
            placeable = !foundBadCollision;
        }
        else
        {
            if (currentCollisions.Count > 0)
            {
                placeable = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        currentCollisions.Add(c);
    }

    private void OnCollisionExit2D(Collision2D c)
    {
        currentCollisions.Remove(c);
    }
}
