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

    //�v���C���[�̃X�N���v�g
    [SerializeField]
    SpriteRenderer playerSprite;
    [SerializeField]
    Sprite[] m_anima;

    //�v���C���[�̕��s�A�j���[�V����
    private Animator m_anim;  

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
    [SerializeField]
    PlayerState playerState = PlayerState.Default;

    [SerializeField]
    List<GameObject> m_itemList = new List<GameObject>(); //�v���C���[�̎��A�C�e��
    public List<GameObject> itemList => m_itemList;
    private Rigidbody2D m_rigidBody;

    [SerializeField]
    private Charactor m_charactor;
    public Charactor charactor => m_charactor;

    void Start()
    {
        //�I�u�W�F�N�g�̌��݂̍��W�����
        m_pos = transform.position;
        m_rigidBody = GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();

        //�A�j���[�^�[
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
    /// �v���C���[�̈ړ��Ǘ�
    /// </summary>
    public void PlayerMove()
    {
        if (playerState != PlayerState.Default)
        {
            return;
        }
        // ���L�[�̓��͏����擾
        var h = Input.GetAxis("Horizontal");
        if (h > 0)
        {
            
            playerSprite.sprite = m_anima[1];
            viewDirection = ViewDirection.LookRight;
        }            //�E�̔���
        else if (h < 0) 
        {
            playerSprite.sprite = m_anima[3];
            viewDirection = ViewDirection.LookLeft; 
        }           //���̔���
        var v = Input.GetAxis("Vertical");
        if (v > 0)
        {
            playerSprite.sprite = m_anima[0];
            viewDirection = ViewDirection.LookUp;
        }           //��̔���
        else if (v < 0) 
        {
            m_anim.SetInteger("walkInt", 1);
            playerSprite.sprite = m_anima[2];
            viewDirection = ViewDirection.LookDown; 
        }           //���̔���
        else
        {
            m_anim.SetInteger("walkInt", 0);
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
    /// NPC���񎦂��ė~�����A�C�e�������邩���ׂ�֐�
    /// </summary>
    /// <param name="itemName">����̗~�����A�C�e��</param>
    /// <param name="lost">true�������ꍇ����ɓn��</param>
    /// <returns>true�������ꍇ�����Ă�</returns>
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