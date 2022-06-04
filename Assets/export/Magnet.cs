using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    GameObject[] exps;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exps = GameObject.FindGameObjectsWithTag("Exp");
            for(int i =0; i < exps.Length; i++)
            {
                exps[i].GetComponent<ExpItem>().goToPlayer = true;
            }
        }
    }
}
