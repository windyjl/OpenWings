using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WelcomeUI : MonoBehaviour {
    private string test;
    public delegate int GetGamedataDelegate();
    public delegate void UpdateGamedataDelegate();
    Dictionary<string, GetGamedataDelegate> dicLevelValue;
    Dictionary<string, UpdateGamedataDelegate> dicLevelEvent;
	// Use this for initialization
    void Start()
    {
        GameData.Instance.Init();
        dicLevelValue = new Dictionary<string, GetGamedataDelegate>();
        dicLevelValue.Add("速度", GameData.Instance.GetLvSpeed);
        dicLevelValue.Add("速度上限", GameData.Instance.GetLvMaxSpeed);
        dicLevelValue.Add("阻力", GameData.Instance.GetLvResistance);
        dicLevelValue.Add("高度", GameData.Instance.GetLvHeight);
        dicLevelValue.Add("金钱加成", GameData.Instance.GetLvMoney);
        dicLevelValue.Add("飞行装备", GameData.Instance.GetLvFlyingEquip);
        dicLevelValue.Add("加速装备", GameData.Instance.GetLvRocketEquip);
        dicLevelValue.Add("加速燃料", GameData.Instance.GetLvRocketFuel);

        dicLevelEvent = new Dictionary<string, UpdateGamedataDelegate>();
        dicLevelEvent.Add("速度", GameData.Instance.UpdateSpeed);
        dicLevelEvent.Add("速度上限", GameData.Instance.UpdateMaxSpeed);
        dicLevelEvent.Add("阻力", GameData.Instance.UpdateResistance);
        dicLevelEvent.Add("高度", GameData.Instance.UpdateHeight);
        dicLevelEvent.Add("金钱加成", GameData.Instance.UpdateMoney);
        dicLevelEvent.Add("飞行装备", GameData.Instance.UpdateFlyingEquip);
        dicLevelEvent.Add("加速装备", GameData.Instance.UpdateRocketEquip);
        dicLevelEvent.Add("加速燃料", GameData.Instance.UpdateRocketFuel);
        
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnGUI()
    {
        //GUILayout.BeginHorizontal("box");
        //GUILayout.Label("にまび");
        //test = GUILayout.TextArea(test);
        //GUILayout.TextField("终于好用了");
        //if (GUILayout.Button("欧耶"))
        //{
        //    print("you input " + test);
        //}
        //GUILayout.EndHorizontal();


        GUILayout.BeginVertical("box");
        if (GUILayout.Button("景物飞行演示"))
        {
            Application.LoadLevel(1);
            //print("you input " + test);
        }

        foreach (KeyValuePair<string, GetGamedataDelegate> pair in dicLevelValue)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(pair.Key);
            GUILayout.TextArea(pair.Value().ToString());
            if (GUILayout.Button("升级"))
            {
                if (pair.Value() < GameData.LevelLimit)
                {
                    dicLevelEvent[pair.Key]();
                }
                else
                {
                    C_TipWindow.SetTipWindow();
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

    }
}
