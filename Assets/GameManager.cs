using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_menu;
    [SerializeField]
    Player m_player;

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
            if (!m_menu.activeSelf)
            {
                m_menu.SetActive(true);
                m_player.PlayerStateManger("menu");
            }
            else
            {
                m_menu.SetActive(false);
                m_player.PlayerStateManger("default");
            }
            
        }
    }
}
