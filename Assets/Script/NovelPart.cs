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
    //現在取得中の文字列
    string nowMainText = default;

    //jsonデータのメンバ変数
    string dataPath = "Assets/Script/Novel/First.json";
    string json;
    TextEventClass jsonData = new TextEventClass();

    private bool waitFlag = false; //待機フラグ
    private bool clickFlag = false; //クリックフラグ

    //コメントのコルーチンを止める為の変数
    Coroutine comentCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        textNum = 0;

        //jsonの読み込み
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
            foreach (var ch in npcText) // 文字列の先頭から1文字ずつ処理
            {
                m_mainText.text += ch; // 一文字追加
                                     // これ待っている間、スキップできない
                yield return new WaitForSeconds(0.04F); // 指定秒待つ
            }

            yield return null;
        }
        Debug.Log("終了");
        waitFlag = true;

        yield return null;
    }
}
