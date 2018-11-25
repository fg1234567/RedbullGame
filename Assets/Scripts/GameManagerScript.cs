using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    public GameObject defaultRedbullCan, flyingRedbulCan, ref1, bar;
    public Slider scoreBar;
    int listOrder = -1;
    float velocity = 1.50f, timeCount = 20;
    List<FallingObject> mFallingObjectList = new List<FallingObject>();
    FallingObject mFallingObject;
    public Text timerText;

    void CreateObject(){
        if(0.00f <= timeCount)
        {
            listOrder += 1;
            mFallingObject = new FallingObject(ref1, bar, defaultRedbullCan, flyingRedbulCan, velocity, listOrder);
            mFallingObjectList.Add(mFallingObject);
        }
    }

    void Start(){

        InvokeRepeating("CreateObject", 2.00f, 3.50f);
        timerText.text = "20";
    }

    void Update(){

        if (Input.GetMouseButtonDown(0)){

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.00f)){
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

            if (!mFallingObjectList[i].hasUpdatedScore && mFallingObjectList[i].hasReached){
                mFallingObjectList[i].hasUpdatedScore = true;
                if (scoreBar.value <= 50){
                    scoreBar.value += 1;
                    var tempPos = bar.transform.position;
                    tempPos.x += 0.20f;
                    tempPos.y += 0.02f;
                    bar.transform.position = tempPos;
                }
            }
        }

        if(scoreBar.value == 50) { timeCount = 0.00f; }
        if( 0.00f > timeCount) { TimeHasEnded(); }
        timeCount -= Time.deltaTime;
        if ((int)timeCount >= 0) { timerText.text = ((int)timeCount).ToString(); }
        else { timerText.text = "0"; }    
    }

    void TimeHasEnded(){
        Invoke("ChangeScene", 4.00f);
    }
    void ChangeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
