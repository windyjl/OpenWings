using UnityEngine;
using System.Collections;

public class GamesceneUI : MonoBehaviour
{
    private Main refMain;               // 主逻辑对象引用
    private float SpeedX = 200;
    private float SpeedY = 2000;
	// Use this for initialization
    void Start()
    {
        GameData.Instance.Init();
        refMain = GameObject.Find("scriptMain").GetComponent<Main>();
        SpeedX = GameData.Instance.GetSpeed();
        SpeedY = GameData.Instance.GetHeight();
	}
	
	// Update is called once per frame
    void Update()
    {
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
        //GUI.Label(new Rect(0,0,160,90),"にまび");
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("回到开始界面"))
        {
            Application.LoadLevel(0);
            //print("you input " + test);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical("box");
        if (GUILayout.Button("重试"))
        {
            //DontDestroyOnLoad(refMain);
            Application.LoadLevel(1);
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("SpeedX");
        SpeedX = int.Parse(GUILayout.TextArea("" + SpeedX));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("SpeedY");
        SpeedY = int.Parse(GUILayout.TextArea("" + SpeedY));
        GUILayout.EndHorizontal();
        if (GUILayout.Button("发射"))
        {
            refMain.Launch(SpeedX, SpeedY);
        }
        if (GUILayout.Button("清除数据"))
        {
            PlayerPrefs.DeleteAll();
        }
        Vector3 pos = refMain.player.transform.position;
        GUILayout.Label("*按空格起飞");
        GUILayout.Label("角色逻辑坐标 (" + pos.x.ToString("f2") + "," + pos.y.ToString("f2") + ")");
        //pos = GameObject.Find("di").transform.position;
        //GUILayout.Label("参照物坐标 (" + pos.x.ToString("f2") + "," + pos.y.ToString("f2") + ")");
        GUILayout.EndVertical();
//         GameObject obj = GameObject.Find("background"); 获取sprite对象长宽，测试代码。测试失败
//         SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
//         if (spr!=null)
//         {
//             print(spr.sprite.rect.width);
//         }
    }
}
