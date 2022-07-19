using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject mainPanel;
    [SerializeField]
    Player m_player;
    [SerializeField]
    Text mainText;

    bool getLog = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && getLog)
        {
            mainPanel.SetActive(false);
            getLog = false;
            StartCoroutine(Wait());
        }
    }

    public void GetItemLog(GameObject item)
    {
        m_player.PlayerStateManger("log");
        mainPanel.SetActive(true);
        mainText.text = item.GetComponent<ItemBase>().itemName + "を手に入れた";
        getLog = true;
    }
    public void ReadNPC(string npcText)
    {
        m_player.PlayerStateManger("log");
        mainPanel.SetActive(true);
        StartCoroutine(ReadText(npcText));
        getLog = true;
    }

    IEnumerator ReadText(string npcText)
    {
        int messageCount = 0; //現在表示中の文字数
        mainText.text = "";
        while (npcText.Length > messageCount)//文字をすべて表示していない場合ループ
        {
            mainText.text += npcText[messageCount];//一文字追加
            if (npcText[messageCount] == char.Parse("。"))
            {
                yield return new WaitForSeconds(0.8f);

            }
            messageCount++;//現在の文字数
            yield return new WaitForSeconds(0.1f);//任意の時間待つ
        }
        yield return null;
    }

    /// <summary>
    /// 連続して当たり判定を取らないようにする
    /// </summary>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        m_player.PlayerStateManger("default");
    }
}
