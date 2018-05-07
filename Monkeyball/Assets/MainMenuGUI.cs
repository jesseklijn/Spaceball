using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class MainMenuGUI : MonoBehaviour
{
    //public Dictionary<int, Texture2D> Score = new Dictionary<int, Texture2D>();
    public float hSliderValue = 0.0F;
    int total;
    float time = 3;
    string error;
    List<int> offsetx = new List<int>();
    List<int> offsety = new List<int>();
    string[] alllevels;
    float button_width = 200;
    float button_height = 80;
    float select_width = 90;
    float select_height = 40;
    int select_offsetx = 0;
    int select_offsety = -60;
    int box_height = 65;
    int box_width = 120;
    bool options_menu = false;
    bool newprofile_menu = false;
    bool loadprofile_menu = false;
    bool profileinfo_menu = true;
    bool levelselector_menu = false;
    string loadprofile = "";
    string newprofile = "";

    int p;
    int allleveli;
    Gamestats gamestats;
    SaveLoad saveload;
    Sceneselector selector;
    public Texture2D YellowStar;
    public Texture2D GreyStar;



    int f;
    // Use this for initialization
    void Start()
    {
        #region
        selector = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Sceneselector>();
        saveload = GameObject.FindGameObjectWithTag("gamestats").GetComponent<SaveLoad>();
        gamestats = GameObject.FindGameObjectWithTag("gamestats").GetComponent<Gamestats>();
        saveload.Loadprofile();
        saveload.Load(gamestats.profile_name);
        alllevels = selector.scenes;
        #endregion
        //for (int i = 0; i < gamestats.LevelScore.Count; i++)
        //{
        //  Debug.Log(gamestats.LevelScore[i] );
        //  Debug.Log("HOla");
        //}
        for (int i = 0; i < (alllevels.Length); i++)
        {
            f++;

            if (f != 1 && ((f - 1) % 4 == 0))
            {

                total = f;
                select_offsetx += 125;
                select_offsety -= 85 * 4;
            }
            if (i != alllevels.Length)
            {
                //select_offsetx = 230;
                select_offsety += 85;


            }
            offsetx.Add(select_offsetx);
            offsety.Add(select_offsety);
        }
        for (int i = 0; i < gamestats.LevelScore.Count; i++)
        {

            if (gamestats.LevelScore.ContainsKey(i) == true)
            {
                int value;
                Debug.Log(gamestats.LevelScore[i]);
                gamestats.LevelScore.TryGetValue(i, out value);
                gamestats.money += value;
            }
            else
                Debug.Log("Not found sir :(");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (error != "")
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 3;
                error = "";
            }

        }
    }
    void OnGUI()
    {




        //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        if (gamestats.profiles.Count != 0)
        {
            GUI.Box(new Rect(10, 10, 210, ((button_height * 4) + (6 * 15))), "");

            if (GUI.Button(new Rect(15, 15, button_width, button_height), "Continue Level")) { } //TODO LOAD LAST PLAYED LEVEL
            if (options_menu == false) { profileinfo_menu = true; } else profileinfo_menu = false;
            if (levelselector_menu == true) { profileinfo_menu = false; options_menu = false; }
            if (profileinfo_menu == true)
            {
                GUI.Box(new Rect(220, 10, 210, ((button_height * 4) + (6 * 15))), "");

                GUI.Label(new Rect(225, 15, 205, 60), "Profile name: " + gamestats.profile_name);//Player
                GUI.Label(new Rect(225, 65, 205, 60), "Stars unlocked: " + gamestats.money);//Player
                GUI.Label(new Rect(225, 85, 205, 60), "Lives Left: " + gamestats.lives);//Player
                GUI.Label(new Rect(225, 105, 205, 60), "Moneypoints: " + Mathf.Round(gamestats.moneypoints));//Player


            }
            if (GUI.Button(new Rect(15, ((button_height * 1) + 15), button_width, button_height), "Options")) { options_menu = !options_menu; levelselector_menu = false; }//TODO CREATE OPTIONS
            if (options_menu == true)
            {
                GUI.Box(new Rect(230, 200, 210, ((button_height * 1) + (6 * 15))), "Load your Profile:");
                loadprofile = GUI.TextField(new Rect(250, 230, 110, 30), loadprofile, 15);
                if (GUI.Button(new Rect(250, 265, 110, 30), "Load profile"))
                {
                    if (loadprofile != "")
                    {
                       
                        if (gamestats.profiles.ContainsValue(loadprofile) == true)
                        {
                            gamestats.profile_name = loadprofile;
                            gamestats.lastplayed = loadprofile;
                            saveload.Saveprofiles();
                            
                            saveload.Load(gamestats.profile_name);
                            Application.LoadLevel(Application.loadedLevel);
                        }
                        else
                        {
                            Debug.Log("Profile doesnt exist");

                        }

                       
                        
                    }
                    else
                    {
                        Debug.Log("No value entered");
                    }

                }

                GUI.Box(new Rect(230, 10, 210, ((button_height * 1) + (6 * 15))), "New Profile:");

                newprofile = GUI.TextArea(new Rect(250, 80, 110, 30), newprofile, 15);
                GUI.Label(new Rect(225, 150, 205, 60), error);//Player
                if (GUI.Button(new Rect(250, 111, 110, 30), "Create profile"))
                {
                    if (newprofile != "")
                    {





                        for (int i = 0; i < 100; i++)
                        {
                            if (gamestats.profiles.ContainsValue(newprofile) == true)
                            {
                                Debug.Log("Deze naam bestaat al");
                                error = "Name already exists";
                                break;
                            }
                            if (gamestats.profiles.ContainsKey(i) == false)
                            {
                                gamestats.profiles.Add(i, newprofile);
                                Debug.Log(i);
                                gamestats.lastplayed = newprofile;
                                gamestats.profile_name = newprofile;
                                saveload.Saveprofiles();
                                Destroy(GameObject.FindGameObjectWithTag("gamestats").gameObject);
                                Application.LoadLevel(Application.loadedLevel);
                                break;

                            }


                        }


                    }
                    else
                    {
                        Debug.Log("No value entered");
                    }

                }

                if (loadprofile_menu == true)
                {


                }

            }

            if (GUI.Button(new Rect(15, ((button_height * 2) + 15), button_width, button_height), "Ball selector")) { Application.LoadLevel(1); } 
          
           
            if (GUI.Button(new Rect(15, ((button_height * 3) + 15), button_width, button_height), "level Selector"))
            {

                levelselector_menu = !levelselector_menu; alllevels = selector.scenes;
                select_offsety = 15;
            }
            if (levelselector_menu == true)
            {

                GUI.Box(new Rect(220, 10, 420, ((button_height * 4) + (6 * 15))), "All Scenes");
                hSliderValue = GUI.HorizontalSlider(new Rect(225, ((button_height * 4) + (5 * 15)), 390, 40), hSliderValue, 0F, (button_width * 4));
                GUILayout.BeginArea(new Rect(220, 10, 420, ((button_height * 8) + (6 * 15))), "");
                GUILayout.BeginArea(new Rect(-hSliderValue, 0, (button_width * 10), (button_height * 4) + (6 * 15)), "");
                for (int i = 2; i < alllevels.Length; i++)
                {



                    GUI.Box(new Rect(offsetx[i - 2], offsety[i - 2], box_width, box_height + 15), "");
                    if (gamestats.LevelScore.ContainsKey(i) == true)
                    {
                        //Debug.Log("Je hebt een key!");
                        if (gamestats.LevelScore[i] == 3)
                        {
                            //Debug.Log("Je hebt 3 sterren!");
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 25, offsety[i - 2] + 40, 25, 23), YellowStar);
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 51, offsety[i - 2] + 40, 25, 23), YellowStar);
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 76, offsety[i - 2] + 40, 25, 23), YellowStar);
                        }
                        else if (gamestats.LevelScore[i] == 2)
                        {
                            //Debug.Log("Je hebt 2 sterren!");
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 25, offsety[i - 2] + 40, 25, 23), YellowStar);
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 51, offsety[i - 2] + 40, 25, 23), YellowStar);
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 76, offsety[i - 2] + 40, 25, 23), GreyStar);
                        }
                        else if (gamestats.LevelScore[i] == 1)
                        {
                            //Debug.Log("Je hebt 1 ster!");
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 25, offsety[i - 2] + 40, 25, 23), YellowStar);
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 51, offsety[i - 2] + 40, 25, 23), GreyStar);
                            GUI.DrawTexture(new Rect(offsetx[i - 2] + 76, offsety[i - 2] + 40, 25, 23), GreyStar);
                        }


                    }
                    else
                    {
                        //Debug.Log("Je hebt 0 sterren!");
                        GUI.DrawTexture(new Rect(offsetx[i - 2] + 25, offsety[i - 2] + 40, 25, 23), GreyStar);
                        GUI.DrawTexture(new Rect(offsetx[i - 2] + 51, offsety[i - 2] + 40, 25, 23), GreyStar);
                        GUI.DrawTexture(new Rect(offsetx[i - 2] + 76, offsety[i - 2] + 40, 25, 23), GreyStar);
                    }
                    if (gamestats.LevelS.ContainsKey(i) == true)
                    {
                        GUI.Label(new Rect(offsetx[i - 2] + 5, offsety[i - 2] + 60, 140, 30), "Highscore:" + Mathf.Round(gamestats.LevelS[i]));
                    }
                    if (GUI.Button(new Rect(offsetx[i - 2] + 15, offsety[i - 2], select_width, select_height), alllevels[i])) { GetComponent<AutoFade>().LoadLevel(i, 0.5F, 0.5F, Color.black); }
                }


                GUILayout.EndArea();
                GUILayout.EndArea();
            }
            if (GUI.Button(new Rect(15, ((button_height * 4) + 15), button_width, button_height), "Exit game")) { Application.LoadLevel(0); }
        }
        else
        {
            GUI.Box(new Rect(430, 10, 210, ((button_height * 1) + (6 * 15))), "New Profile:");

            newprofile = GUI.TextArea(new Rect(435, 45, 110, 30), newprofile, 15);

            GUI.Label(new Rect(425, 220, 225, 60), error);//Player
            if (GUI.Button(new Rect(435, 80, 110, 30), "Create profile"))
            {
                if (newprofile != "")
                {

                    for (int i = 0; i < 100; i++)
                    {
                        if (gamestats.profiles.ContainsKey(i) == false)
                        {
                            gamestats.profiles.Add(i, newprofile);
                            Debug.Log(i);
                            break;

                        }

                    }
                    gamestats.lastplayed = newprofile;
                    gamestats.profile_name = newprofile;
                    saveload.Saveprofiles();
                    saveload.Loadprofile();

                }
                else
                {
                    Debug.Log("No value entered");
                }
            }

        }
    }
}