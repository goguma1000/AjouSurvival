using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject temp;
    private GameObject target;
    [SerializeField]
    private int key;
    [SerializeField] 
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void PushPool()
    {
        DropItem();
        Stack<GameObject> targetPool;
        ObjecstPool.instance.enemyPoolDic.TryGetValue(key, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }

    private void DropItem()
    {
        Stack<GameObject> targetPool;
        int num = Random.Range(0, 11);
        if (num < 9)
        {
            ObjecstPool.instance.DropItemPoolDic.TryGetValue(0, out targetPool);
            if (targetPool.Count > 0)
            {
                temp = targetPool.Pop();
            }
            temp.SetActive(true);
            temp.transform.SetParent(null);
            temp.transform.position = transform.position;
        }
        else if(num == 9)
        {
            ObjecstPool.instance.DropItemPoolDic.TryGetValue(1, out targetPool);
            temp = targetPool.Pop();
            if (targetPool.Count > 0)
            {
                GameObject temp = targetPool.Pop();
            }
            temp.SetActive(true);
            temp.transform.SetParent(null);
            temp.transform.position = transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision with player");
        }
    }
}
