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
                if (p.Flag(needItem, true))
                {
                    textManager.LeadNPC(passedItem);

                }
                else
                {
                    textManager.LeadNPC(speekText);
                }

            }
            else
            {
                Debug.Log("í èÌ");
                textManager.LeadNPC(speekText);
            }


        }
    }
}
