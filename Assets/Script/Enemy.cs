using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// Use this for initialization
    private Player player;
    public float SpeedXMin;
    public float SpeedXMax;
    void Awake()
    {
        player = GameObject.Find("avatar").GetComponent<Player>();
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 v1 = transform.position;
        //Vector3 v2 = v1; v2.y = player.transform.position.y;
        //transform.position = Vector3.Lerp(v1, v2,Time.deltaTime*5);
        if (player.transform.position.x-transform.position.x>Main.ScreenWidth)
        {
            Destroy(gameObject);
        }
        
	}
}
