using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Player m_player;

    [SerializeField]
    GameObject itemText;
    [SerializeField]
    GameObject healBox;

    public enum SubMenuState  //サブメニュー
    {
        Main,
        Status,
        Item,
        Fitted,
        Character,
    }
    [SerializeField]
    GameObject mainPanel;
    [SerializeField]
    GameObject itemPanel;

    SubMenuState subMenuState = SubMenuState.Main;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    /// <summary>m
    /// メニューのステート管理
    /// </summary>
    /// <param name="state"></param>
    public void MenuStateManger(string menu)
    {
        if (menu == "main")
        {
            mainPanel.SetActive(true);
            itemPanel.SetActive(false);
        }
        else if (menu == "item")
        {
            mainPanel.SetActive(false);
            itemPanel.SetActive(true);
        }
    }

    public void AddItem(GameObject item)
    {
        GameObject obj = (GameObject)Instantiate(itemText, transform.position, Quaternion.identity);
        obj.GetComponent<Text>().text = item.GetComponent<ItemBase>().itemName;
        Debug.Log(item.name);
        obj.transform.parent = healBox.transform;
    }

}
