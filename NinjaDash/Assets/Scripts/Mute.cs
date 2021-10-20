using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{

    public Sprite mute;
    public Sprite unmute;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("Mute", 0) == 0){
            GetComponent<Image>().sprite = unmute;
            GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = false;
        }else{
            GetComponent<Image>().sprite = mute;
            GameObject.Find("Background-Music").GetComponent<AudioSource>().mute = true;
        }
    }

    public void toggleMute(){
        if(PlayerPrefs.GetInt("Mute", 0) == 0){
            PlayerPrefs.SetInt("Mute", 1);
            
        }else{
            PlayerPrefs.SetInt("Mute", 0);
        }
    }
}
