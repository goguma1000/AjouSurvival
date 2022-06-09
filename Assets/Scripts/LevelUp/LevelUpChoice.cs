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
    //enable >> Ÿ�ӽ����� = 0 >> ���� �� �̹��� �� ������ �ο� 
    private void OnEnable()
    {
        SetSlotInfo();
        Time.timeScale = 0;
        player = GameObject.Find("player");
        character = player.GetComponent<Character>();
    }

    private void Awake()
    {
        itemText.Add(0, "�߻�ü ���� 2�� ����");
        itemText.Add(1, "���� ���� 1�� ����");
        itemText.Add(2, "å ���� 1�� ����");
        itemText.Add(3, "���� ���� 1�� ����");
        itemText.Add(4, "��Ÿ�� 15%����");
        itemText.Add(5, "�ߵ�Ȯ�� 10%����");
        itemText.Add(6, "ü�� 20% ȸ��");

        itemInfo.Add(0, "���� ���� ����\n������ �� 2����\n�߻�ü �߻�");
        itemInfo.Add(1, "������ ������ ���� �߻�");
        itemInfo.Add(2, "�÷��̾� �ֺ���\nȸ���ϴ� å 1�� ��ȯ");
        itemInfo.Add(3, "�÷��̾� ������\n���� 1�� �Ѹ�");
        itemInfo.Add(4, "�������� �߻�ü 1�� �߻�");
        itemInfo.Add(5, "10�ʿ� �ѹ���\n3%Ȯ���� ��� �� ����");

        itemName.Add(0, "���۸�");
        itemName.Add(1, "����ġ��");
        itemName.Add(2, "������");
        itemName.Add(3, "���");
        itemName.Add(4, "��");
        itemName.Add(5, "���찳");
        itemName.Add(6, "ü�� ȸ��");
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
    
    //�̹��� Ǯ���� �����ؼ� 3�� �̱� >> ������ ����üũ
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
