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
    public void LeadNPC(string npcText)
    {
        m_player.PlayerStateManger("log");
        mainPanel.SetActive(true);
        mainText.text = npcText;
        getLog = true;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        m_player.PlayerStateManger("default");
    }
}
