using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public GameObject defaultRedbullCan, flyingRedbulCan;
    public GameObject ref1, bar;
    float velocity = 1.50f, v = 0.50f;
    List<FallingObject> mFallingObjectList = new List<FallingObject>();
    FallingObject mFallingObject;
    int listOrder = -1;
    public Slider scoreBar;


    void CreateObject(){
        listOrder += 1;
        velocity += v;
        v -= 0.01f;

        mFallingObject = new FallingObject(ref1, bar, defaultRedbullCan, flyingRedbulCan, velocity, listOrder);
        mFallingObjectList.Add(mFallingObject);
    }

    void Start(){
        InvokeRepeating("CreateObject", 2.00f, 5.00f);
    }

    void Update(){

        if (Input.GetMouseButtonDown(0)){

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)){
                if (hit.transform != null){
                    if (hit.transform.gameObject.tag == "RedbullCan_Default"){
                       for (int i = 0; i<mFallingObjectList.Count; i++){
                            if(hit.transform.gameObject.name == "RedbullCan_Default" + i){
                                mFallingObjectList[i].changeModel(hit);

                            }                       
                        } 
                    }
                }
            }
        }

        for (int i = 0; i<mFallingObjectList.Count; i++){
            mFallingObjectList[i].move();
            if(mFallingObjectList[i].hasReached && !mFallingObjectList[i].hasScoreUpdated){
                scoreBar.value += 1;
                mFallingObjectList[i].hasScoreUpdated = true;

            }
        }
    }
}
