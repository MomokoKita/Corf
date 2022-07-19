using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Player m_player;
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    GameObject m_menuPanel;

    public enum SubMenuState  //サブメニュー
    {
        Main,
        Item,
        Skill,
        Status,       
        Fitted,
    }
    SubMenuState subMenuState = SubMenuState.Main;

    //画面の説明
    [SerializeField]
    private Text mainText;

    //所持金の表示
    [SerializeField]
    private Text m_moneyText;

    //画面のパネル配列
    [SerializeField]
    GameObject[] m_panels;

    //アイテム画面
    [SerializeField]
    private GameObject m_toolPanel; //道具画面
    [SerializeField]
    private Text m_summaryText; //概要説明

    //ステータス画面
    [SerializeField]
    Text[] m_statusText; //hp,mp,攻撃力,防御力,速度

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_menuPanel.activeSelf)
        {
            m_moneyText.text = gameManager.money.ToString() + "G";
        }       
    }

    public void ActivePanel(int num)
    {
        foreach (var item in m_panels)
        {
            item.SetActive(false);
        }
        m_panels[num].SetActive(true);
    }

    /// <summary>
    /// アイテムの説明
    /// </summary>
    public void SummaryItem()
    {
        GameObject button = EventSystem.current.currentSelectedGameObject;
        GameObject itemName = button.transform.GetChild(0).gameObject;
        string name = itemName.GetComponent<Text>().text;

        foreach (var item in m_player.itemList)
        {
            if (item.GetComponent<ItemBase>().itemName == name)
            {
                m_summaryText.text = item.GetComponent<ItemBase>().itemSummary;
            }
        }

    }

    public void AddItem(GameObject item) 
    {
        ItemBase itemBase = item.GetComponent<ItemBase>();
        int number = itemBase.itemNumber;
        if (!(m_toolPanel.transform.GetChild(number).gameObject.activeSelf))
        {
            m_toolPanel.transform.GetChild(number).gameObject.SetActive(true);
            GameObject text = m_toolPanel.transform.GetChild(number).gameObject;
            GameObject nameObject = text.transform.GetChild(0).gameObject;
            nameObject.GetComponent<Text>().text = itemBase.itemName;

            GameObject numberObject = text.transform.GetChild(2).gameObject;
            int qty = int.Parse(numberObject.GetComponent<Text>().text);
            qty += itemBase.receiveNumber;
            numberObject.GetComponent<Text>().text = qty.ToString();
        }
        else if(m_toolPanel.transform.GetChild(number).gameObject.activeSelf)
        {
            GameObject text = m_toolPanel.transform.GetChild(number).gameObject;
            GameObject nameObject = text.transform.GetChild(0).gameObject;
            string name = nameObject.GetComponent<Text>().text;
            if (name != itemBase.itemName)
            {
                SortItem(itemBase,number);
            }

            GameObject numberObject = text.transform.GetChild(2).gameObject;
            int qty = int.Parse(numberObject.GetComponent<Text>().text);
            qty += itemBase.receiveNumber;
            numberObject.GetComponent<Text>().text = qty.ToString();
        }
    }

    public void SortItem(ItemBase itemBase,int number)
    {
        int count = 0;
        for (int i = 0; i < 20; i++)
        {
            if (m_menuPanel.transform.GetChild(i).gameObject.activeSelf)
            {
                count++;
            }            
        }
        for (int i = count; i == number; i--)
        {
            GameObject newText = m_menuPanel.transform.GetChild(count + 1).gameObject;
            GameObject passText = m_menuPanel.transform.GetChild(count).gameObject;
            GameObject newNameObject = newText.transform.GetChild(0).gameObject;
            GameObject passNameObject = passText.transform.GetChild(0).gameObject;
            newNameObject.GetComponent<Text>().text = passNameObject.GetComponent<Text>().text;
        }
    }

    public void RemoveItem(GameObject item)
    {
        ItemBase itemBase = item.GetComponent<ItemBase>();
        int number = itemBase.itemNumber;
        GameObject text = m_toolPanel.transform.GetChild(number).gameObject;
        GameObject numberObject = text.transform.GetChild(2).gameObject;
        int qty = int.Parse(numberObject.GetComponent<Text>().text);
        qty -= itemBase.receiveNumber;
        numberObject.GetComponent<Text>().text = qty.ToString();
    }

    public void StateManager()
    {

    }

}
