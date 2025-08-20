using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SharpenerGravitationArea : MonoBehaviour
{
    public List<Collider2D> affectedObjects = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            affectedObjects.Add(collision);
            collision.GetComponent<Rigidbody2D>().linearVelocity *= 0.9f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            affectedObjects.Remove(collision);
        }
    }
}
