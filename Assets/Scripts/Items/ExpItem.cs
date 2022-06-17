using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public bool goToPlayer = false;
    public AudioSource audio;
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
            audio.Play();
            GameManager.instance.updateEXP(exp);
            StartCoroutine(PushoPool());
        }
    }

   

    IEnumerator PushoPool()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(audio.clip.length);
        GetComponent<Collider2D>().enabled = true;
        Stack<GameObject> targetPool;
        ObjecstPool.instance.DropItemPoolDic.TryGetValue(0, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
}
