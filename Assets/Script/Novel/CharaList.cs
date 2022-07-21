using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaList : MonoBehaviour
{
   
    [SerializeField]
    private List<GameObjectList> charaObject;

    [SerializeField]
    private GameObject player;


    public void FaceChange(int charaNum,int faceNum)
    {
        Debug.Log(charaNum+"aaa"+faceNum);
       player.GetComponent<SpriteRenderer>().sprite = charaObject[charaNum].List[faceNum];
    }
}

[System.Serializable]
public class GameObjectList : ListWrapper<Sprite>
{
}

[System.Serializable]
public class ListWrapper<T>
{
    public List<T> List;
}

