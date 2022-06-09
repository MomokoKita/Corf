using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] string m_item;

    public string itemName => m_item;
    // Start is called before the first frame update
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
            Debug.Log(gameObject);
            col.transform.parent.GetComponent<Player>().AddItemList(gameObject);
            col.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
