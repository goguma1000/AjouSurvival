using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleShoot : MonoBehaviour
{
    [SerializeField]
    private int key = 0;
    public GameObject bullet;
    private bool canShoot = true;
    private int sign = -1;
    float shootDelay = 0.1f;

    // Update is called once per frame
    void Update()
    {

        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject temp = Spawn();
            if (temp != null)
            {
                GoogleBullet com = temp.GetComponent<GoogleBullet>();
                com.sign = sign;
                temp.SetActive(true);
                com.enabled = true;

                yield return new WaitForSeconds(shootDelay);
                sign *= -1;
            }

        }
        yield return new WaitForSeconds(3);
        canShoot = true;
    }

    private GameObject Spawn()
    {
        Stack<GameObject> targetPool;
        ObjecstPool.instance.WeaponPoolDic.TryGetValue(key, out targetPool);
        if (targetPool.Count > 0)
        {
            GameObject temp = targetPool.Pop();
            temp.transform.SetParent(null);
            temp.transform.position = transform.position;
            return temp;
        }
        else return null;
    }
}
