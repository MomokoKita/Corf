using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //���W�̕ϐ���`
    Vector3 m_pos;
    [SerializeField] float m_speed = 3.2f;
    [SerializeField] GameObject up;
    List<GameObject> itemList = new List<GameObject>();

    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;
    // Start is called before the first frame update
    void Start()
    {
        //�I�u�W�F�N�g�̌��݂̍��W�����
        m_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            up.SetActive(true);
        }
        // ���L�[�̓��͏����擾
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        // �ړ�����������쐬����
        Vector2 direction = new Vector2(h, v).normalized;

        // �ړ���������ƃX�s�[�h���� 
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
