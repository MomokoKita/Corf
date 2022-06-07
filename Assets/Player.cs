using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    //移動に関わる変数
    Vector3 m_pos;　//プレイヤーの位置
    [SerializeField] float m_speed = 3.2f; //プレイヤーの速さ

    //上下左右の当たり判定
    [SerializeField]
    GameObject[] catchColsion;  //上０右１下２左３
    public enum ViewDirection   //向いてる方向
    {
        LookUp, 
        LookRight, 
        LookDown, 
        LookLeft, 
    }
    [SerializeField]
    ViewDirection viewDirection = ViewDirection.LookDown;

    [SerializeField]
    TextManager m_textManager; //テキストに関する変数
    [SerializeField]
    Menu m_menu; //メニューに関わる変数

    public enum PlayerState
    {
        Default,    //マップ移動、アイテムの取得などの基本の状態
        Menu,       //メニューの表示中
        Log         //テキストの表示中   
    }
    PlayerState playerState = PlayerState.Default;

    [SerializeField]
    List<GameObject> m_itemList = new List<GameObject>(); //プレイヤーの持つアイテム

    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private Charactor m_charactor;
    public Charactor charactor => m_charactor;

    void Start()
    {
        //オブジェクトの現在の座標を入手
        m_pos = transform.position;
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Default)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(PlayerCauth());
            }
            PlayerMove();       
        }
        

    }

    /// <summary>
    /// プレイヤーの移動管理
    /// </summary>
    public void PlayerMove()
    {
        // 矢印キーの入力情報を取得
        var h = Input.GetAxis("Horizontal");
        if (h > 0) viewDirection = ViewDirection.LookRight;    //右の判定
        else if (h < 0) viewDirection = ViewDirection.LookLeft; //左の判定
        var v = Input.GetAxis("Vertical");
        if (v > 0) viewDirection = ViewDirection.LookUp;       //上の判定
        else if (v < 0) viewDirection = ViewDirection.LookDown;     //下の判定
        if (playerState != PlayerState.Default)
        {
            return;
        }
        // 移動する向きを作成する
        Vector2 direction = new Vector2(h, v).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // スピード二倍
            m_rigidBody.velocity = direction * m_speed * 2;
        }
        else
        {
            // 移動する向きとスピードを代入 
            m_rigidBody.velocity = direction * m_speed;
        }    
    }

    /// <summary>
    /// アイテム取得やNPCに話しかけるときの当たり判定
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerCauth()
    {
        if (viewDirection == ViewDirection.LookUp)
        {
            catchColsion[0].SetActive(true);
        }
        else if (viewDirection == ViewDirection.LookRight)
        {
            catchColsion[1].SetActive(true);
        }
        else if (viewDirection == ViewDirection.LookDown)
        {
            catchColsion[2].SetActive(true);
        }
        else if (viewDirection == ViewDirection.LookLeft)
        {
            catchColsion[3].SetActive(true);
        }
        else
        {
            Debug.Log("Error");
        }

        yield return new WaitForSeconds(0.1f);
        foreach (var item in catchColsion)
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Playerが今どの行動をしてるか指定する関数
    /// </summary>
    /// <param name="state"></param>
    public void PlayerStateManger(string state)
    {
        if (state == "default")
        {
            playerState = PlayerState.Default;
        }
        else if(state == "menu")
        {
            playerState = PlayerState.Menu;
        }
        else if (state == "log")
        {
            playerState = PlayerState.Log;
        }
    }

    public void ItemList(GameObject item)
    {      
        m_itemList.Add(item);
        m_menu.AddItem(item);
        m_textManager.GetItemLog(item);
    }

    public List<GameObject> GetItem()
    {
        return m_itemList;
    }
    public bool Flag(string itemName,bool lost)
    {
        foreach (var item in m_itemList)
        {
            if (item.name == itemName)
            {
                if (lost)
                {
                    m_itemList.Remove(item);
                }
                
                return true;
            }
        }
        return false;
    }

}
