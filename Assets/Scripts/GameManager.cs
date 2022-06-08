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
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject[] shooters;
    [SerializeField] private Image[] slotImages;
    [SerializeField] private Sprite[] itemImages;
    [SerializeField] private Text[] levelTexts;
    private Dictionary<int, Text> levelDic = new Dictionary<int, Text>();
    private Text temp;
    private float maxEXP = 100;
    private float nowEXP = 0;
    private float timer = 0;
    private int min = 0;
    private int sec = 0;
    private int[] itemLevel;
    public bool gameOver = false;
    public Image expBar;
    public GameObject levelUpCanvas;
    public int googleBulletNum = 2;
    public int thunderNum = 1;
    public int bookNum = 1;
    public int axeNum = 1;
    public float PenCool = 3;
    public float erasePercent = 3;
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
        itemLevel = new int[] { 0, 0, 0, 0, 1, 0 };
        levelDic.Add(4, levelTexts[0]);
    }

    // Update is called once per frame
    void Update()
    {
        ShowTimer();
        if(min >= 15 && !gameOver)
        {
            gameOver = true;
        }

    }
    private void ShowTimer()
    {
        timer += Time.deltaTime;
        min = Mathf.FloorToInt(timer / 60);
        sec = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
    public void ItemLevelUp(int itemNum)
    {
        if (itemLevel[itemNum] == 0)
        {
            shooters[itemNum].SetActive(true);
            for(int i = 0; i < slotImages.Length; i++)
            {
                if(slotImages[i].sprite == null)
                {
                    slotImages[i].sprite = itemImages[itemNum];
                    levelDic.Add(itemNum, levelTexts[i]);
                    break;
                }
            }
        }
        else
        {
            if (itemNum == 0) //google
            {
                googleBulletNum += 2;
            }
            else if (itemNum == 1) //thunder
            {
                thunderNum += 1;
            }
            else if (itemNum == 2) //book
            {
                bookNum += 1;
            }
            else if (itemNum == 3) //axe
            {
                axeNum += 1;
            }
            else if (itemNum == 4) // Pen
            {
                PenCool *= 0.85f;
            }
            else if (itemNum == 5) // Eraser
            {
                erasePercent *= 1.1f; 
            }
        }
        itemLevel[itemNum] += 1;
        levelDic.TryGetValue(itemNum, out temp);
        temp.text = "" + itemLevel[itemNum];
        if(itemLevel[itemNum] == 6)
        {
            maxLevelItem.Add(itemNum);
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
