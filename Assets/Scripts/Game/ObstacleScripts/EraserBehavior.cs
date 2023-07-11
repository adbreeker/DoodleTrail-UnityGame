using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserBehavior : MonoBehaviour
{
    public float speed;

    int moveCounter = 0;

    private void Update()
    {
        Erase();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Erase()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D eraseFilter = new ContactFilter2D().NoFilter();
        GetComponent<PolygonCollider2D>().OverlapCollider(eraseFilter, colliders);

        foreach(Collider2D collider in colliders)
        {
            if(collider.tag == "Brush")
            {
                Destroy(collider.gameObject);
            } 
        }
           
    }

    void Move()
    {
        moveCounter++;
        transform.position += transform.right * speed;

        if (moveCounter == 100)
        {
            moveCounter = -100;
            speed *= -1;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
