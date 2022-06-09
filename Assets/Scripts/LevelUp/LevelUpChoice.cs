using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpChoice : MonoBehaviour
{
    public Image[] slotImages;
    public Text[] slotTexts;
    public Text[] slotName;
    public Sprite[] items;
    private List<int> randomNums = new List<int>();
    private Dictionary<int, string> itemText = new Dictionary<int, string>();
    private Dictionary<int, string> itemInfo = new Dictionary<int, string>();
    private Dictionary<int, string> itemName = new Dictionary<int, string>();
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

        itemInfo.Add(0, "일정 범위 내에\n임의의 적 2명에게\n발사체 발사");
        itemInfo.Add(1, "임의의 적에게 벼락 발사");
        itemInfo.Add(2, "플레이어 주변을\n회전하는 책 1개 소환");
        itemInfo.Add(3, "플레이어 주위에\n도끼 1개 뿌림");
        itemInfo.Add(4, "전방으로 발사체 1개 발사");
        itemInfo.Add(5, "10초에 한번씩\n3%확률로 모든 적 제거");

        itemName.Add(0, "구글링");
        itemName.Add(1, "벼락치기");
        itemName.Add(2, "교과서");
        itemName.Add(3, "찍기");
        itemName.Add(4, "펜");
        itemName.Add(5, "지우개");
        itemName.Add(6, "체력 회복");
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
                    SetItemName(randomNums[j],j);
                    SetItemText(randomNums[j],j);
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
                    SetItemName(randomNums[j],j);
                    SetItemText(randomNums[j],j);
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
                    SetItemName(randomNums[j],j);

                    if(randomNums[j] == 6)
                    {
                        SetItemText(randomNums[j],j);
                    }
                    else if(GameManager.instance.itemLevel[randomNums[j]] != 0)
                    {
                        SetItemText(randomNums[j],j);
                    }
                    else
                    {
                        SetItemInfo(randomNums[j],j);
                    }
                }
            }
        }
        
    }

    private void SetItemText(int itemNum, int slotNum)
    {
        string itemtext;
        itemText.TryGetValue(itemNum, out itemtext);
        slotTexts[slotNum].text = itemtext;
    }

    private void SetItemName(int itemNUm, int slotNum)
    {
        string nametext;
        itemName.TryGetValue(itemNUm, out nametext);
        slotName[slotNum].text = nametext;
    }

    private void SetItemInfo(int itemNum, int slotNum)
    {
        string infotext;
        itemInfo.TryGetValue(itemNum, out infotext);
        slotTexts[slotNum].text = infotext;
    }
}
