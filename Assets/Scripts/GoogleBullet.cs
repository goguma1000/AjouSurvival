using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleBullet : MonoBehaviour
{
    [SerializeField]
    int key;
    GameObject[] target;
    float x = 0, y, a, b;
    float xpos, ypos, angle, preX, preY;
    public int sign = 1;
    Vector2 dir;
    // Start is called before the first frame update
    private void OnEnable()
    {
        GetComponent<TrailRenderer>().Clear();
        StopCoroutine(destroy());
        GetAngle();
        a = (1 / b) * sign;
        StartCoroutine(destroy());
        x = 0;
        y = 0;
        preX = 0;
        preY = 0;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 13;
        x += Time.deltaTime * speed;
        y = a * ((x * x) - (b * x));
        xpos = x * Mathf.Cos(angle) - y * Mathf.Sin(angle);
        ypos = x * Mathf.Sin(angle) + y * Mathf.Cos(angle);
        transform.position += new Vector3(xpos - preX, ypos - preY, 0);
        preX = xpos;
        preY = ypos;
    }

    void GetAngle()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 4f, LayerMask.GetMask("Enemy"));
        if (col.Length > 0)
        {
            GameObject temp = col[Random.Range(0, col.Length)].gameObject;
            dir = temp.transform.position - transform.position;
            b = (dir.magnitude * Vector2.right).x;
            angle = Mathf.Atan2(dir.y, dir.x);
        }
        else
        {
            dir = new Vector3(3, 2);
            b = (dir.magnitude * Vector2.right).x;
            angle = Mathf.Atan2(dir.y, dir.x);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().Damaged();
            this.PushPool();
        }
    }

    private void PushPool()
    {
        Stack<GameObject> targetPool;
        ObjecstPool.instance.WeaponPoolDic.TryGetValue(key, out targetPool);
        gameObject.SetActive(false);
        GetComponent<TrailRenderer>().Clear();
        this.GetComponent<GoogleBullet>().enabled = false;
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.5f);
        this.PushPool();
    }

}
