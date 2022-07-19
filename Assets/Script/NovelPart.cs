using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelPart : MonoBehaviour
{
    //テキストを表示するパネル
    [SerializeField]
    private GameObject m_Panel;
    //文章を表示するテキスト
    [SerializeField]
    private Text m_Text;

    // Reaourcesフォルダから直接テキストを読み込む
    private string loadText;
    // 改行で分割して配列に入れる
    private string[] splitText;
    // 現在表示中のテキスト番号
    private int textNum;


    // Start is called before the first frame update
    void Start()
    {
        loadText = (Resources.Load("First", typeof(TextAsset)) as TextAsset).text;
        splitText = loadText.Split(char.Parse("\n"));
        textNum = 0;
        StartCoroutine(ReadText(splitText[textNum]));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (textNum < splitText.Length -1)
            {                
                textNum++;
                StartCoroutine(ReadText(splitText[textNum]));               
            }
            
        }
    }

    IEnumerator ReadText(string npcText)
    {
        int messageCount = 0; //現在表示中の文字数
        m_Text.text = "";
        while (npcText.Length > messageCount)//文字をすべて表示していない場合ループ
        {
            m_Text.text += npcText[messageCount];//一文字追加
            if (npcText[messageCount] == char.Parse("。"))
            {
                yield return new WaitForSeconds(0.8f);

            }
            messageCount++;//現在の文字数
            yield return new WaitForSeconds(0.1f);//任意の時間待つ
        }
        yield return null;
    }
}
