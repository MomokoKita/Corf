using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class NovelTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadJson()
    {
        string dataPath = "Assets/Script/Novel/First.json";
        string json = File.ReadAllText(dataPath);

        Debug.Log(json);

        TextEventClass jsonData = new TextEventClass();
        JsonUtility.FromJsonOverwrite(json, jsonData);

        foreach (var item in jsonData.event_one)
        {
            Debug.Log(item.name);
            Debug.Log(item.main);
        }
        
    }
}
