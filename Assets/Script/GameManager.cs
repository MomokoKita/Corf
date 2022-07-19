using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //プレイヤーの管理
    [SerializeField]
    Player m_player;

    //メニューの管理
    [SerializeField]
    Menu m_menu;
    [SerializeField]
    GameObject m_menuPanel;
    

    [SerializeField]
    private int m_money = 0;
    public int money => m_money;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!m_menuPanel.activeSelf)
            {
                m_menu.ActivePanel(0);
                m_menuPanel.SetActive(true);
                m_player.PlayerStateManger("menu");
            }
            else
            {
                m_menuPanel.SetActive(false);
                m_player.PlayerStateManger("default");
            }
            
        }
    }
}
