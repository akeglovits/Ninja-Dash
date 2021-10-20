using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class GameEndAd : MonoBehaviour, IInterstitialAdListener
{
    // Start is called before the first frame update
    void Start()
    {
        Appodeal.setInterstitialCallbacks(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Interstitial callback handlers 
public void onInterstitialLoaded(bool isPrecache) { print("Interstitial loaded"); } // Called when interstitial was loaded (precache flag shows if the loaded ad is precache) 
public void onInterstitialFailedToLoad() { print("Interstitial failed"); } // Called when interstitial failed to load 
public void onInterstitialShowFailed() { print ("Interstitial show failed"); } // Called when interstitial was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative) 
public void onInterstitialShown() { 
    
    GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = true;
 } // Called when interstitial is shown 
public void onInterstitialClosed() { 

    if(PlayerPrefs.GetInt("Mute", 0) == 0){
        GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = false;
    }
 } // Called when interstitial is closed 
public void onInterstitialClicked() { print("Interstitial clicked"); } // Called when interstitial is clicked 
public void onInterstitialExpired() { print("Interstitial expired"); } // Called when interstitial is expired and can not be shown
 #endregion

 public void showInterstitial(){

     if(PlayerPrefs.GetInt("ShowAd", 0) == 1){
     if(Appodeal.isLoaded(Appodeal.INTERSTITIAL)){
         PlayerPrefs.SetInt("ShowAd", 0);
         Appodeal.show(Appodeal.INTERSTITIAL);
     }
     }else{
         PlayerPrefs.SetInt("ShowAd", 1);
     }
 }
}
