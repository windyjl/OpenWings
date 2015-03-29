using UnityEngine;
using System.Collections;
//一切的开始，斯曼，本弱渣还是喜欢Main
public delegate void NoArguDelegate();
public class Main : MonoBehaviour
{
    public static float ScreenHeight = 7.68f;   //  屏幕高度
    public static float ScreenWidth = 13.66f;   //  屏幕宽度
    public static int CloudMaxPerspective = 300;//  云的最大距离
    public static int BuilMaxPerspective = 500; //  房子的最大距离
    public static float tanTheta = 5.12f;       //  假设100为视距，屏幕最右侧到视角中心的正弦值
    public static float GroundHeight = 1.20f;
    public Player player;                  // 主角
    private bool IsLaunched = false;       // 是否出发
    public float PerspectiveDis = 100;     // 观察距离
    ArrayList ReferenceObjsBGImage;   // 景物们 
    public BackgroundImageControl preGround;        // 地面预置
    public BackgroundImageControl[] preBackground;  // 背景预置
    public ReferenceObject[] preBuilding;   // 房屋预置
    public ReferenceObject[] preCloud;      // 云预置
    //独立的刷怪控制
    public Enemy[] arrEnemys;
    private RepopulationCtrl mRepopNemeyCtrl;
    //各种触发
    //public NoArguDelegate KillEnemyDele;
    // Use this for initialization
    void Awake()
    {
        ReferenceObjsBGImage = new ArrayList();
        mRepopNemeyCtrl   = new RepopulationCtrl();
        Screen.SetResolution((int)(ScreenWidth * 100), (int)(ScreenHeight * 100), false);
    }
	void Start () {
        tanTheta = ScreenWidth / 2;
        //初始云
        for (int i = 0; i < 10;++i )
        {
            ReferenceObject refercenobj = (ReferenceObject)Instantiate(preCloud[Random.Range(0,preCloud.Length)]);
            int randomX = Random.Range(1, 100 * 189);
            //print(randomX);
            refercenobj.Init(randomX, Random.Range(100, 4000), Random.Range(100, CloudMaxPerspective));
        }
        // 创建背景图
        for (int i = 0; i < BackgroundImageControl.BGSize ; ++i)
        {
            // 下半段 0.6f是地面图片的高度
            float groundImgHeight = 120.0f / 100;
            BackgroundImageControl refercenobj = (BackgroundImageControl)Instantiate(preBackground[0]);
            refercenobj.transform.position = new Vector3((i - 1) * 1.2f - ScreenWidth/2, groundImgHeight, 100);
            refercenobj.startPos = refercenobj.transform.position;
            refercenobj.Bangou = i;
            ReferenceObjsBGImage.Add(refercenobj);
            // 上半段
            refercenobj = (BackgroundImageControl)Instantiate(preBackground[1]);
            refercenobj.transform.position = new Vector3((i - 1) * 1.2f - ScreenWidth/2, groundImgHeight + 25.0f, 100);
            refercenobj.Bangou = i;
            refercenobj.startPos = refercenobj.transform.position;
            ReferenceObjsBGImage.Add(refercenobj);
        }
        // 创建地面
        for (int i = 0; i < BackgroundImageControl.BGSize ; ++i)
        {
            BackgroundImageControl refercenobj = (BackgroundImageControl)Instantiate(preGround);
            refercenobj.transform.position = new Vector3((i - 1) * 1.89f - ScreenWidth/2, 0, 100);
            refercenobj.Bangou = i;
            refercenobj.startPos = refercenobj.transform.position;
            ReferenceObjsBGImage.Add(refercenobj);
        }
        // 初始化刷怪方法
        mRepopNemeyCtrl.Init(0.5f, arrEnemys.Length, PopEnemy);
	}
	
	// Update is called once per frame
	void Update () {
        // 刷新云
        RefreshCloud();
        // 刷新建筑
        RefreshBuilding();
        // 键盘事件
        MyKeyDown();
        // 刷新敌人
        mRepopNemeyCtrl.Repopulation();
	}
    void MyKeyDown()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Attack();
        }
    }
    void FixedUpdate()
    {
        CheckAvatarPos();
        CheckBackgroundImgagePosition();
    }
    // 玩家检测
    void CheckAvatarPos()
    {
        if (!player)
        {
            return;                
        }
        Vector3 pos = player.transform.position;
        if (pos.y <= GroundHeight)
        {
            pos.y = GroundHeight;
            player.transform.position = pos;
            //if (IsLaunched)
            {
                //Player.velocity = new Vector3(0, 0, 0);//落地取消速度
            }
        }
    }
    public void Launch(float speedX,float speedY)
    {
        player.Launch(new Vector3(speedX, speedY, 0));
        //player.Speed.x = speed;
        //player.Speed.x = speed;
        //player. = rot;//Mathf.PI / 16;
        //if (!IsLaunched)
        //{
        //    //Player.velocity = (new Vector2(Mathf.Cos(TravelSpeedRot) * TravelSpeed, Mathf.Sin(TravelSpeedRot) * TravelSpeed));
        //    player.transform.position += new Vector3(0, GroundHeight, 0);
        //    IsLaunched = true;
        //}
    }
    // 创建建筑
    void SetBuilding(int x, int z)
    {
        ReferenceObject refercenobj = (ReferenceObject)Instantiate(preBuilding[Random.Range(0, preBuilding.Length)]);
        refercenobj.Init(x, 1.2f, z);
    }
    // 刷新建筑
    void RefreshBuilding()
    {
        //if (offX < -Main.ScreenWidth * 100) offX = -Main.ScreenWidth * 100;
        int countCloud = GameObject.FindGameObjectsWithTag("Building").Length;
        if (countCloud < 3)
        {
            //在可视范围外刷新一个建筑。当角色速度灰常快时，刷新很可能超过半个屏幕，所以突然出现也不会突兀
            int z = Random.Range(100, BuilMaxPerspective);
            float offX;
            float VisibleRange = z * tanTheta * 2;
            if (player.SpeedX < VisibleRange)
                offX = -player.SpeedX;
            else
                offX = player.SpeedX - VisibleRange;
            int x = Random.Range(0, (int)(ScreenWidth * 100) * 2) + (int)(100 * Camera.main.transform.position.x + (z * tanTheta));
            SetBuilding(x + (int)offX, z);
        }
    }
    // 创建云
    void SetCloud(int x,int y,int z)
    {
        ReferenceObject refercenobj = (ReferenceObject)Instantiate(preCloud[Random.Range(0, preCloud.Length)]);
        refercenobj.Init(x, y, z);
    }
    // 刷新云
    void RefreshCloud()
    {
        //if (offX < -Main.ScreenWidth * 100) offX = -Main.ScreenWidth * 100;
        int countCloud = GameObject.FindGameObjectsWithTag("Cloud").Length;
        if (countCloud<10)
        {
            //在可视范围外刷新一个云。当角色速度灰常快时，刷新很可能超过半个屏幕，所以突然出现也不会突兀
            int z = Random.Range(100, CloudMaxPerspective);
            float offX;
            float VisibleRange = z * tanTheta * 2;
            if (player.SpeedX < VisibleRange)
                offX = -player.SpeedX;
            else
                offX = player.SpeedX - VisibleRange;
            int x = Random.Range(0, (int)(ScreenWidth * 100)*2) + (int)(100 * Camera.main.transform.position.x + (z * tanTheta));
            SetCloud(x+(int)offX, Random.Range(100, 4000), z);
            
            //Vector3 off = new Vector3(x,0,z) - Camera.main.transform.position*100;
            //float tanThetaX = off.x / z;
            //float tanThetaY = off.y / z;
            //Vector3 viewPos = Camera.main.transform.position + new Vector3(100 * tanThetaX, 100 * tanThetaY, 0)/100;
        }
    }
    // 检查景物位置
    void CheckBackgroundImgagePosition()
    {
        for (int i = 0; i < ReferenceObjsBGImage.Count;++i )
        {
            BackgroundImageControl _bgi = ReferenceObjsBGImage[i] as BackgroundImageControl;
            _bgi.ElderPostionFucntion();
        }
    }
    // 生成障碍物并向玩家飞来
    void PopEnemy(int index)
    {
        if (!player.IsHassha || player.IsOwari)
            return;
        Enemy enemy = (Enemy)Instantiate(arrEnemys[index]);
        enemy.gameObject.transform.parent = Camera.main.transform;  // 飞行障碍相对于镜头移动
        // 在镜头外某一位置生成，超玩家位置飞去
        enemy.transform.localPosition = new Vector3(ScreenWidth, Random.Range(-ScreenHeight / 2, ScreenHeight / 2), 5);
        float speedX = Random.Range(-20.0f,-5.0f);//100.0f;
        float timeReachPlayer = Mathf.Abs(ScreenWidth*1.5f / speedX);
        float playerY = player.transform.position.y - Camera.main.transform.position.y;
        float speedY = (playerY - enemy.transform.localPosition.y) / timeReachPlayer;
        
        enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, speedY);
        enemy.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(180, 1080);
    }
}
