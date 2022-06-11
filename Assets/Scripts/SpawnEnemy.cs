using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] 
    private GameObject target;
    [SerializeField] 
    private float radius = 1;
    [SerializeField] 
    private int phase = 1;

    private float spawnDelay = 3f;
   

    void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(PhaseChange());
    }
    
    public int GetPhase()
    {
        return phase;
    }

    IEnumerator Spawn()
    {
        for (int j = 0; j < phase; j++)
        {
            for (int i = 0; i < phase; i++)
            {
                if (i >= ObjecstPool.instance.enemyPoolDic.Count) continue;
                Stack<GameObject> targetPool;
                ObjecstPool.instance.enemyPoolDic.TryGetValue(i, out targetPool);
                if (targetPool.Count > 0)
                {
                    float angle = Random.Range(0, 359);
                    GameObject temp = targetPool.Pop();
                    temp.transform.position = new Vector3(target.transform.position.x + radius * Mathf.Cos(angle), target.transform.position.y + radius * Mathf.Sin(angle), 0);
                    temp.gameObject.SetActive(true);
                    temp.transform.SetParent(null);
                }
            }
        }
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(Spawn());
    }
    IEnumerator PhaseChange()
    {
        yield return new WaitForSeconds(120);
        phase += 1;
        spawnDelay *= 0.9f;
        StartCoroutine(PhaseChange());
    }
}
