using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{

    public float moveScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.localPosition.y >= -2098f){
        transform.Rotate(0f,0f,10f,Space.Self);

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + (Vector3.down * moveScale), moveScale);
        }else{
            if(this != null){
                Destroy(gameObject);
            }
        }
    }
}
