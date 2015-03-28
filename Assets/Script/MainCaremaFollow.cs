using UnityEngine;
using System.Collections;
public class MainCaremaFollow : MonoBehaviour {
	public Transform target;
	public GameObject BackgroundImage;
	public float	ImageWidth = 20.48f;
	private Vector3 centerOffset;
	public int LastLocationAreaID = -1;	// 当前所在区域，用于背景图层的更新，当进入对应区域时生成两边的背景
	private Vector3 StartLocation;
	// Use this for initialization
	void Start () {
		centerOffset = Vector3.forward*-5;
        centerOffset.x = Screen.resolutions[0].width/200;//飞行物大约在画面左侧的中心
		StartLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
    void FixedUpdate()
    {
        Vector3 targetCenter = target.position + centerOffset;
        //camera不能低于地面
        if (targetCenter.y-Main.ScreenHeight/2<0)
        {
            targetCenter.y = Main.ScreenHeight / 2;
        }
        //camera不能超出左边界
//         if (targetCenter.x - Main.ScreenWidth / 2 < -2.0f)
//         {
//             targetCenter.x = Main.ScreenWidth / 2 - 2.0f;
//         }
        Vector3 midPos = targetCenter;
        midPos.y = transform.position.y + (targetCenter.y - transform.position.y) * 0.6f;
        transform.position = midPos;
    }
}
