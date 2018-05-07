using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoad : MonoBehaviour
{
    Scenemaster scenemaster;
    Gamestats gamestats;
    void Awake()
    {
        gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
        
       
    }
    //it's static so we can call it from anywhere
    public void Save(string profile)
    {
        Debug.Log(Application.persistentDataPath);
        Gamedata gamedata = new Gamedata();
        //SaveLoad.savedGames.Add(Scenemaster.current); //meebzig 
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/"+profile+".gd"); //you can call it anything you want
        //voer hier data in die gesaved moet worden
        gamedata.profile_name = profile;
        gamedata.money = gamestats.money;
        gamedata.moneypoints = gamestats.moneypoints;
        gamedata.lives = gamestats.lives;
        gamedata.chosenball = gamestats.chosenball;
        gamedata.LevelScore = gamestats.LevelScore;
        gamedata.LevelS = gamestats.LevelS;
        bf.Serialize(file, gamedata);
        file.Close();
    }
    public void SaveLevel(string profile,int level,float time)
    {
        Debug.Log(Application.persistentDataPath);
        Gamedata gamedata = new Gamedata();
        //SaveLoad.savedGames.Add(Scenemaster.current); //meebzig 
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/Scene"+ level + profile + ".gd"); //you can call it anything you want
        //voer hier data in die gesaved moet worden
       
        gamedata.level = level;
        gamedata.time = time;
        gamedata.LevelScore = gamestats.LevelScore;
        gamedata.LevelS = gamestats.LevelS;
        bf.Serialize(file, gamedata);
        file.Close();
    }
    public void LoadLevel(string profile,int level)
    {
       
        if (File.Exists(Application.persistentDataPath + "/" + profile + ".gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Scene" + level + profile + ".gd", FileMode.Open); 
            Gamedata data = (Gamedata)bf.Deserialize(file);
            file.Close();


                scenemaster = GameObject.FindGameObjectWithTag("Scenemaster").GetComponent<Scenemaster>();
                scenemaster.record = data.time;
                gamestats.LevelScore = data.LevelScore;
                gamestats.LevelS = data.LevelS;
            
            Debug.Log(data.time);
           
        }
    }
    public void Load(string profile)
    {

        if (File.Exists(Application.persistentDataPath + "/" +profile+".gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/"+profile+".gd", FileMode.Open);
            Gamedata data = (Gamedata)bf.Deserialize(file);
            file.Close();
            gamestats.profile_name = data.profile_name;
            gamestats.lives = data.lives;
            gamestats.moneypoints = data.moneypoints;
            gamestats.chosenball = data.chosenball;
            gamestats.LevelScore = data.LevelScore;
            gamestats.LevelS = data.LevelS;
        }
    }
    public void Saveprofiles()
    {

        Debug.Log(Application.persistentDataPath);
        Gamedata gamedata = new Gamedata();
        //SaveLoad.savedGames.Add(Scenemaster.current); //meebzig 
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(Application.persistentDataPath + "/"+ "profiles.gd"); //you can call it anything you want
        //voer hier data in die gesaved moet worden
        gamedata.lastplayed = gamestats.lastplayed;
        gamedata.profiles = gamestats.profiles;
        bf.Serialize(file, gamedata);
        file.Close();
        Debug.Log("Profiel toegevoegd");
    }
    public void Loadprofile()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/profiles.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/profiles.gd", FileMode.Open);
            Gamedata data = (Gamedata)bf.Deserialize(file);
            file.Close();
            
                gamestats.lastplayed = data.lastplayed;
                gamestats.profile_name = data.lastplayed;
            gamestats.profiles = data.profiles;
        }
        else
        {
            Debug.Log("Geen profiel, profiel bestaat niet");
        }
    }

}
[Serializable]
class Gamedata
{
    public int chosenball;
    public float speed;
    public float moneypoints;
    public float money;
    public int lives;
    public int level;
    public float time;
    public string profile_name;
    public Dictionary<int, int> LevelScore = new Dictionary<int, int>();
    public Dictionary<int, float> LevelS = new Dictionary<int,float>();
    public string lastplayed;
    public Dictionary<int, string> profiles = new Dictionary<int, string>();
}
