using UnityEngine;
using System.Collections;

public delegate void PopDelegate(int index);
class RepopulationCtrl
{
    public static float BaseRate = 10.0f;   // 以该数为基地随机，0~2*BaseRate * deltaTime * PopRate，加满一个BaseRate就调用召唤事件
    private bool isStartRepopulation = false;
    private float LastTime;
    private float PopRate;  // 刷新率，一秒刷几只
    private float PopCount = 0; // 刷新计数，满1就刷一只
    private int enemyArraySize;
    private PopDelegate mPopFunction;

    public void Init(float popRate,int enemayArraySize,PopDelegate _popfunction)
    {
        isStartRepopulation = true;
        PopRate = popRate;
        enemyArraySize = enemayArraySize;
        mPopFunction = _popfunction;
    }
    // 刷新敌机
    public void Repopulation()
    {
        if (!isStartRepopulation)
            return;
        PopCount += Random.Range(0.0f, BaseRate*2) * Time.deltaTime * PopRate;
        if (PopCount>BaseRate)//当随机数累积到1，就刷新一只怪物
        {
            PopCount -= 10.0f;
            int ranIndex = Random.Range(0, enemyArraySize);
            mPopFunction(ranIndex);
        }
    }
}
