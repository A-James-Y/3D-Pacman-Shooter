using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject destroyedVersion;

    public void Shatter()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);

        
        Destroy(gameObject);
    }
}
