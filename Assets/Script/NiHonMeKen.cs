using UnityEngine;
using System.Collections;

public class NiHonMeKen : MonoBehaviour {
    private SpriteRenderer spriterenderer;
    public Sprite[] ImgKen;
    public float startTime;
    public int framesPerSecond;
    public NoArguDelegate killEnemyDele;
	// Use this for initialization
    void Awake()
    {
        spriterenderer = renderer as SpriteRenderer;
    }
    void Start()
    {
        startTime = Time.time;
	}
    void AttackAnimation()
    {
        int index = (int)((Time.time - startTime) * framesPerSecond);
        index = index % ImgKen.Length;
        spriterenderer.sprite = ImgKen[index];
        if (Time.time - startTime > 1.0f*ImgKen.Length / framesPerSecond)
        {
            gameObject.SetActive(false);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)//不加RigidBody2D会在一次操作内出发多次
    {
        Debug.Log(other.name);
        Destroy(other.gameObject);
        killEnemyDele();
    }
	// Update is called once per frame
	void Update () 
    {
        AttackAnimation();
	}
}
