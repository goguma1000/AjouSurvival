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
    private GameObject player;
    private Character character;
    //enable >> 타임스케일 = 0 >> 스롯 별 이미지 및 데이터 부여 
    private void OnEnable()
    {
        SetSlotInfo();
        Time.timeScale = 0;
        player = GameObject.Find("player");
        character = player.GetComponent<Character>();
    }

    private void Awake()
    {
        itemText.Add(0, "발사체 개수 2개 증가");
        itemText.Add(1, "번개 개수 1개 증가");
        itemText.Add(2, "책 개수 1개 증가");
        itemText.Add(3, "도끼 개수 1개 증가");
        itemText.Add(4, "쿨타임 15%감소");
        itemText.Add(5, "발동확률 10%증가");
        itemText.Add(6, "체력 20% 회복");
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
            if (character != null)
            {
                character.Heal(Mathf.FloorToInt(character.maxHp * 0.2f));
            }
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
            if (GameManager.instance.maxLevelItem.Count == 6)
            {
                randomNums.Add(6);
                randomNums.Add(6);
                randomNums.Add(6);
                for (int j = 0; j < randomNums.Count; j++)
                {
                    slotImages[j].GetComponent<Image>().sprite = items[randomNums[j]];
                    string itemtext;
                    itemText.TryGetValue(randomNums[j], out itemtext);
                    slotTexts[j].GetComponent<Text>().text = itemtext;
                }
                return;

            }
            else if (GameManager.instance.maxLevelItem.Count == 5)
            {
                int num = 15;
                randomNums.Add(6);
                randomNums.Add(6);
                foreach (int i in GameManager.instance.maxLevelItem)
                {
                    num -= i;
                }
                randomNums.Add(num);
                for (int j = 0; j < randomNums.Count; j++)
                {
                    slotImages[j].GetComponent<Image>().sprite = items[randomNums[j]];
                    string itemtext;
                    itemText.TryGetValue(randomNums[j], out itemtext);
                    slotTexts[j].GetComponent<Text>().text = itemtext;
                }
                return;
            }
            else
            {
                if (GameManager.instance.maxLevelItem.Count > 0 && GameManager.instance.maxLevelItem != null)
                {
                    bool go = true;
                    foreach (int i in GameManager.instance.maxLevelItem)
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

                for (int j = 0; j < randomNums.Count; j++)
                {
                    slotImages[j].GetComponent<Image>().sprite = items[randomNums[j]];
                    string itemtext;
                    itemText.TryGetValue(randomNums[j], out itemtext);
                    slotTexts[j].GetComponent<Text>().text = itemtext;
                }
            }
        }
        
    }
}
