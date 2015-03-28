using UnityEngine;
using System;
using System.Collections;
public delegate void OnTipWindowClick();
public class C_TipWindow
{
    private static bool TipWindowShow = false;                 // 提示窗口
    private static string strWindowName;
    private static string strWindowContent;    // 提示内容
    private static bool isButtonOn = true;
    private static string strButtonContent;
    private static OnTipWindowClick OnWindowClick;
    // 提示框
    public static void SetTipWindow()
    {
        SetTipWindow("\t穆风有一件重要的事要我告诉你", "提示", true, "确定", Nothing);
    }
    public static void SetTipWindow(string content)
    {
        SetTipWindow("\t" + content, "提示", true, "确定", Nothing);
    }
    public static void SetTipWindow(string content,string title,bool _isButtonOn,string button,OnTipWindowClick _delegate)
    {
        strWindowContent = content;
        strButtonContent = title;
        isButtonOn = _isButtonOn;
        strButtonContent = button;
        TipWindowShow = true;
        OnWindowClick = _delegate;
    }
    public static void Gui()
    {
        if (TipWindowShow)
        {
            int windowWidth = 200;
            int windowHeight = 100;
            int left = (int)(Screen.width - windowWidth) / 2;
            int top = (int)(Screen.height - windowHeight) / 2;
            GUI.Window(0, new Rect(left, top, windowWidth, windowHeight), MyWindow, "提示");
        }
    }
    //对话框函数;
    static void MyWindow(int WindowID)
    {
        GUILayout.Label(strWindowContent);
        if (GUILayout.Button(strButtonContent))
        {
            OnWindowClick();
            TipWindowShow = false;
        }
    }
    static void Nothing()
    {

    }
}
public class GameData:MonoBehaviour{
    private static GameData instance;
    public static int LevelLimit = 10;
    public static GameData Instance{
        get { return GameData.instance;}
    }
    private float LastTime;
    private bool IsInit = false;
    public int LvSpeed { get; private set; }        // 速度
    public int LvMaxSpeed { get; private set; }     // 速度上限
    public int LvResistance { get; private set; }   // 阻力
    public int LvHeight { get; private set; }       // 高度
    public int LvMoney { get; private set; }        // 金钱加成
    public int LvFlyingEquip { get; private set; }  // 飞行装备
    public int LvRocketEquip { get; private set; }  // 加速装备
    public int LvRocketFuel { get; private set; }   // 加速燃料
    //其他配置属性
    public float simGravity = 1.0f;  // 模拟重力值-暂时用于兑换势能
    //调试属性
    private static bool isFirstBlood = false;
    public static string logs;
    public static void QiangDiZhu(string str)
    {
        if (!isFirstBlood)
        {
            print(str + "抢地主");
        }
        isFirstBlood = true;
    }
    public GameData()
    {
    }
    ~GameData()
    {
    }
    void OnGUI()
    {
        C_TipWindow.Gui();
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LastTime = Time.time;
    }
    void Update()
    {
        if (Time.time-LastTime>10)
        {
            Save();
            LastTime = Time.time;
        }
    }
    public void Init()
    {
        //如果没有存过就用默认值，否则读表
        bool isInit = PlayerPrefs.HasKey("IsInit");
        if (isInit)
        {
            //print(PlayerPrefs.GetString("IsInit", "初始化于" + System.DateTime.Now.ToString()));
            Load();
        }
        else
        {
            LvSpeed = 1;
            LvMaxSpeed = 1;
            LvResistance = 1;
            LvHeight = 1;
            LvMoney = 1;
            LvFlyingEquip = 1;
            LvRocketEquip = 0;
            LvRocketFuel = 1;
            PlayerPrefs.SetString("IsInit","初始化于"+System.DateTime.Now.ToString());
            Save();
        }
    }
    public void Load()
    {
        LvSpeed = PlayerPrefs.GetInt("LvSpeed");		    // 速度
        LvMaxSpeed = PlayerPrefs.GetInt("LvMaxSpeed");		// 速度上限
        LvResistance = PlayerPrefs.GetInt("LvResistance");	// 阻力
        LvHeight = PlayerPrefs.GetInt("LvHeight");		    // 高度
        LvMoney = PlayerPrefs.GetInt("LvMoney");		    // 金钱加成
        LvFlyingEquip = PlayerPrefs.GetInt("LvFlyingEquip");// 飞行装备
        LvRocketEquip = PlayerPrefs.GetInt("LvRocketEquip");// 加速装备
        LvRocketFuel = PlayerPrefs.GetInt("LvRocketFuel");	// 加速燃料
    }
    public void Save()
    {
        PlayerPrefs.SetInt("LvSpeed", LvSpeed);				// 速度
        PlayerPrefs.SetInt("LvMaxSpeed", LvMaxSpeed);		// 速度上限
        PlayerPrefs.SetInt("LvResistance", LvResistance);	// 阻力
        PlayerPrefs.SetInt("LvHeight", LvHeight);			// 高度
        PlayerPrefs.SetInt("LvMoney", LvMoney);				// 金钱加成
        PlayerPrefs.SetInt("LvFlyingEquip", LvFlyingEquip);	// 飞行装备
        PlayerPrefs.SetInt("LvRocketEquip", LvRocketEquip);	// 加速装备
        PlayerPrefs.SetInt("LvRocketFuel", LvRocketFuel);	// 加速燃料
    }
    public void UpdateSpeed()			// 速度
    {
        ++LvSpeed;
        Save();
    }
    public void UpdateMaxSpeed()		// 速度上限
    {
        ++LvMaxSpeed;
        Save();
    }
    public void UpdateResistance()		// 阻力
    {
        ++LvResistance;
        Save();
    }
    public void UpdateHeight()			// 高度
    {
        ++LvHeight;
        Save();
    }
    public void UpdateMoney()			// 金钱加成
    {
        ++LvMoney;
        Save();
    }
    public void UpdateFlyingEquip()		// 飞行装备
    {
        ++LvFlyingEquip;
        Save();
    }
    public void UpdateRocketEquip()		// 加速装备
    {
        ++LvRocketEquip;
        Save();
    }
    public void UpdateRocketFuel()		// 加速燃料
    {
        ++LvRocketFuel;
        Save();
    }
    public float GetSpeed()			// 速度
    {
        return LvSpeed * 10 + 100;
    }
    public float GetMaxSpeed()		// 速度上限
    {
        return LvMaxSpeed;
    }
    public float GetResistance()	// 阻力
    {
        //return LvResistance;
        //临时假定减速度为200每秒
        return 5 * Time.deltaTime;
    }
    public float GetHeight()		// 高度
    {
        return LvHeight*500;
    }
    public float GetMoney()			// 金钱加成
    {
        return LvMoney;
    }
    public float GetFlyingEquip()	// 飞行装备
    {
        return LvFlyingEquip;
    }
    public float GetRocketEquip()	// 加速装备
    {
        return LvRocketEquip;
    }
    public float GetRocketFuel()	// 加速燃料
    {
        return LvRocketFuel;
    }
    public int GetLvSpeed()
    {
        return LvSpeed;
    }
    public int GetLvMaxSpeed()
    {
        return LvMaxSpeed;
    }
    public int GetLvResistance()
    {
        return LvResistance;
    }
    public int GetLvHeight()
    {
        return LvHeight;
    }
    public int GetLvMoney()
    {
        return LvMoney;
    }
    public int GetLvFlyingEquip()
    {
        return LvFlyingEquip;
    }
    public int GetLvRocketEquip()
    {
        return LvRocketEquip;
    }
    public int GetLvRocketFuel()
    {
        return LvRocketFuel;
    }
}
