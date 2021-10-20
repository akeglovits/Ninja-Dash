using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class ContinueGame : MonoBehaviour, IRewardedVideoAdListener
{

    public Ninja ninja;
    public GameEndAd gameEndAd;
    public Leaderboard leaderboard;
    // Start is called before the first frame update
    void Start()
    {
        Appodeal.setRewardedVideoCallbacks(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Rewarded Video callback handlers 
public void onRewardedVideoLoaded(bool isPrecache) { print("Video loaded"); } //Called when rewarded video was loaded (precache flag shows if the loaded ad is precache). 
public void onRewardedVideoFailedToLoad() { print("Video failed"); } // Called when rewarded video failed to load 
public void onRewardedVideoShowFailed() { print ("Video show failed"); } // Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative) 
public void onRewardedVideoShown() { print("Video shown"); } // Called when rewarded video is shown 
public void onRewardedVideoClicked() { print("Video clicked"); } // Called when reward video is clicked 
public void onRewardedVideoClosed(bool finished) { 
    
    if(finished){
        ninja.resetFromDeath();
    }
 } // Called when rewarded video is closed 
public void onRewardedVideoFinished(double amount, string name) { print("Reward: " + amount + " " + name); } // Called when rewarded video is viewed until the end 
public void onRewardedVideoExpired() { print("Video expired"); } //Called when rewarded video is expired and can not be shown 
#endregion

    public void showRewardedVideo(){

        if(Appodeal.isLoaded(Appodeal.REWARDED_VIDEO)){
            Appodeal.show(Appodeal.REWARDED_VIDEO);

            #if UNITY_EDITOR
                ninja.resetFromDeath();
            #endif

        }else{
            ninja.resetFromDeath();
        }

    }

    public void gameOver(){
        leaderboard.ReportScore();
        gameEndAd.showInterstitial();
        GameObject.Find("Continue-Panel").transform.SetAsFirstSibling();
        GameObject.Find("Game-Over-Panel").transform.SetAsLastSibling();
    }
}
