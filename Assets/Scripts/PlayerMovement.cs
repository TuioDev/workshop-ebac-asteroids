using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerAcceleration = 3.0f;
    private Rigidbody2D playerRB;
    private float rotationSpeed = 180.0f;
    private float maxVelocity = 7.0f;
    private float playerHorizontalInput = 0.0f;

    //Bullet cooldown properties
    private float fireRate = 0.5F;
    private float nextFire = 0.0F;

    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D prefabProjectile;
    private float projectileSpeed = 10.0f;

    [SerializeField] private GameAudioManager gameAudioManager;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerInputs();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Rigidbody2D projectile = Instantiate(prefabProjectile, firePoint.position, transform.rotation);
            projectile.velocity = transform.up * projectileSpeed;
            gameAudioManager.PlaySound("laserSound");
        }
    }

    void FixedUpdate()
    {
        //Straight movement key
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerImpulse();
        }
        //Left and right keys for rotation
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            playerRotation(playerHorizontalInput);
        }
        //Set a max value for the player velocity
        if(playerRB.velocity.magnitude > maxVelocity)
        {
            playerRB.velocity = Vector2.ClampMagnitude(playerRB.velocity, maxVelocity);
        }
    }

    //Handle the player inputs
    void playerInputs()
    {
        playerHorizontalInput = Input.GetAxisRaw("Horizontal");
    }

    //Creates an impulse for the player in the up direction of the object
    void playerImpulse()
    {
        Vector3 accelerationForce = transform.up * playerAcceleration;
        playerRB.AddForce(accelerationForce, ForceMode2D.Force);
    }

    //Manages player rotation
    void playerRotation(float number)
    {
        playerRB.rotation -= rotationSpeed * number * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameAudioManager.PlaySound("playerExplosionSound");
        Destroy(this.gameObject);
        Time.timeScale = 0;
    }
}
