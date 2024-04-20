using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;
    public GameObject obj5;
    public GameObject r1;
    public GameObject r2;
    public GameObject r3;
    public GameObject r4;
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 20 * Time.deltaTime);
        obj2.transform.Rotate(0, 0, 20 * Time.deltaTime);
        obj3.transform.Rotate(0, 0, 20 * Time.deltaTime);
        obj4.transform.Rotate(0, 0, 20 * Time.deltaTime);
        obj5.transform.Rotate(0, 0, 20 * Time.deltaTime);

        r1.transform.Rotate(0, 0, 20 * Time.deltaTime);
        r2.transform.Rotate(0, 0, 20 * Time.deltaTime);
        r3.transform.Rotate(0, 0, 20 * Time.deltaTime);
        r4.transform.Rotate(0, 0, 20 * Time.deltaTime);


    }
}
