using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    //�ړ��Ɋւ��ϐ�
    Vector3 m_pos;�@//�v���C���[�̈ʒu
    [SerializeField] float m_speed = 3.2f; //�v���C���[�̑���

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
    [SerializeField]
    ViewDirection viewDirection = ViewDirection.LookDown;

    [SerializeField]
    TextManager m_textManager; //�e�L�X�g�Ɋւ���ϐ�
    [SerializeField]
    Menu m_menu; //���j���[�Ɋւ��ϐ�

    public enum PlayerState
    {
        Default,    //�}�b�v�ړ��A�A�C�e���̎擾�Ȃǂ̊�{�̏��
        Menu,       //���j���[�̕\����
        Log         //�e�L�X�g�̕\����   
    }
    PlayerState playerState = PlayerState.Default;

    [SerializeField]
    List<GameObject> m_itemList = new List<GameObject>(); //�v���C���[�̎��A�C�e��

    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private Charactor m_charactor;
    public Charactor charactor => m_charactor;

    void Start()
    {
        //�I�u�W�F�N�g�̌��݂̍��W�����
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
    /// �v���C���[�̈ړ��Ǘ�
    /// </summary>
    public void PlayerMove()
    {
        // ���L�[�̓��͏����擾
        var h = Input.GetAxis("Horizontal");
        if (h > 0) viewDirection = ViewDirection.LookRight;    //�E�̔���
        else if (h < 0) viewDirection = ViewDirection.LookLeft; //���̔���
        var v = Input.GetAxis("Vertical");
        if (v > 0) viewDirection = ViewDirection.LookUp;       //��̔���
        else if (v < 0) viewDirection = ViewDirection.LookDown;     //���̔���
        if (playerState != PlayerState.Default)
        {
            return;
        }
        // �ړ�����������쐬����
        Vector2 direction = new Vector2(h, v).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // �X�s�[�h��{
            m_rigidBody.velocity = direction * m_speed * 2;
        }
        else
        {
            // �ړ���������ƃX�s�[�h���� 
            m_rigidBody.velocity = direction * m_speed;
        }    
    }

    /// <summary>
    /// �A�C�e���擾��NPC�ɘb��������Ƃ��̓����蔻��
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
    /// Player�����ǂ̍s�������Ă邩�w�肷��֐�
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
