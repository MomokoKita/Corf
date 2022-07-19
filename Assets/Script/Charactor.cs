using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Charactor : MonoBehaviour
{
    private int level = 1;
    private int exp = 0;
    private int nextExp = 10;
    private int maxExp = 10;
    
    public int Exp
    {
        get { return exp; }
        set
        {
            if (value < 0)
            {
                float nextExp = this.nextExp;
                float exp = this.exp;
                float maxExp = this.maxExp;
                int ng = 0;

                exp += value;
                while(true)
                {
                    if (exp >= nextExp)
                    {
                        level++;
                        this.exp -= this.nextExp;
                        this.nextExp = (int)Mathf.Floor(maxExp * 1.1f);
                        this.maxExp = this.nextExp;
                        exp = this.exp;
                    }
                    else
                    {
                        break;
                    }
                    if (ng > 100)
                    {
                        Debug.LogError("ñ≥å¿ÉãÅ[ÉvÇµÇΩ");
                        break;
                    }
                    ng++;
                }
                
            }
        }
    }
    public int maxHp = 100;
    public int hp = 100;
    public int maxMp = 100;
    public int mp = 100;
    public int attack = 10;
    public int defense = 10;
    public int speed = 10;
}
