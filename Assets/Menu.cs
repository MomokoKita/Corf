using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Player m_player;
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    GameObject m_menuPanel;

    public enum SubMenuState  //�T�u���j���[
    {
        Main,
        Item,
        Skill,
        Status,       
        Fitted,
    }
    SubMenuState subMenuState = SubMenuState.Main;

    //��ʂ̐���
    [SerializeField]
    private Text mainText;

    //��ʂ̃p�l���z��
    [SerializeField]
    GameObject[] m_panels;

    //���̕\��
    [SerializeField]
    private Text m_moneyText;

    //�X�e�[�^�X���
    [SerializeField]
    Text m_hpText;

    //�A�C�e���̕\��
    [SerializeField]
    GameObject statusPanel;
    [SerializeField]
    GameObject itemPanel;
    [SerializeField]
    GameObject itemText;


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

    public void AddItem(GameObject item)
    {
        GameObject obj = (GameObject)Instantiate(itemText, transform.position, Quaternion.identity);
        obj.name = item.name;
        obj.GetComponent<Text>().text = item.GetComponent<ItemBase>().itemName;
        Debug.Log(item.name);
        obj.transform.parent = itemPanel.transform;
    }
    public void RemoveItem(GameObject item)
    {

    }

}
