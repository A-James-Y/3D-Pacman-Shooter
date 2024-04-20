using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShoot : MonoBehaviour
{
    public float maxHealth = 50f; // Introduce a maxHealth variable
    public GameObject destroyedVersion;
    public float respawnTime = 5f;

    private bool shouldRespawn = true;
    private float currentHealth; // Track current health
    private GameObject originalObject;
    private Vector3 initialPosition;
    private Quaternion initialRotation;


    void Start()
    {
        if (destroyedVersion == null)
        {
            Debug.LogError("RespawnableObject on " + gameObject.name + ": destroyedVersion is not assigned!");
        }

        originalObject = gameObject; // Store reference to this object
        currentHealth = maxHealth;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (!shouldRespawn) return; // Check if we should respawn at all

        Debug.Log(gameObject.name + " destroyed! Starting respawn process...");

        Instantiate(destroyedVersion, transform.position, transform.rotation);
        gameObject.SetActive(false);
        Invoke("Respawn", 5);

        
    }

    void Respawn()
    {
        
        currentHealth = maxHealth;
        Debug.Log("Respawning " + gameObject.name + "...");
        originalObject.SetActive(true);
        // Reset to initial position and rotation:
        originalObject.transform.position = initialPosition;
        originalObject.transform.rotation = initialRotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Check for collision with Player
        {
            Die();
        }
    }



    //reset health


}
