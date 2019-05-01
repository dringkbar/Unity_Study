using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public float playerSpeed = 4.0f;
    private float currentSpeed = 0.0f;
    private Vector3 lastMovement = new Vector3();
    public Transform laser;
    public float laserDistance = .2f;
    public float timeBetweenFires = .3f;
    private float timeTilnextFire = 0.0f;
    public List<KeyCode> shootButton;
    public AudioClip shootSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuBehaviour.isPaused)
        {
            Rotation();
            Movement();
            foreach (KeyCode element in shootButton)
            {
                if (Input.GetKey(element) && timeTilnextFire < 0)
                {
                    timeTilnextFire = timeBetweenFires; ;
                    ShootLaser();
                    break;
                }
            }
            timeTilnextFire -= Time.deltaTime;
        }
    }

    void ShootLaser() {
        audioSource.PlayOneShot(shootSound);
        Vector3 laserPos = this.transform.position;
        float rotationAngle = transform.localEulerAngles.z - 90;
        laserPos.x += (Mathf.Cos((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
        laserPos.y += (Mathf.Sin((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
        Instantiate(laser, laserPos, transform.rotation);
    }

    void Rotation()
    {
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToViewportPoint(worldPos);

        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.x - worldPos.y;
        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        this.transform.rotation = rot;
    }

    void Movement()
    {
        Vector3 movement = new Vector3();
        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");
        movement.Normalize();

        if(movement.magnitude > 0)
        {
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;
        }
        else
        {
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
            currentSpeed *= 0.9f;
        }
    }
}
