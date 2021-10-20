using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public GameController gameController;

    public float moveScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameController.startMoving){
            if(transform.localPosition.y <= -2088f){
                if(this.gameObject.name.Contains("Wall")){
                    transform.localPosition = new Vector3(transform.localPosition.x, 2098, 0);
                }
            }else{
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + (Vector3.down * moveScale), moveScale);
            }
        }
    }
}
