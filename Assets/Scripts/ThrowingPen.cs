using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingPen: MonoBehaviour
{
    Vector3 direction;
    [SerializeField] int key = 4;
    [SerializeField] float speed;
    [SerializeField] float damge;
    public int sign = 1;
    private void OnEnable()
    {
        StopCoroutine(destroy());
        StartCoroutine(destroy());

    }

    public void SetDirection(float dir_x, float dir_y)
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        direction = new Vector3(dir_x, dir_y, 0);

        if (dir_x < 0)
        {
            render.flipX = false;
        }
        else
        {
            render.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().Damaged();
        }

    }
    private void PushPool()
    {
        Stack<GameObject> targetPool;
        ObjecstPool.instance.WeaponPoolDic.TryGetValue(key, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(3.0f);
        this.PushPool();
    }
}
