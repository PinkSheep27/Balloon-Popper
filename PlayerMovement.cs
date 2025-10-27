using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject pinPrefab;
    public Transform firePoint;

    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool isFacingRight = true;

    private Camera mainCamera;
    private float leftBoundary;
    private float rightBoundary;
    private float topBoundary;
    private float bottomBoundary;
    private float spriteWidth;
    private float spriteHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        mainCamera = Camera.main;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x / 2;
        spriteHeight = sr.bounds.size.y / 2;

        leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + spriteWidth;
        rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - spriteWidth;
        bottomBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + spriteHeight;

        float cameraTopBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - spriteHeight;
        float customTopBoundary = -3.1f;

        topBoundary = Mathf.Min(cameraTopBoundary, customTopBoundary);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f)
        {
            movementInput = Vector2.zero;
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(moveX, moveY).normalized;

        if (moveX > 0 && !isFacingRight) Flip();
        else if (moveX < 0 && isFacingRight) Flip();
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    private void FixedUpdate()
    {
        Vector2 newPos = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;

        newPos.x = Mathf.Clamp(newPos.x, leftBoundary, rightBoundary);
        newPos.y = Mathf.Clamp(newPos.y, bottomBoundary, topBoundary);

        rb.MovePosition(newPos);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }
    void Shoot()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Instantiate(pinPrefab, firePoint.position, rotation);
    }
}
