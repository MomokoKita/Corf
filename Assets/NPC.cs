using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] TextManager textManager;

    [SerializeField]
    private string speekText;
    [SerializeField]
    private bool need = false;
    [SerializeField]
    private string needItem = "";
    [SerializeField]
    private string passedItem = "";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Ç†ÇΩÇ¡ÇƒÇÈÇÊ");
        if (col.tag == "Player")
        {
            Player p = col.transform.parent.GetComponent<Player>();
            if (need)
            {
                NeedItem(p);

            }
            else
            {
                Debug.Log("í èÌ");
                textManager.LeadNPC(speekText);
            }


        }
    }

    public void NeedItem(Player p)
    {
        if (p.Flag(needItem, true))
        {
            textManager.LeadNPC(passedItem);
            need = false;
        }
        else
        {
            textManager.LeadNPC(speekText);
        }
    }
}
