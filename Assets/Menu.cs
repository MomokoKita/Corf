using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Player m_player;

    [SerializeField]
    GameObject itemText;
    [SerializeField]
    GameObject healBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddItem(GameObject item)
    {
        GameObject obj = (GameObject)Instantiate(itemText, transform.position, Quaternion.identity);
        obj.GetComponent<Text>().text = item.name;
        Debug.Log(item.name);
        obj.transform.parent = healBox.transform;
    }
}
