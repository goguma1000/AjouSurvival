using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMove : MonoBehaviour
{
    public int sign = 1;
    public float b;
    private int key = 3;
    private float x, y, preX, preY;
    private float startX;
    private Vector3 pos;
    private void OnEnable()
    {   
        x = 0;
        y = 0;
        preX = 0;
        preY = 0;
        StartCoroutine(PushPool());
    }
    
    // Update is called once per frame
    void Update()
    {
        x += sign*Time.deltaTime*3;
        y = -((x * x) - (((b)) * x));
        pos = new Vector3(x - preX, y - preY,0);
        transform.position += pos;
        preX = x;
        preY = y;
        transform.Rotate(Vector3.forward, 5);
    }

    IEnumerator PushPool()
    {
        yield return new WaitForSeconds(3);
        Stack<GameObject> targetPool;
        ObjecstPool.instance.WeaponPoolDic.TryGetValue(key, out targetPool);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(ObjecstPool.instance.transform);
        targetPool.Push(this.gameObject);
    }
}
