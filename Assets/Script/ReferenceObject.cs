using UnityEngine;
using System.Collections;

public class ReferenceObject : MonoBehaviour
{
    public Vector3 Position;
    public SpriteRenderer sprite;
    private Main refMain;               // 主逻辑对象引用
    public bool isInit = false;
    private bool isFirstTime = false;
    // Use this for initialization
	void Start () {
        refMain = GameObject.Find("scriptMain").GetComponent<Main>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
	}
    //参考Y值0是地面，Z值100是1:1透视
    public void Init(float x, float y,float z)
    {
        Position = new Vector3(x, y, z);
        isInit = true;
    }
	// Update is called once per frame
    void Update()
    {
        //如果参照物飞出视野左侧以外就删除
        float LeftBorderOfCamera = Camera.main.transform.position.x - 768 / 100;
        float RefObjWidth = sprite.sprite.rect.width * transform.localScale.x;
        Vector3 viewPos = GetDisplayPosition();
        float abc = RefObjWidth / 100;
        if (viewPos.x+abc<LeftBorderOfCamera)
        {
            Destroy(gameObject);
        }
    }
	void FixedUpdate () 
    {
        if (isInit)
        {
            transform.position = GetDisplayPosition();
        }
    }
    Vector3 GetDisplayPosition()
    {
        Vector3 off = Position - Camera.main.transform.position*100;
        float tanThetaX = off.x / Position.z;
        float tanThetaY = off.y / Position.z;
        Vector3 viewPos = Camera.main.transform.position + new Vector3(refMain.PerspectiveDis * tanThetaX, refMain.PerspectiveDis * tanThetaY, 0)/100;
        //建筑物额外处理
        if (tag=="Building")
        {
            viewPos.y = Position.y;
        }
        viewPos.z = Position.z / 10;
        
//         if (!isFirstTime)
//         {
//             print("MyX-cameraPosX:" + (viewPos.x-Camera.main.transform.position.x));
//         }
//         isFirstTime = true;
        return viewPos;
    }
}
