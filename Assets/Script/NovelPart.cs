using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelPart : MonoBehaviour
{
    //�e�L�X�g��\������p�l��
    [SerializeField]
    private GameObject m_Panel;
    //���͂�\������e�L�X�g
    [SerializeField]
    private Text m_Text;

    // Reaources�t�H���_���璼�ڃe�L�X�g��ǂݍ���
    private string loadText;
    // ���s�ŕ������Ĕz��ɓ����
    private string[] splitText;
    // ���ݕ\�����̃e�L�X�g�ԍ�
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
        int messageCount = 0; //���ݕ\�����̕�����
        m_Text.text = "";
        while (npcText.Length > messageCount)//���������ׂĕ\�����Ă��Ȃ��ꍇ���[�v
        {
            m_Text.text += npcText[messageCount];//�ꕶ���ǉ�
            if (npcText[messageCount] == char.Parse("�B"))
            {
                yield return new WaitForSeconds(0.8f);

            }
            messageCount++;//���݂̕�����
            yield return new WaitForSeconds(0.1f);//�C�ӂ̎��ԑ҂�
        }
        yield return null;
    }
}
