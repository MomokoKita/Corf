using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Player p = col.transform.parent.GetComponent<Player>();
            if (p.Flag("liquor",true))
            {
                Debug.Log("Ç†Ç¥Ç¡Ç∑");
            }
            else if(p.Flag("potionHP", false))
            {
                Debug.Log("Ç÷Å[Ç‚ÇÈÇ∂Ç·ÇÒ");
            }
        }
    }
}
