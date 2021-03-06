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

    //プレイヤーのスクリプト
    [SerializeField]
    SpriteRenderer playerSprite;
    [SerializeField]
    Sprite[] m_anima;

    //プレイヤーの歩行アニメーション
    private Animator m_anim;  

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
    [SerializeField]
    PlayerState playerState = PlayerState.Default;

    [SerializeField]
    List<GameObject> m_itemList = new List<GameObject>(); //プレイヤーの持つアイテム
    public List<GameObject> itemList => m_itemList;
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private Charactor m_charactor;
    public Charactor charactor => m_charactor;

    void Start()
    {
        //オブジェクトの現在の座標を入手
        m_pos = transform.position;
        m_rigidBody = GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();

        //アニメーター
        m_anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Default)
        {
            PlayerMove();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (playerState == PlayerState.Default)
            {
                StartCoroutine(PlayerCauth());                
            }
        }

    }

    /// <summary>
    /// プレイヤーの移動管理
    /// </summary>
    public void PlayerMove()
    {
        if (playerState != PlayerState.Default)
        {
            return;
        }
        // 矢印キーの入力情報を取得
        var h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            
            playerSprite.sprite = m_anima[1];
            viewDirection = ViewDirection.LookRight;
        }            //右の判定
        else if (h < 0) 
        {
            playerSprite.sprite = m_anima[3];
            viewDirection = ViewDirection.LookLeft; 
        }           //左の判定
        var v = Input.GetAxis("Vertical");
        if (v > 0)
        {
            playerSprite.sprite = m_anima[0];
            viewDirection = ViewDirection.LookUp;
        }           //上の判定
        else if (v < 0) 
        {
            m_anim.SetInteger("walkInt", 1);
            playerSprite.sprite = m_anima[2];
            viewDirection = ViewDirection.LookDown; 
        }           //下の判定
        else
        {
            m_anim.SetInteger("walkInt", 0);
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
        if(playerState == PlayerState.Log)
        {
            yield return new WaitForSeconds(0.1f);
        }
        else if (playerState == PlayerState.Default)
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
        }
        yield return null;
        
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

    public void AddItemList(GameObject item)
    {      
        m_itemList.Add(item);
        m_textManager.GetItemLog(item);
        m_menu.AddItem(item);
    }

    public List<GameObject> GetItem()
    {
        return m_itemList;
    }
    /// <summary>
    /// NPCが提示して欲しいアイテムがあるか調べる関数
    /// </summary>
    /// <param name="itemName">相手の欲しいアイテム</param>
    /// <param name="lost">trueだった場合相手に渡す</param>
    /// <returns>trueだった場合持ってる</returns>
    public bool Flag(string itemName,bool lost)
    {
        foreach (var item in m_itemList)
        {
            if (item.name == itemName)
            {
                if (lost)
                {
                    m_itemList.Remove(item);
                    m_menu.RemoveItem(item);
                }
                
                return true;
            }
        }
        return false;
    }

}
