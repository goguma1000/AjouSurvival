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
            exps = GameObject.FindGameObjectsWithTag("EXP");
            for(int i =0; i < exps.Length; i++)
            {
                Debug.Log("follow");
                exps[i].GetComponent<ExpItem>().goToPlayer = true;
            }
            PushPool();
        }
    }

    private void PushPool()
    {
        Stack<GameObject> targetPool;
        ObjecstPool.instance.DropItemPoolDic.TryGetValue(1, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
}
