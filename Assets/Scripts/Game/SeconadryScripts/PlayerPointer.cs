using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    public float speed;
    public float rotationModifier;

    float scrH, scrW;

    // Start is called before the first frame update
    void Start()
    {
        SetBoundaries();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetBoundaries();
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

        transform.position = new Vector2(Mathf.Clamp(player.transform.position.x, -scrW, scrW), Mathf.Clamp(player.transform.position.y, -scrH, scrH));
    }

    void SetBoundaries()
    {
        scrH = (cam.orthographicSize - 0.7f) + cam.transform.position.y;
        scrW = ((cam.orthographicSize * cam.aspect) - 0.7f) + cam.transform.position.x;
    }
}
