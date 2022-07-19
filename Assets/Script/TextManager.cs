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
        mainText.text = item.GetComponent<ItemBase>().itemName + "����ɓ��ꂽ";
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
        int messageCount = 0; //���ݕ\�����̕�����
        mainText.text = "";
        while (npcText.Length > messageCount)//���������ׂĕ\�����Ă��Ȃ��ꍇ���[�v
        {
            mainText.text += npcText[messageCount];//�ꕶ���ǉ�
            if (npcText[messageCount] == char.Parse("�B"))
            {
                yield return new WaitForSeconds(0.8f);

            }
            messageCount++;//���݂̕�����
            yield return new WaitForSeconds(0.1f);//�C�ӂ̎��ԑ҂�
        }
        yield return null;
    }

    /// <summary>
    /// �A�����ē����蔻������Ȃ��悤�ɂ���
    /// </summary>
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        m_player.PlayerStateManger("default");
    }
}
