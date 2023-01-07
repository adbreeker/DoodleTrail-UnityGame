using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float rotationModifier;

    public float scrH_Modifier, scrW_Modifier;
    float scrH, scrW;

    // Start is called before the first frame update
    void Start()
    {
        scrH = ((Screen.height / Screen.dpi)) + scrH_Modifier;
        scrW = ((Screen.width / Screen.dpi)) + scrW_Modifier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vectorToTarget = player.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

        transform.position = new Vector2(Mathf.Clamp(player.transform.position.x, -scrW, scrW), Mathf.Clamp(player.transform.position.y, -scrH, scrH));
    }
}
