using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IPointerDownHandler
{

    public Ninja ninja;

    public bool gameStarted;
    public bool startMoving;

    public float distance;

    public GameObject shuriken;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(throwShurikens());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(startMoving){
            distance += .1f;
        }

        GameObject.Find("Distance").GetComponent<Text>().text = Mathf.Floor(distance).ToString();
        GameObject.Find("Distance-Game-Over").GetComponent<Text>().text = Mathf.Floor(distance).ToString();

        if((int)Mathf.Floor(distance) > PlayerPrefs.GetInt("Best", 0)){
            PlayerPrefs.SetInt("Best", (int)Mathf.Floor(distance));
        }

        GameObject.Find("Best-Game-Over").GetComponent<Text>().text = PlayerPrefs.GetInt("Best", 0).ToString();
    }

    public void OnPointerDown(PointerEventData eventData){

        
            if(!gameStarted){
                gameStarted = true;
                GameObject.Find("Start-Text").transform.SetAsFirstSibling();
                ninja.currentMovement = "running";
            }else if(startMoving){
            
                if(ninja.screenSide == "left"){
                    ninja.screenSide = "right";
                    ninja.transform.localRotation = Quaternion.Euler(0f,180f,60f);
                }else{
                    ninja.screenSide = "left";
                    ninja.transform.localRotation = Quaternion.Euler(0f,0f,60f);
                }

                ninja.currentMovement = "flying";
            
            }else{
                Debug.Log("You Can't Click Before the Screen Starts Moving!");
            }

    }

    public IEnumerator restartShurikens(){

        yield return new WaitForSeconds(1f);

        StartCoroutine(throwShurikens());
    }

    

    public IEnumerator throwShurikens(){

        while(startMoving){

            int randomNum = Random.Range(0,2);

            yield return new WaitForSeconds(.6f);

            if(distance < 200f){

                shuriken.GetComponent<Shuriken>().moveScale = 10f;
            
            }else{
                shuriken.GetComponent<Shuriken>().moveScale = distance / 20f;
            }

            Instantiate(shuriken, GameObject.Find("Shuriken-Spawner-"+randomNum).transform, false);
        }
    }

    public void restartGame(){

        SceneManager.LoadScene("Gameplay");
    }

}
