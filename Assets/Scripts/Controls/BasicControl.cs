using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicControl : MonoBehaviour
{

    [SerializeField]
    private float speed = 15.0f;
    private GameObject focalPoint;
    private Rigidbody playerRb;
    public GameObject powerupIndicator;
    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;
    bool smashing = false;
    float floorY;
    private float nextRocketTime;
    private float nextSmashTime;
    public Material[] powerupMaterials;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.Space) && Time.time > nextRocketTime)
        {
            LaunchRockets();
            nextRocketTime = Time.time + 0.25f;
        }
        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing && Time.time > nextSmashTime)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
        if (transform.position.y < -10)
        {
            playerRb.Sleep();
            transform.position = new Vector3(0, 0, 0);
        }
        /* if (transform.position.y < -10 && !GameManager.instance.isGameOver)
        {
            menu.EndGame();
            soundEffect[0].Stop();
            backgroundMusic[1].Stop();
            backgroundMusic[0].Play();
        }
        if (playerRb.velocity.magnitude > 2 && !isSoundPlaying && !smashing)
        {
            soundEffect[0].pitch = 0.4f;
            soundEffect[0].Play();
            isSoundPlaying = true;
        }
        else if (playerRb.velocity.magnitude > 2 && isSoundPlaying)
        {
            soundEffect[0].pitch = playerRb.velocity.magnitude / 5;
        }
        if ((playerRb.velocity.magnitude < 2 && isSoundPlaying) || smashing)
        {
            soundEffect[0].Stop();
            isSoundPlaying = false;
        } */
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            // if (powerupCountdown != null)
            // {
            //     StopCoroutine(powerupCountdown);
            // }
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            if (currentPowerUp == PowerUpType.Rockets)
            {
                powerupIndicator.GetComponent<MeshRenderer>().material = powerupMaterials[1];
            }
            else if (currentPowerUp == PowerUpType.Smash)
            {
                powerupIndicator.GetComponent<MeshRenderer>().material = powerupMaterials[0];
            }
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            // powerupCountdown = StartCoroutine(PowerupCountdownRoutine());
        }

    }
    // IEnumerator PowerupCountdownRoutine()
    // {
    //     yield return new WaitForSeconds(7);
    //     currentPowerUp = PowerUpType.None;
    //     powerupIndicator.SetActive(false);
    //     yield return new WaitForSecondsRealtime(5);
    //     GameManager.instance.powerupAvailable = false;

    // }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (currentPowerUp == PowerUpType.Pushback)
            {
                Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
                enemyRigidbody.AddForce(awayFromPlayer * 20, ForceMode.Impulse);
            }
        }
    }
    void LaunchRockets()
    {
        foreach (var dummy in FindObjectsOfType<Dummy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(dummy.transform);
        }
    }
    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Dummy>();
        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
        smashing = false;
        nextSmashTime = Time.time + 0.5f;
    }
}
