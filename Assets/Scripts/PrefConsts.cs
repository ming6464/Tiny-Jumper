using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefConsts : Singleton<PrefConsts>
{
    const string BESTSCORE = "BestScore";
    public int BestScore
    {
        get => PlayerPrefs.GetInt(BESTSCORE, 0);
        set
        {
            if(PlayerPrefs.GetInt(BESTSCORE, 0) < value)
            {
                PlayerPrefs.SetInt(BESTSCORE, value);
            }
        }
    }
    public override void Awake()
    {
        MakeSingleton(true);
    }
}
