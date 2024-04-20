using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Color lightColor = new Color(1f, 0.5f, 0f); // Orange color
    public float flickerSpeed = 1f;

    private Light myLight;

    private void Start()
    {
        StartCoroutine(FlickerCoroutine());
        myLight = GetComponent<Light>();
        if (myLight == null)
        {
            Debug.LogError("OrangeLightBulb script requires a Light component attached to the GameObject.");
            enabled = true;
            return;
        }
        
        
    }

    private IEnumerator FlickerCoroutine()
    {
        while (true)
        {
            if (myLight != null)  // Null check
            {
                myLight.intensity = 1f;
                myLight.color = lightColor;
                yield return new WaitForSeconds(1 / flickerSpeed);
            }
            else
            {
                Debug.LogWarning("FlickeringLight: Light component is missing!");
                yield return null;
            }
        }
    }


    void Update()
    {
        // Toggle light bulb when Space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myLight.enabled = !myLight.enabled;
        }
    }
}
