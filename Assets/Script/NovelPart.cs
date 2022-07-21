using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class NovelPart : MonoBehaviour
{
    //�e�L�X�g��\������p�l��
    [SerializeField] private GameObject m_Panel;
    //���͂�\������e�L�X�g
    [SerializeField] private Text m_mainText;
    //���O��\������e�L�X�g
    [SerializeField] private Text m_nameText;
    // ���ݕ\�����̃e�L�X�g�ԍ�
    private int textNum;

    //json�f�[�^�̃����o�ϐ�
    string dataPath = "Assets/Script/Novel/First.json";
    string json;
    TextEventClass jsonData = new TextEventClass();

    private bool waitFlag = false; //�ҋ@�t���O

    //�R�����g�̃R���[�`�����~�߂�ׂ̕ϐ�
    Coroutine comentCoroutine;

    //�L�����N�^�[
    [SerializeField ]CharaList charaList;

    //�L�����N�^�[�̑I���� 
    [SerializeField] GameObject m_select;


    // Start is called before the first frame update
    void Start()
    {
        textNum = 0;

        //json�̓ǂݍ���
        json = File.ReadAllText(dataPath);
        JsonUtility.FromJsonOverwrite(json, jsonData);

        m_nameText.text = jsonData.event_one[textNum].name;
        comentCoroutine = StartCoroutine(ReadText(jsonData.event_one[textNum].main));
        FaceChange();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (jsonData.event_one[textNum].name == "end")
            {
                Debug.Log("end����");
                return;
            }
            Debug.Log("nomal����" + textNum);
            if (waitFlag)
            {
                textNum++;
                comentCoroutine = StartCoroutine(ReadText(jsonData.event_one[textNum].main));
            }
            else
            {
                m_nameText.text = jsonData.event_one[textNum].name;
                StopCoroutine(comentCoroutine);
                m_mainText.text = jsonData.event_one[textNum].main;

                waitFlag = true;
            }

        }

    }

    IEnumerator ReadText(string npcText)
    {
        if (jsonData.event_one[textNum].name == "select")
        {
            Debug.Log("select����");
            m_select.gameObject.SetActive(true);
            yield break;
        }

        FaceChange();
        waitFlag = false;
        m_mainText.text = "";

        if (npcText == null)
        {
            yield return null;
        }
        else
        {
            m_mainText.text = "";
            foreach (var ch in npcText) // ������̐擪����1����������
            {
                m_mainText.text += ch; // �ꕶ���ǉ�
                                     // ����҂��Ă���ԁA�X�L�b�v�ł��Ȃ�
                yield return new WaitForSeconds(0.04F); // �w��b�҂�
            }

            yield return null;
        }
        waitFlag = true;

        yield return null;
    }


    public void SelectButton(string text)
    {
        if (text == "two")
        {
            m_nameText.text = jsonData.event_two[0].name;
            m_mainText.text = jsonData.event_two[0].main;
            
        }
        else if (text == "three")
        {
            m_nameText.text = jsonData.event_three[0].name;
            m_mainText.text = jsonData.event_three[0].main;
        }
        m_select.gameObject.SetActive(false);
        Debug.Log(jsonData.event_one[textNum].main +""+textNum);
    }

    public void FaceChange()
    {
        charaList.FaceChange(jsonData.event_one[textNum].face[0], jsonData.event_one[textNum].face[0]);
    }
}
