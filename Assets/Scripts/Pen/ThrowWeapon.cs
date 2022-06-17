using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack;
    [SerializeField] int key = 4;
    Player playerMove;
    private bool canShoot = true;
    private AudioSource audio;
    private void Awake()
    {
        playerMove = GetComponentInParent<Player>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.02f);
        GameObject temp = Spawn();
        temp.SetActive(true);
        if (audio.isPlaying == false)
        {
            audio.Play();
        }
        yield return new WaitForSeconds(GameManager.instance.PenCool);
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
            temp.GetComponent<ThrowingPen>().SetDirection(playerMove.lastHorizontalVector, 0f);
            return temp;
        }
        else return null;
    }
}