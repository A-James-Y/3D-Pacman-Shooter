
using UnityEngine;
using System.Collections;
using Unity.UI;

public class GunShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public float damge = 10f;
    public float range = 100f;
    public float impactForce = 50f;
    public Camera cam;
    public ParticleSystem muzzeleFlash;
    public float shakeDuration = 0.1f;
    public float shakeAmount = 0.1f;
    public GameObject gun;
    public GameObject impactEffect;


    private Vector3 originalCameraPosition;
    private Vector3 originalGunPosition;

    private float shakeTimeRemaining = 0f;


    // Update is called once per frame
    void Start()
    {
        originalCameraPosition = cam.transform.localPosition;
        originalGunPosition = gun.transform.localPosition;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();


        }

        void Shoot()
        {
            muzzeleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name); //for this to work add box collider

                TargetShoot target = hit.transform.GetComponent<TargetShoot>();
                if (target != null)
                {
                    target.TakeDamage(damge);
                }
                
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 2f);



            }
            StartCoroutine(CameraShake());
           



        }
    }

    private IEnumerator CameraShake()
    {
        float shakeTimeElapsed = 0f;

        while (shakeTimeElapsed < shakeDuration)
        {
            float xOffset = Random.Range(-shakeAmount, shakeAmount);
            float yOffset = Random.Range(-shakeAmount, shakeAmount);

            cam.transform.localPosition = originalCameraPosition + new Vector3(xOffset, yOffset, 0);
            gun.transform.localPosition = originalGunPosition + new Vector3(xOffset, yOffset, 0);

            shakeTimeElapsed += Time.deltaTime;
            shakeTimeRemaining = shakeDuration - shakeTimeElapsed; // Update shakeTimeRemaining
            yield return null; // Wait for the next frame update
        }

        // Reset the camera position to the original position
        cam.transform.localPosition = originalCameraPosition;
        gun.transform.localPosition = originalGunPosition;

        shakeTimeRemaining = 0f; // Ensure shakeTimeRemaining is reset at the end
    }

}
//this is 