using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Main refMain;                   // 主逻辑对象引用
    public const float ZeroPositonY = 60f;  // 起步位置高度
    public float MaxSpeedY = 10.0f;         // Y轴最大变化值
    private Vector3 Position;
    private float SpeedMagnitude;           //全速
    public float SpeedX;                    // 横向移动速度
    //public float SpeedY;                    // 纵向移动速度
    private float TargetHeight;             // 目标高度
    public float TravelDistace = 0;         // 移动距离
    //private bool IsReachHeight;             // 是否达到过目标高度，到达后就开始下落
    //子成员
    public NiHonMeKen DarkRepulser;         // 逐暗者
    //逻辑开关
    public bool IsHassha = false;           // 是否已发射
    public bool IsOwari = false;            // 是否已结束
	// Use this for initialization
    void Awake()
    {
        SpeedMagnitude = 200;
        Position = new Vector3(0, 0, -5);
        DarkRepulser = GameObject.Find("NiHonMe").GetComponent<NiHonMeKen>();
    }
	void Start () {
        refMain = GameObject.Find("scriptMain").GetComponent<Main>();
	}
	public void Launch(Vector3 _speed)
    {
        if (!IsHassha)
        {
            IsHassha = true;
            SpeedX = _speed.x;
            TargetHeight = _speed.y;
        }
    }
	// Update is called once per frame
    void FixedUpdate()
    {
        GameData.QiangDiZhu("角色");
        CalcSpeed();
        CalcPosition();
        ShowPosition();
        if (IsOwari)
        {
            C_TipWindow.SetTipWindow("飞行结束", "提示", true, "重试", CallRestart);
        }
	}
    void CallRestart()
    {
        Application.LoadLevel(1);
        print(GameData.logs);
        GameData.logs = "";
    }
    // 显示位置
    void ShowPosition()
    {
        Vector3 showPos = Position;
        showPos.y += ZeroPositonY;//地面高度偏移
        showPos.x /= 100;
        showPos.y /= 100;
        transform.position = showPos;
    }
    //计算速度，各种因素照成的加速和减速
    void CalcSpeed()
    {
        if (!IsHassha || IsOwari)
            return;
        //X处理
        if (Position.y<=0)
        {
            SpeedX *= 0.9f;
        }
        else if (SpeedX>GameData.Instance.GetMaxSpeed())
        {
            SpeedX -= GameData.Instance.GetResistance() * 2;
        }
//         else
//         {
//             SpeedX -= GameData.Instance.GetResistance();
//         }
        //Y处理
//         float increment = SpeedY;
//         float newSpdY = Mathf.Sqrt(2.0f * Mathf.Pow(increment, 2) * GameData.Instance.simGravity);  // 势能差
//         if (increment < 0)
//             SpeedY = -newSpdY;  // 正负校正




        //速度小于0结束
        if (SpeedX<0.01f)
        {
            SpeedX = 0;
            if (Position.y <= 0)
            {
                IsOwari = true;
            }
        }
    }
    void CalcPosition()
    {
        if (!IsHassha || IsOwari) return;
        //X
        Position.x += SpeedX;

        //Y
        float rateSpeedY = 1.0f;
        float dis = Mathf.Abs(TargetHeight);
        if (dis<50)//接近目标高度时，上升和下落的速度变慢
        {
            rateSpeedY = dis/50.0f;
            if (rateSpeedY<0.1f)
                rateSpeedY = 0.1f;
        }
        if (TargetHeight > 0)         // 升高剩余值
        {
            Position.y += MaxSpeedY * rateSpeedY;
            TargetHeight -= MaxSpeedY * rateSpeedY;
        }
        else
        {
            Position.y -= MaxSpeedY * rateSpeedY;
            TargetHeight -= MaxSpeedY * rateSpeedY;
        }
        // 减小y值时，重力势能转化为动能
//         SpeedMagnitude -= SpeedY;
//         SpeedY -= SpeedY;
    }
    public void Attack()
    {
        DarkRepulser.gameObject.SetActive(true);
        DarkRepulser.startTime = Time.time;
    }
}
