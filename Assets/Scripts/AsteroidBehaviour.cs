using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D asteroidRB;
    private GameObject player;

    private float asteroidIntensity = 0.0f;
    private float directionOffSetX = 0.0f;
    private float directionOffSetY = 0.0f;
    private float asteroidTorque = 0.0f;
    private float asteroidSize = 1.0f;


    void Start()
    {
        SetAsteroidProperties();
    }

    private void SetAsteroidProperties()
    {
        //Sets the size of the asteroid
        this.transform.localScale *= asteroidSize;

        //Find the player object to use the position
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
            Invoke("DestroyProjectile", 15f);
        }
        else
        {
            //Avoid errors
            player.transform.position = Vector3.zero;
        }
        //Set a random value for the intensity of the velocity when spawned
        asteroidIntensity = Random.Range(1.0f, 2.0f);

        //Set a random vector to off set the direction for the asteroid
        //so that not always will hit the player position
        directionOffSetX = SetRandomNumber();
        directionOffSetY = SetRandomNumber();
        Vector2 offSet = new Vector2(directionOffSetX, directionOffSetY);

        //Calculate the direction for the asteroid and set the velocity
        Vector2 asteroidDirection = player.transform.position - transform.position;
        asteroidDirection += offSet;
        asteroidDirection.Normalize();
        asteroidRB.velocity = asteroidDirection * asteroidIntensity;

        //Set a random value to the torque
        asteroidTorque = SetRandomNumber() * 10;
        //Give the asteroid a torque so that it rotates
        asteroidRB.AddTorque(asteroidTorque);
    }

    private float SetRandomNumber()
    {
        return Random.Range(-3.0f, 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If a projectile hits the asteroid it will destroy both
        if (collision.tag != "Player")
        {
            //Not the best way, but it works
            GameObject gameAudioManager = GameObject.Find("AudioManager");
            gameAudioManager.GetComponent<GameAudioManager>().PlaySound("asteroidExplosionSound");
            Destroy(collision.gameObject);
        }

        //Checks based on the size if it can create others
        if (this.asteroidSize >= 1)
        {
            CutOnHalf();
            CutOnHalf();
        }
        Destroy(this.gameObject);
    }

    //The function will create two small asteroids from the original
    private void CutOnHalf()
    {
        //Sets the position with different values
        Vector2 newPosition = this.transform.position;
        newPosition += Random.insideUnitCircle * 0.5f;

        //Creates the new asteroid based on the original
        AsteroidBehaviour halfAsteroid = Instantiate(this, newPosition, this.transform.rotation);
        halfAsteroid.asteroidSize *= 0.5f;
        halfAsteroid.asteroidRB.AddTorque(asteroidTorque);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
