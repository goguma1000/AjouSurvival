using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject target;
    [SerializeField]
    private int key;
    [SerializeField] private float speed;
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
        Stack<GameObject> targetPool;
        ObjecstPool.instance.enemyPoolDic.TryGetValue(key, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision with player");
        }
    }
}
