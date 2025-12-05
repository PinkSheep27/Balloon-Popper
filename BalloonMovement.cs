using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    public float speed = 3f;
    public float leftBoundary;
    public float rightBoundary;

    public float minSize = 0.015f;
    public float growAmount = 0.00025f;
    public float growInterval = 0.1f;

    private int direction = 1;
    private float spriteWidth;

    private SpriteRenderer sr;

    private Animator anim;
    private bool isPopped = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

        Camera mainCamera = Camera.main;

        sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x / 2;

        leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteWidth;

        rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteWidth;

        InvokeRepeating("Grow", growInterval, growInterval);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= rightBoundary)
        {
            direction = -1;
            Flip();
        }
 
        else if (transform.position.x <= leftBoundary)
        {
            direction = 1;
            Flip();
        }
    }
    void Flip()
    {
        sr.flipX = !sr.flipX;
    }

    void Grow()
    {
        transform.localScale -= new Vector3(growAmount, growAmount, 0);

        if (transform.localScale.x <= minSize)
        {
            GameManager.instance.RestartLevel();

            Destroy(gameObject);
        }
    }
    public void TriggerPop()
    {
        if (isPopped) return;
        isPopped = true;

        if (anim != null) anim.SetTrigger("Pop");

        speed = 0;
        CancelInvoke("Grow");

        Destroy(gameObject, 0.5f);
    }
}
