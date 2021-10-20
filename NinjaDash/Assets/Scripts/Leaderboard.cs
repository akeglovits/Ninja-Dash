using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class Leaderboard : MonoBehaviour
{

    private static ILeaderboard leaderboard;
    public GameController gameController;
    private bool loggedIn;
    public bool fade;
    public byte alpha;

    private string boardid;

    // Start is called before the first frame update
    void Start()
    {

        #if UNITY_ANDROID
        PlayGamesPlatform.Activate();
        #endif

        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);

                loggedIn = true;
            }
            else{
                Debug.Log("Authentication failed");

                loggedIn = false;
            }
        });

         leaderboard = Social.CreateLeaderboard();

         #if UNITY_IOS

        if(GameObject.Find("Not-Signed-In")){
            GameObject.Find("Not-Signed-In").GetComponent<Text>().text = "YOU MUST BE SIGNED IN TO GAMECENTER TO ACCESS THE LEADERBOARD";
        }

        leaderboard.id = "distance";
        boardid = "distance";

        #elif UNITY_ANDROID

        if(GameObject.Find("Not-Signed-In")){
            GameObject.Find("Not-Signed-In").GetComponent<Text>().text = "YOU MUST BE SIGNED IN TO GOOGLE PLAY GAMES TO ACCESS THE LEADERBOARD";
        }

        leaderboard.id = "CgkImZ-07OYTEAIQAQ";
        boardid = "CgkImZ-07OYTEAIQAQ";

        #endif

        Debug.Log(boardid);
    }

    void Update(){
        if(fade){
            if(alpha > 0f){
                alpha -= 1;
                if(GameObject.Find("Not-Signed-In")){
                    GameObject.Find("Not-Signed-In").GetComponent<Text>().color = new Color32(255,0,0,alpha);
                }
            }else{
                fade = false;
            }
        }
    }

    public void ReportScore()
    {

        Debug.Log("Reporting score " + Mathf.Floor(gameController.distance) + " on leaderboard " + boardid);
        Social.ReportScore((long)Mathf.Floor(gameController.distance), boardid, success => {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });
    }

    public void OpenLeaderboard()
    {
        if(loggedIn){
        Social.ShowLeaderboardUI();
        }else{
            openNotLoggedIn();
        }
    }


    public void openNotLoggedIn(){
            alpha = 255;
            fade = true;
        }


}
