using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder : MonoBehaviour
{
    GameObject[] enemies;
    bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        enemies=GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttack)
        {
            isAttack = true;
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        transform.position = enemies[Random.Range(0, enemies.Length)].transform.position;
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        isAttack = false;
    }
}
