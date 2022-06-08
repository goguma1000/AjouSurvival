using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseAll : MonoBehaviour
{
    int rendom;
    [SerializeField] float deltime;
    bool act = false;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        rendom = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < deltime)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        is_act();
        eraseAll();
    }
    
    private void is_act()
    {
        if (rendom < GameManager.instance.erasePercent)
        {
            act = true;
        }
    }

    private void eraseAll()
    {
        if (act == true)
        {
            Debug.Log("active");
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<EnemyController>().PushPool();
            }
            act = false;
        }
        else
        {
            Debug.Log("fail");
        }
    }
}
