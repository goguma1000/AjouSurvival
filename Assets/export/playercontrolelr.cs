using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrolelr : MonoBehaviour
{
    float horizontal, verticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        verticle = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal*Time.deltaTime*5, verticle*Time.deltaTime*5, 0);

    }
}
