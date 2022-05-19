using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     Vector3 m_pos;
    [SerializeField] float m_speed = 3.2f;

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
    ViewDirection viewDirection = ViewDirection.LookDown;

    List<GameObject> itemList = new List<GameObject>();

    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトの現在の座標を入手
        m_pos = transform.position;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Catch();
            StartCoroutine(PlayerCauth());
        }
    }

    /// <summary>
    /// プレイヤーの移動管理
    /// </summary>
    public void PlayerMove()
    {
        // 矢印キーの入力情報を取得
        var h = Input.GetAxis("Horizontal");
        if (h > 0)  viewDirection = ViewDirection.LookRight;    //右の判定
        else if (h < 0) viewDirection = ViewDirection.LookLeft; //左の判定

        var v = Input.GetAxis("Vertical");
        if (v > 0)  viewDirection = ViewDirection.LookUp;       //上の判定
        if (v < 0)  viewDirection = ViewDirection.LookDown;     //下の判定

        // 移動する向きを作成する
        Vector2 direction = new Vector2(h, v).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // スピード二倍
            rigidBody.velocity = direction * m_speed * 2;
        }
        else
        {
            // 移動する向きとスピードを代入 
            rigidBody.velocity = direction * m_speed;
        }    
    }

    /// <summary>
    /// アイテム取得やNPCに話しかけるときの当たり判定
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerCauth()
    {
        Debug.Log(viewDirection);
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
        yield return new WaitForSeconds(0.5f);
        foreach (var item in catchColsion)
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
            }
        }
    }

    public void ItemList(GameObject item)
    {
        itemList.Add(item);
        Debug.Log("お酒を手に入れた");
    }
    public bool Flag(string itemName,bool lost)
    {
        foreach (var item in itemList)
        {
            if (item.name == itemName)
            {
                if (lost)
                {
                    itemList.Remove(item);
                }
                
                return true;
            }
        }
        return false;
    }

}
