using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class SaveData
{

    public static SaveData Instanc;

    public int numberOfLevel = 0;

    public int tankSpeed;
    public int normalspeed;
    public int maxSpeed;

    public float totalCoins;
    public int earningCost =10;
    public int speedupCost =10;

    public int totalEarnLevel;
    public int totalSpeedLevel;

    public int spawnPos;
    public int numberofTanks = 1;
    public int lastLevelPlayers;

    public string hashOfSaveData;

    public bool  sfx =true;
    public bool vibration = true;
    public bool bgMusic =true;

    public int lvlhandler;

    //Constructor to save actual GameData

    public SaveData() { }

    public SaveData(int levelno, int tankNormalSpeed, int tankspeed, int maxSpeedd , float totCoins , int earnCost,int speedCost , int earnlevel, int speedlevel , int tankNum, int lastLevelPlayer , bool sfxSD , bool  bgMusicSD , bool vibrationBG , int lvlHandler)
    {
//        Debug.Log("save data call");
        numberOfLevel = levelno;
        tankNormalSpeed = normalspeed;
        tankspeed = tankSpeed;
        totalCoins = totCoins;
        earningCost = earnCost;
        speedupCost = speedCost;
        totalEarnLevel = earnlevel;
        totalSpeedLevel = speedlevel;
        numberofTanks = tankNum;
        lastLevelPlayers = lastLevelPlayer;
        sfx = sfxSD;
        vibration = vibrationBG;
        bgMusic = bgMusicSD;
        lvlhandler = lvlHandler;
        
    }

}