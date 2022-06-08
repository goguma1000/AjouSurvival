using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_up : MonoBehaviour
{
    private int healAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character c = collision.GetComponent<Character>();
        if (c != null)
        {
            healAmount = Mathf.FloorToInt(c.maxHp * 0.2f);
            c.Heal(healAmount);
            PushPool();
        }
    }

    private void PushPool()
    {
        Stack<GameObject> targetPool;
        ObjecstPool.instance.DropItemPoolDic.TryGetValue(2, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
}
