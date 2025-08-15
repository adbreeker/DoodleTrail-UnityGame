using System.Collections;
using UnityEngine;



public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    WheelJoint2D wj;
    public int StartMode = 1;
    public float maxAngularVelocity;
    public PhysicsMaterial2D engineFriction, cannonFriction;
    Vector2 direction = new Vector2(1, 0);

    public GameObject playerPointer = null;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        wj = gameObject.GetComponent<WheelJoint2D>();

        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(StartMode == 1)
        {
            if (rb.angularVelocity > maxAngularVelocity)
            {
                rb.angularVelocity = maxAngularVelocity;
            }
            if (rb.angularVelocity < -maxAngularVelocity)
            {
                rb.angularVelocity = -maxAngularVelocity;
            }
        }
    }

    private void FixedUpdate()
    {
        if(transform.position.y < -20)
        {
            gameManager.StartCoroutine("LvLFailed", 0f);
        }
    }

    public void StartPlayer()
    {
        UnFreeze();
        if (StartMode == 1)
        {
            GetComponent<CircleCollider2D>().sharedMaterial = engineFriction;
            wj.useMotor = true;
            SoundManager.Instance.PlaySound(SoundEnum.EFFECT_ENGINE);
        }
        if (StartMode == -1)
        {
            GetComponent<CircleCollider2D>().sharedMaterial = cannonFriction;
            wj.useMotor = false;
            rb.AddForce(direction * 50, ForceMode2D.Impulse);
            SoundManager.Instance.PlaySound(SoundEnum.EFFECT_CANNON);
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
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Freeze();
            FindFirstObjectByType<GameManager>().LvLCompleted();
        }
        if (collision.gameObject.tag == "Star")
        {
            SoundManager.Instance.PlaySound(SoundEnum.EFFECT_CATCH);
            gameManager.starCollected = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Destroyer")
        {
            GetComponentInChildren<PlayerBreak>().Break(transform.position, rb.linearVelocity, 10.0f);
            Destroy(playerPointer);
            Destroy(gameObject);
            gameManager.StartCoroutine("LvLFailed", 0.5f);
        }
    }

    void OnBecameInvisible()
    {
        if (playerPointer != null)
        {
            playerPointer.SetActive(true);
        }
    }

    void OnBecameVisible()
    {
        if (playerPointer != null)
        {
            playerPointer.SetActive(false);
        }
    }

}
