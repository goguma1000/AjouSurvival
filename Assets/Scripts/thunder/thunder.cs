using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder : MonoBehaviour
{
    GameObject enemy;
    bool isAttack = false;
    private int key = 1;
    private void OnEnable()
    {
        isAttack = false;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAttack && enemy != null)
        {
            isAttack = true;
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(attack());
        }
        
        if(enemy == null)
        {
            PushPool();
        }
    }

    IEnumerator attack()
    {
        transform.position = enemy.transform.position;
        /*yield return new WaitForSeconds(0.3f);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;*/
        yield return new WaitForSeconds(1f);
        PushPool();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().Damaged();
        }
    }

    public void PushPool()
    {
        Stack<GameObject> targetPool;
        ObjecstPool.instance.WeaponPoolDic.TryGetValue(key, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
}
