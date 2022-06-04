using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject book;
    [SerializeField]
    private int booknum = 6;
    [SerializeField]
    private int radius = 6;
    private float angle = 0f;
    private float anglestep;
    // Start is called before the first frame update
    void Start()
    {
        anglestep = 360 / booknum;
        for(int i = 0; i < booknum; i++)
        {
            Vector2 spawnPos = new Vector2(radius*Mathf.Cos(angle*Mathf.Deg2Rad),radius*Mathf.Sin(angle* Mathf.Deg2Rad));
            Instantiate(book, spawnPos, Quaternion.identity).transform.SetParent(transform);
            angle += anglestep;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
