using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    WheelJoint2D wj;
    public int StartMode = 1;
    public float posXMove;
    public float posYMove;
    Vector2 direction = new Vector2(1, 0);

    public GameObject playerPointer = null;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        wj = gameObject.GetComponent<WheelJoint2D>();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + posXMove, gameObject.transform.position.y + posYMove, 0);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPlayer()
    {
        UnFreeze();
        if (StartMode == 1)
        {
            wj.useMotor = true;
        }
        if(StartMode == -1)
        {
            wj.useMotor = false;
            rb.AddForce(direction*50, ForceMode2D.Impulse);
        }
    }

    public void Freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnFreeze()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    public void ReverseForce()
    {
        //cannon
        direction *= -1;

        //engine
        JointMotor2D motor = wj.motor;
        motor.motorSpeed *= -1;
        wj.motor = motor;

        //direction arrow
        gameObject.GetComponentInChildren<ArrowRotator>().Rotate();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            Freeze();
            FindObjectOfType<GameManager>().LvLCompleted();
        }
        if(collision.gameObject.tag == "Star")
        {
            gameManager.starCollected = true;
            Destroy(collision.gameObject);
        }
    }

    public void OnBecameInvisible()
    {
        if (playerPointer != null)
        {
            playerPointer.SetActive(true);
        }
    }

    public void OnBecameVisible()
    {
        if (playerPointer != null)
        {
            playerPointer.SetActive(false);
        }
    }

}
