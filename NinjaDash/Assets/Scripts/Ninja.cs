using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ninja : MonoBehaviour
{
    public GameController gameController;
    
    public Leaderboard leaderboard;
    public GameEndAd gameEndAd;
    public List<Sprite> runningSprites;

    public Sprite flyingSprite;

    public Sprite idleSprite;
    
    public Sprite deathSprite;
    public Sprite currentSprite;

    public bool vertical;

    public string screenSide;
    public float yRotation;
    public float moveScale;
    public int currentrunningSprite;
    public string currentMovement;

    public bool secondChance;

    private int frameCounter;

    // Start is called before the first frame update
    void Start()
    {

        frameCounter = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentMovement == "idle"){
            currentSprite = idleSprite;
        }else if(currentMovement == "running"){
            
            if(frameCounter == 5){
            currentSprite = runningSprites[currentrunningSprite];

            if(currentrunningSprite == runningSprites.Count - 1){
                currentrunningSprite = 0;
            }else{
                currentrunningSprite++;
            }
            frameCounter = 0;
            }else{
                frameCounter++;
            }

            if(gameController.gameStarted && !gameController.startMoving){

                if(vertical){
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + (Vector3.up * (moveScale/5f)), moveScale/5f);

                    if(transform.localPosition.y >= 0){
                        gameController.startMoving = true;
                        StartCoroutine(gameController.throwShurikens());
                    }
                }else{
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + (Vector3.left * (moveScale/5f)), moveScale/5f);
                }

            }
        }else if(currentMovement == "flying"){

            currentSprite = flyingSprite;

            if(screenSide == "right"){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + (Vector3.right * moveScale), moveScale);
            }else{
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + (Vector3.left * moveScale), moveScale);
        }

        }else{

            currentSprite = deathSprite;

            if(transform.localPosition.y >= -2098f){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0f,-2098f,0f), moveScale);
            }

            
        }


        GetComponent<Image>().sprite = currentSprite;
        
    }

    public void OnTriggerEnter2D(Collider2D collider){


        if(collider.gameObject.name.Contains("Wall")){

            if(!vertical){
                GetComponent<BoxCollider2D>().size = new Vector2(70f,200f);
            }

            vertical = true;

            currentMovement = "running";

            if(screenSide == "right"){
                yRotation = 0f;
            }else{
                yRotation = 180f;
            }

            transform.localRotation = Quaternion.Euler(0f,yRotation, 90f);

        }else{

            gameController.startMoving = false;

            currentMovement = "death";

            if(secondChance){
                StartCoroutine(showGameOverPanel("Game-Over"));
            }else{
                StartCoroutine(showGameOverPanel("Continue"));
                
            }

        }
    }

    public void resetFromDeath(){

        GameObject.Find("Continue-Panel").transform.SetAsFirstSibling();
        transform.localPosition = new Vector3(0f,0f,0f);
        currentMovement = "flying";

        secondChance = true;

        gameController.startMoving = true;

        StartCoroutine(gameController.restartShurikens());
    }

    public IEnumerator showGameOverPanel(string panel){

        if(panel == "Game-Over"){
            leaderboard.ReportScore();
            gameEndAd.showInterstitial();
        }
        yield return new WaitForSeconds(.5f);
        GameObject.Find(panel + "-Panel").transform.SetAsLastSibling();
    }
}
