using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class NovelPart : MonoBehaviour
{
    //テキストを表示するパネル
    [SerializeField] private GameObject m_Panel;
    //文章を表示するテキスト
    [SerializeField] private Text m_mainText;
    //名前を表示するテキスト
    [SerializeField] private Text m_nameText;
    // 現在表示中のテキスト番号
    private int textNum;

    //jsonデータのメンバ変数
    string dataPath = "Assets/Script/Novel/First.json";
    string json;
    TextEventClass jsonData = new TextEventClass();

    private bool waitFlag = false; //待機フラグ

    //コメントのコルーチンを止める為の変数
    Coroutine comentCoroutine;

    //キャラクター
    [SerializeField ]CharaList charaList;

    //キャラクターの選択肢 
    [SerializeField] GameObject m_select;


    // Start is called before the first frame update
    void Start()
    {
        textNum = 0;

        //jsonの読み込み
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
                Debug.Log("endだよ");
                return;
            }
            Debug.Log("nomalだよ" + textNum);
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
            Debug.Log("selectだよ");
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
            foreach (var ch in npcText) // 文字列の先頭から1文字ずつ処理
            {
                m_mainText.text += ch; // 一文字追加
                                     // これ待っている間、スキップできない
                yield return new WaitForSeconds(0.04F); // 指定秒待つ
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
