using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     Vector3 m_pos;
    [SerializeField] float m_speed = 3.2f;

    //�㉺���E�̓����蔻��
    [SerializeField]
    GameObject[] catchColsion;  //��O�E�P���Q���R
    public enum ViewDirection   //�����Ă����
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
        //�I�u�W�F�N�g�̌��݂̍��W�����
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
    /// �v���C���[�̈ړ��Ǘ�
    /// </summary>
    public void PlayerMove()
    {
        // ���L�[�̓��͏����擾
        var h = Input.GetAxis("Horizontal");
        if (h > 0)  viewDirection = ViewDirection.LookRight;    //�E�̔���
        else if (h < 0) viewDirection = ViewDirection.LookLeft; //���̔���

        var v = Input.GetAxis("Vertical");
        if (v > 0)  viewDirection = ViewDirection.LookUp;       //��̔���
        if (v < 0)  viewDirection = ViewDirection.LookDown;     //���̔���

        // �ړ�����������쐬����
        Vector2 direction = new Vector2(h, v).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // �X�s�[�h��{
            rigidBody.velocity = direction * m_speed * 2;
        }
        else
        {
            // �ړ���������ƃX�s�[�h���� 
            rigidBody.velocity = direction * m_speed;
        }    
    }

    /// <summary>
    /// �A�C�e���擾��NPC�ɘb��������Ƃ��̓����蔻��
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
        Debug.Log("��������ɓ��ꂽ");
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
