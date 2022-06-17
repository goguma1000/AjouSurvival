using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_up : MonoBehaviour
{
    private int healAmount;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character c = collision.GetComponent<Character>();
        if (c != null)
        {
            healAmount = Mathf.FloorToInt(c.maxHp * 0.05f);
            c.Heal(healAmount);
            StartCoroutine(PushPool());
        }
    }

    IEnumerator PushPool()
    {
        audio.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(audio.clip.length);
        Stack<GameObject> targetPool;
        ObjecstPool.instance.DropItemPoolDic.TryGetValue(2, out targetPool);
        gameObject.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
}
