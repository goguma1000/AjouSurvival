using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject temp;
    private GameObject target;
    private GameObject fallowTarget;
    private Animator anim;
    [SerializeField]
    private int key;
    [SerializeField] 
    private float speed;
    [SerializeField]
    private float health;
    private float nowHealth;
    [SerializeField] 
    int damage = 1;
    // Start is called before the first frame update
    private void OnEnable()
    {
        nowHealth = health;
    }
    void Start()
    {
        target = GameObject.Find("player");
        fallowTarget = target.transform.GetChild(0).gameObject;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =Vector3.MoveTowards(transform.position, fallowTarget.transform.position, speed * Time.deltaTime);
    }
    public void Damaged()
    {
        nowHealth -= 1;
        anim.SetTrigger("isHit");
        if (nowHealth == 0)
        {
            PushPool();
        }
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
        int num = Random.Range(0, 100);
        if (num < 90)//9
        {
            ObjecstPool.instance.DropItemPoolDic.TryGetValue(0, out targetPool);
            if (targetPool.Count > 0)
            {
                temp = targetPool.Pop();
                if (key == 0 || key == 1)
                {
                    temp.GetComponent<ExpItem>().exp = 10;
                    temp.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else if (key == 2 || key == 3)
                {
                    temp.GetComponent<ExpItem>().exp = 15;
                    temp.GetComponent<SpriteRenderer>().color = Color.green;
                }
                else if (key == 4 || key == 5)
                {
                    temp.GetComponent<ExpItem>().exp = 20;
                    temp.GetComponent<SpriteRenderer>().color = Color.red;
                }
                temp.SetActive(true);
                temp.transform.SetParent(null);
                temp.transform.position = transform.position;
            }
        }
        else if(89< num && num < 95)
        {
            ObjecstPool.instance.DropItemPoolDic.TryGetValue(1, out targetPool);
            temp = targetPool.Pop();
            if (targetPool.Count > 0)
            {
                GameObject temp = targetPool.Pop();
                temp.SetActive(true);
                temp.transform.SetParent(null);
                temp.transform.position = transform.position;
            }
            
        }
        else if(94 < num && num < 100)
        {
            ObjecstPool.instance.DropItemPoolDic.TryGetValue(2, out targetPool);
            temp = targetPool.Pop();
            if (targetPool.Count > 0)
            {
                GameObject temp = targetPool.Pop();
                temp.SetActive(true);
                temp.transform.SetParent(null);
                temp.transform.position = transform.position;
            }
        }
    }
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == target)
        {
            Attack();
        }
    }
    Character targetCharacter;
    private void Attack()
    {
        if (targetCharacter == null)
        {
            targetCharacter = target.GetComponent<Character>();
        }
        targetCharacter.TakeDamage(damage);
    }
}
