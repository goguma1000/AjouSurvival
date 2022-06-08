using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookmove : MonoBehaviour
{
    GameObject target;
    [SerializeField]
    private float angleSpeed = 0;
    private int key = 2;
    private void OnEnable()
    {
        target = GameObject.Find("Player");
        StartCoroutine(PushPool());
    }

    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.forward, angleSpeed * Time.deltaTime);
    }

    IEnumerator PushPool()
    {
        yield return new WaitForSeconds(3f);
        Stack<GameObject> targetPool;
        ObjecstPool.instance.WeaponPoolDic.TryGetValue(key, out targetPool);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        gameObject.SetActive(false);
        targetPool.Push(this.gameObject);    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().Damaged();
        }
    }
}
