using UnityEngine;

public class CrowMovement : MonoBehaviour
{
    public float speed = 5f;
    private float leftBoundary;
    private float rightBoundary;
    private int direction = 1;
    private float spriteWidth;

    private SpriteRenderer sr;

    void Start()
    {
        Camera mainCamera = Camera.main;

        sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x / 2;

        leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteWidth;

        rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteWidth;
    }

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
}