using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecstPool : MonoBehaviour
{
    public static ObjecstPool instance;
    public GameObject[] EnemyPrefabs;
    public GameObject[] WeaponPrefabs;
    public GameObject[] ItemPrefabs;
    public Dictionary<int, Stack<GameObject>> enemyPoolDic = new Dictionary<int, Stack<GameObject>>();
    public Dictionary<int, Stack<GameObject>> WeaponPoolDic = new Dictionary<int, Stack<GameObject>>();
    public Dictionary<int, Stack<GameObject>> DropItemPoolDic = new Dictionary<int, Stack<GameObject>>();
    private Stack<GameObject> EnemyC = new Stack<GameObject>();
    private Stack<GameObject> EnemyCp = new Stack<GameObject>();
    private Stack<GameObject> EnemyB = new Stack<GameObject>();
    private Stack<GameObject> EnemyBp = new Stack<GameObject>();
    private Stack<GameObject> EnemyA = new Stack<GameObject>();
    private Stack<GameObject> EnemyAp = new Stack<GameObject>();
    private Stack<GameObject> google = new Stack<GameObject>();
    private Stack<GameObject> thunder = new Stack<GameObject>();
    private Stack<GameObject> axe = new Stack<GameObject>();
    private Stack<GameObject> book = new Stack<GameObject>();
    private Stack<GameObject> pen = new Stack<GameObject>();
    private Stack<GameObject> exp = new Stack<GameObject>();
    private Stack<GameObject> magnet = new Stack<GameObject>();
    private Stack<GameObject> potion = new Stack<GameObject>();
    // Start is called before the first frame update
    private GameObject CreateObject(GameObject prefab)
    {
        GameObject temp = Instantiate(prefab, transform);
        temp.gameObject.SetActive(false);
        temp.transform.SetParent(this.transform);
        return temp;
    }
    private void MakeEnemyPool(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            EnemyC.Push(CreateObject(EnemyPrefabs[0]));
            EnemyCp.Push(CreateObject(EnemyPrefabs[1]));
            EnemyB.Push(CreateObject(EnemyPrefabs[2]));
            EnemyBp.Push(CreateObject(EnemyPrefabs[3]));
            EnemyA.Push(CreateObject(EnemyPrefabs[4]));
            EnemyAp.Push(CreateObject(EnemyPrefabs[5]));
        }
    }
    private void MakeWeaponPool(int key,int initCount)
    {
        Stack<GameObject> target;
        WeaponPoolDic.TryGetValue(key, out target);
        for (int i = 0; i < initCount; i++)
        {
            target.Push(CreateObject(WeaponPrefabs[key]));
        }
    }

    private void MakeItemPool(int key, int initCount)
    {
        Stack<GameObject> target;
        DropItemPoolDic.TryGetValue(key, out target);
        for (int i = 0; i < initCount; i++)
        {
            target.Push(CreateObject(ItemPrefabs[key]));
        }
    }

    private void Awake()
    {   
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);

        enemyPoolDic.Add(0, EnemyC);
        enemyPoolDic.Add(1, EnemyCp);
        enemyPoolDic.Add(2, EnemyB);
        enemyPoolDic.Add(3, EnemyBp);
        enemyPoolDic.Add(4, EnemyA);
        enemyPoolDic.Add(5, EnemyAp);

        WeaponPoolDic.Add(0, google);
        WeaponPoolDic.Add(1, thunder);
        WeaponPoolDic.Add(2, book);
        WeaponPoolDic.Add(3, axe);
        WeaponPoolDic.Add(4, pen);

        DropItemPoolDic.Add(0, exp);
        DropItemPoolDic.Add(1, magnet);
        DropItemPoolDic.Add(2, potion);

        MakeEnemyPool(500);

        MakeWeaponPool(0,100);
        MakeWeaponPool(1, 30);
        MakeWeaponPool(2, 6);
        MakeWeaponPool(3, 13);
        MakeWeaponPool(4, 100);

        MakeItemPool(0, 5000);
        MakeItemPool(1, 1000);
        MakeItemPool(2, 1000);
        

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
