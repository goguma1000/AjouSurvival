using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public List<int> maxLevelItem = new List<int>();
    [SerializeField] private int oldlevel = 1;
    [SerializeField] private int nowlevel;
    private float maxEXP = 100;
    private float nowEXP = 0;
    public Image expBar;
    public GameObject levelUpCanvas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        nowlevel = oldlevel;
    }
    // Start is called before the first frame update
    void Start()
    {
  

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemLevelUp(int itemNum)
    {
        if(itemNum == 0) //
        {
            Debug.Log("Level up item 0");
        }
        else if(itemNum == 1) //
        {
            Debug.Log("Level up item 1");
        }
        else if (itemNum == 2) //
        {
            Debug.Log("Level up item 2");
        }
        else if (itemNum == 3) //
        {
            Debug.Log("Level up item 3");
        }
        else if (itemNum == 4) // 
        {
            Debug.Log("Level up item 4");
        }
        else if (itemNum == 5) //
        {
            Debug.Log("Level up item 5");
        }
    }

    public void updateEXP(float exp)
    {
        nowEXP += exp;
        expBar.fillAmount = nowEXP / maxEXP;
        if(Mathf.Floor(nowEXP/maxEXP) == 1)
        {   

            nowlevel += 1;
            nowEXP = nowEXP % maxEXP;
            levelUpCanvas.SetActive(true);
            maxEXP *= 1.3f;
            expBar.fillAmount = nowEXP / maxEXP;
        }
    }
}
