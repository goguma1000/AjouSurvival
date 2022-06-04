using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private int key = 0;
    [SerializeField]
    private int radius = 4;
    private bool canShoot = true;
    private int sign = -1;
    float shootDelay = 0.1f;
    private float angle = 0f;
    private float anglestep;
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
        if (key == 0)
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
        }
        else if(key == 1)
        {
            GameObject temp = Spawn();
            temp.SetActive(true);
            yield return new WaitForSeconds(shootDelay);
            
        }
        else if(key == 2)
        {
            angle = 0;
            anglestep = 360 / 6;
            for (int i = 0; i < 6; i++)
            {
                Vector3 spawnPos = new Vector3(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad),transform.position.z);
                GameObject temp = Spawn();
                temp.SetActive(true);
                temp.transform.SetParent(transform);
                temp.transform.localPosition = spawnPos;
                angle += anglestep;
            }
            yield return new WaitForSeconds(3);
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
