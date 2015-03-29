﻿using UnityEngine;
using System.Collections;

public class BackgroundImageControl : MonoBehaviour {
    public static int BGSize = 13;
	// Use this for initialization
    public int Bangou;   // 出席番号
    public Vector3 startPos;
    private Player refPlayer;
	private Camera CameraTarget;
    private SpriteRenderer spriterenderer;
    void Awake()
    {
        refPlayer = GameObject.Find("avatar").GetComponent<Player>();
    }
	void Start () {
		if (Camera.main) {
			CameraTarget = Camera.main;
		}
        spriterenderer = GetComponent<SpriteRenderer>();
	}
	
	// 假如当前这个图的位置超出屏幕左侧，就把自己挪到到下一个点
	void FixedUpdate () 
    {
        GameData.QiangDiZhu("背景");
        //ElderPostionFucntion();
        //FixedPosition();
	}
    // 老式，移动单个图片到下一个位置的算法。无法适用于超高速飞行
    public void ElderPostionFucntion()
    {
        //Vector3 rightPos = transform.position;
        //float width = GetComponent<SpriteRenderer>().sprite.rect.width;
        //rightPos.x += width / 100;
        //Vector3 screenPos = CameraTarget.WorldToScreenPoint(rightPos);
        //if (screenPos.x < 0)
        //{
        //    int multi = (int)(refPlayer.SpeedX / (width * BGSize)) + 1;
        //    transform.position += new Vector3(width * BGSize * multi / 100, 0, 0);
        //}
        /**************************/
        // 新算法，根据player位置计算背景图位置
        // 角色在屏幕左起1/4处
        float width = GetComponent<SpriteRenderer>().sprite.rect.width;
        //float rightpos = transform.position.x + width / 100;
        Vector3 rightPoint3 = transform.position + new Vector3(width/100,0,0);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(rightPoint3);
        float leftBorderX = refPlayer.transform.position.x - Main.ScreenWidth/4;
        if (screenPos.x<0)
        {
            float groupWidth = width * BGSize / 100;
            float dis = leftBorderX - width / 100 - Main.ScreenWidth / 2;
            int multi = (int)(dis / groupWidth) + 1;
            int leftImgNo = (int)(dis / width*100)-13*(multi-1);
            if (Bangou<leftImgNo)
            {
                ++multi;
            }
            if (gameObject.CompareTag("Di"))
                GameData.logs += Bangou + "号 跳转" + multi + "倍,此时最左:" + leftImgNo                + "\r\n";
            transform.position = new Vector3(startPos.x + groupWidth * multi,transform.position.y,transform.position.z);
        }
    }
    //直接固定背景位置
    void FixedPosition()
    {
        float width = GetComponent<SpriteRenderer>().sprite.rect.width-0.1f;
        transform.position = new Vector3(Camera.main.transform.position.x-Main.ScreenWidth/2+width * Bangou / 100, transform.position.y, transform.position.z);
    }
}
