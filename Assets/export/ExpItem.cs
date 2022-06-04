using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public bool goToPlayer = false;
    private GameObject target;
    private float speed = 10f;
    private void Start()
    {
        target = GameObject.Find("Player");
    }
    void Update()
    {
        if (goToPlayer)
        {
            Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //exp++
        }
    }
}
