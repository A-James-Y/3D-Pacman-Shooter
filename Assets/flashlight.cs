using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    public Light flash;
    void Start()
    {
        flash = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flash.enabled = !flash.enabled;
        }// press f to turn on/off light
    }
}
