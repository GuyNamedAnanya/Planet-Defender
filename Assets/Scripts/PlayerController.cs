using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float ySpeed = 10f;
    [SerializeField] float xRange = 7f;
    [SerializeField] float yRange = 7f;
    [SerializeField] float pitchFactorDueToPosition = -2f;
    [SerializeField] float pitchControlSpeed = 10f;
    [SerializeField] float yawFactorDueToPosition = 1f;
    [SerializeField] float rollControlSpeed = 10f;
    [SerializeField] float rotationFactor;
    [SerializeField] GameObject laser;

    float horizontalInput;
    float verticalInput;
    CollisionHandler collisionHandler;


    void Awake()
    {
        collisionHandler = FindObjectOfType<CollisionHandler>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!collisionHandler.isAlive)
        {
            return;
        }
        ProcessMovement();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        float xOffset = horizontalInput * xSpeed * Time.deltaTime;
        float xPos = transform.localPosition.x + xOffset;
        float clampXpos = Mathf.Clamp(xPos, -xRange, xRange);

        float yOffset = verticalInput * ySpeed * Time.deltaTime;
        float yPos = transform.localPosition.y + yOffset;
        float clampYpos = Mathf.Clamp(yPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampXpos, clampYpos, transform.localPosition.z);
    }

    void ProcessRotation()
    {


        float pitch = transform.localPosition.y * pitchFactorDueToPosition + verticalInput * pitchControlSpeed;
        float yaw = transform.localPosition.x * yawFactorDueToPosition;
        float roll = horizontalInput * rollControlSpeed;

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            ActivateLasers(true);
        }
        else
        {
            ActivateLasers(false);
        }
    }

    void ActivateLasers(bool isLaserActive)
    {
        var emisionModule = laser.GetComponent<ParticleSystem>().emission;
        emisionModule.enabled = isLaserActive;
    }
}
