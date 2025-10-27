using UnityEngine;

public class PinMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public AudioClip popSound;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Balloon"))
        {
            int score = 5;

            GameManager.instance.AddScore(score);

            AudioSource.PlayClipAtPoint(popSound, transform.position);

            Destroy(other.gameObject);
            Destroy(gameObject);

            GameManager.instance.BalloonPopped();
        }
        else if (other.gameObject.CompareTag("Crow"))
        {
            Destroy(gameObject);
        }
    }
}
