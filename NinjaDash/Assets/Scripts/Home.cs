using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using ConsentManager.Api;
using ConsentManager.Common;
using AppodealAppTracking.Unity.Api;
using AppodealAppTracking.Unity.Common;

public class Home : MonoBehaviour, IConsentFormListener, IConsentInfoUpdateListener, IAppodealAppTrackingTransparencyListener
{

    private string appKey;

    private ConsentManager.Api.ConsentManager consentManager;
    // Start is called before the first frame update
    void Start()
    {

        #if UNITY_IOS
        appKey = "ca4b7d37899cb71faea8512a6d84af164418e2d3ade5453b";
        #elif UNITY_ANDROID
        appKey = "0c672fd3651ae64c5a8a44e33b2b5267489d43779fcf90df";
        #else
        appKey = "";
        #endif

        /// Initialisation 

    consentManager = ConsentManager.Api.ConsentManager.getInstance();

    consentManager.requestConsentInfoUpdate(appKey, this);

// Get current ShouldShow status
Consent.ShouldShow consentShouldShow = consentManager.shouldShowConsentDialog();

if (consentShouldShow == Consent.ShouldShow.TRUE){
    
        StartCoroutine(showConsentForm());
        
}else{

    AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);

    Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentManager.getConsent());

}

        AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);

        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentManager.getConsent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region ConsentFormListener

public void onConsentFormLoaded() { print("ConsentFormListener - onConsentFormLoaded");}

public void onConsentFormError(ConsentManagerException exception) { print($"ConsentFormListener - onConsentFormError, reason - {exception.getReason()}");}

public void onConsentFormOpened() { print("ConsentFormListener - onConsentFormOpened");}

public void onConsentFormClosed(Consent consent) { 

    AppodealAppTrackingTransparency.RequestTrackingAuthorization(this);

    Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consent);
}

#endregion


public void AppodealAppTrackingTransparencyListenerNotDetermined(){ 
    
    Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentManager.getConsent());

 }
public void AppodealAppTrackingTransparencyListenerRestricted(){ 
    
    Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentManager.getConsent());

 }
public void AppodealAppTrackingTransparencyListenerDenied() { 
    
    Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentManager.getConsent());

}
public void AppodealAppTrackingTransparencyListenerAuthorized() { 
    
    Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consentManager.getConsent());
 }


 public IEnumerator showConsentForm(){

    ConsentForm consentForm = new ConsentForm.Builder().withListener(this).build();
    consentForm?.load();

    while(!consentForm.isLoaded()){
        yield return new WaitForSeconds(.01f);
    }

        consentForm.showAsDialog();
    }

    #region ConsentInfoUpdateListener

public void onConsentInfoUpdated(Consent consent) { 
    print("onConsentInfoUpdated");
    Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, consent);
    }

public void onFailedToUpdateConsentInfo(ConsentManagerException error) { print($"onFailedToUpdateConsentInfo Reason: {error.getReason()}");}

#endregion


    public void startGame(){

        SceneManager.LoadScene("Gameplay");
    }
}
