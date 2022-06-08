using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public bool goToPlayer = false;
    private GameObject target;
    private float speed = 10f;
    public int exp = 10;

    private void OnEnable()
    {
        target = GameObject.Find("Player");
        goToPlayer = false;
    }
    void Update()
    {
        if (goToPlayer)
        {
            transform.position=Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.updateEXP(exp);
            PushPool();
        }
    }

    public void PushPool()
    {
        Stack<GameObject> targetPool;
        ObjecstPool.instance.DropItemPoolDic.TryGetValue(0, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
}
