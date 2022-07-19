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
    //���ݎ擾���̕�����
    string nowMainText = default;

    //json�f�[�^�̃����o�ϐ�
    string dataPath = "Assets/Script/Novel/First.json";
    string json;
    TextEventClass jsonData = new TextEventClass();

    private bool waitFlag = false; //�ҋ@�t���O
    private bool clickFlag = false; //�N���b�N�t���O

    //�R�����g�̃R���[�`�����~�߂�ׂ̕ϐ�
    Coroutine comentCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        textNum = 0;

        //json�̓ǂݍ���
        json = File.ReadAllText(dataPath);
        JsonUtility.FromJsonOverwrite(json, jsonData);

        m_nameText.text = jsonData.event_one[textNum].name;
        comentCoroutine = StartCoroutine(ReadText(jsonData.event_one[textNum].main));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (jsonData.event_one[textNum].name == "end") return;


            if (waitFlag)
            {
                textNum++;
                comentCoroutine = StartCoroutine(ReadText(jsonData.event_one[textNum].main));
            }
            else {
                m_nameText.text = jsonData.event_one[textNum].name;
                StopCoroutine(comentCoroutine);               
                m_mainText.text = jsonData.event_one[textNum].main;
                waitFlag = true;
            }

        }

    }

    IEnumerator ReadText(string npcText)
    {
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
        Debug.Log("�I��");
        waitFlag = true;

        yield return null;
    }
}
