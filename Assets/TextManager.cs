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
            m_player.PlayerStateManger("default");
        }
    }

    public void GetItemLog(GameObject item)
    {
        m_player.PlayerStateManger("log");
        mainPanel.SetActive(true);
        mainText.text = item.GetComponent<ItemBase>().itemName + "‚ðŽè‚É“ü‚ê‚½";
        getLog = true;
    }
    public void LeadNPC(string npcText)
    {
        m_player.PlayerStateManger("log");
        mainPanel.SetActive(true);
        mainText.text = npcText;
        getLog = true;
    }
}
