using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpChoice : MonoBehaviour
{
    public Image[] slotImages;
    public Text[] slotTexts;
    public Sprite[] items;
    private List<int> randomNums = new List<int>();
    private Dictionary<int, string> itemText = new Dictionary<int, string>();
    //enable >> 타임스케일 = 0 >> 스롯 별 이미지 및 데이터 부여 
    private void OnEnable()
    {
        SetSlotInfo();
        Time.timeScale = 0;
    }

    private void Awake()
    {
        itemText.Add(0, "item1\nitem1");
        itemText.Add(1, "item2\nitem2");
        itemText.Add(2, "item3\nitem3");
        itemText.Add(3, "item4\nitem4");
        itemText.Add(4, "item5\nitem5");
        itemText.Add(5, "item6\nitem6");
        itemText.Add(6, "item7\nitem7");
    }
    public void ChoiceSlot(int slotnum)
    {
        UpdateItemStat(slotnum);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
 
    private void UpdateItemStat(int slotnum) 
    {
        int targetItem = randomNums[slotnum -1];
        if (targetItem == 6)
        {
            Debug.Log("Health up");
        }
        else GameManager.instance.ItemLevelUp(targetItem);
       
    }
    
    //이미지 풀에서 랜덤해서 3개 뽑기 >> 아이템 만랩체크
    private void SetSlotInfo()
    {
        int temp = 0;
        bool canAdd = true;
        randomNums.Clear();
        while(randomNums.Count < 3)
        {
            canAdd = true;
            temp = Random.Range(0, items.Length);
            if(GameManager.instance.maxLevelItem.Count > 0 && GameManager.instance.maxLevelItem != null)
            {
                bool go = true;
                foreach(int i in GameManager.instance.maxLevelItem)
                {
                    if (i == temp) go = false; 
                }
                if (go == false) continue;
            }

            if (randomNums.Count == 0) randomNums.Add(temp);
            else
            {
                foreach (int i in randomNums)
                {
                    if (i == temp) canAdd = false;
                }
                if (canAdd) randomNums.Add(temp);
            }

            for(int j = 0; j < randomNums.Count; j++)
            {
                slotImages[j].GetComponent<Image>().sprite = items[randomNums[j]];
                string itemtext;
                itemText.TryGetValue(randomNums[j], out itemtext);
                slotTexts[j].GetComponent<Text>().text = itemtext; 
            }
        }
        
    }
}
