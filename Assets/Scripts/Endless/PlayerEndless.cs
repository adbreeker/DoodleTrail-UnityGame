using UnityEngine;

public class PlayerEndless : MonoBehaviour
{
    Rigidbody2D rb;
    WheelJoint2D wj;
    public float maxAngularVelocity;

    public GameObject playerPointer;
    public EndlessGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        wj = gameObject.GetComponent<WheelJoint2D>();
    }

    // Update is called once per frame
    void Update()
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

    private void FixedUpdate()
    {
        if (transform.position.y < -20)
        {
            gameManager.FailGame(0f);
        }
    }

    public void StartPlayer()
    {
        UnFreeze();
        wj.useMotor = true;
    }

    public void Freeze()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnFreeze()
    {
        rb.constraints = RigidbodyConstraints2D.None;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            SoundManager.Instance.PlaySound(SoundEnum.EFFECT_CATCH);
            gameManager.StarCollected();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Destroyer")
        {
            GetComponentInChildren<PlayerBreak>().Break(transform.position, rb.linearVelocity, 10.0f);
            Destroy(playerPointer);
            Destroy(gameObject);
            gameManager.FailGame(0.5f);
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
