using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //座標の変数定義
    Vector3 m_pos;
    [SerializeField] float m_speed = 3.2f;
    [SerializeField] GameObject up;
    List<GameObject> itemList = new List<GameObject>();

    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトの現在の座標を入手
        m_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            up.SetActive(true);
        }
        // 矢印キーの入力情報を取得
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // 移動する向きを作成する
        Vector2 direction = new Vector2(h, v).normalized;

        // 移動する向きとスピードを代入 
        GetComponent<Rigidbody2D>().velocity = direction * m_speed;
    }

    public void ItemList(GameObject item)
    {
        itemList.Add(item);
        Debug.Log(itemList[0]);
    }
    public bool Flag(string itemName)
    {
        foreach (var item in itemList)
        {
            if (item.name == itemName)
            {
                return true;
            }
        }
        return false;
    }

}
