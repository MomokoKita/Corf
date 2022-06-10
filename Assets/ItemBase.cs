using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    
    [SerializeField] string m_item; //�A�C�e���̖��O
    public string itemName => m_item;

    [SerializeField]
    private int m_itemNumber = -1; //�A�C�e���̏���
    public int itemNumber => m_itemNumber;
    [SerializeField]
    private string m_itemSummary; //�A�C�e���̊T�v
    public string itemSummary => m_itemSummary;


    [SerializeField]
    private int m_receiveNumber = 1;
    public int receiveNumber => m_receiveNumber;
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
            col.transform.parent.GetComponent<Player>().AddItemList(gameObject);
            col.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
