using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    public GameObject defaultRedbullCan1, flyingRedbulCan1, defaultRedbullCan2, flyingRedbulCan2, ref1, bar;
    public Slider scoreBar;
    int listOrder = -1;
    float velocity1 = 1.50f, velocity2 = 3.00f, timeCount = 50;
    int score1 = 10, score2 = 20;
    List<FallingObject> mFallingObjectList = new List<FallingObject>();
    FallingObject mFallingObject;
    public Text timerText;
    public GameObject displayedScoreGameObject;//Will have animation as prefab
    bool check = true;

    void CreateObject(){
        if(0.00f <= timeCount){
            if (check){
                listOrder += 1;
                mFallingObject = new FallingObject(ref1, bar, defaultRedbullCan1, flyingRedbulCan1, velocity1, score1, displayedScoreGameObject, listOrder);
                mFallingObjectList.Add(mFallingObject);
                check = !check;
            }
            else{
                listOrder += 1;
                mFallingObject = new FallingObject(ref1, bar, defaultRedbullCan2, flyingRedbulCan2, velocity2, score2, displayedScoreGameObject, listOrder);
                mFallingObjectList.Add(mFallingObject);
                check = !check;
            }
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
                                mFallingObjectList[i].ChangeModel(hit);                                
                                mFallingObjectList[i].DisplayScore(hit);
                                Vector3 v = new Vector3(0.00f, 0.00f, 0.00f);
                                //GameObject go = Instantiate(displayedScore.gameObject, hit.transform.position + v , Quaternion.identity);
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
                    if(mFallingObjectList[i].fallingVelocity == velocity1) { scoreBar.value += 1; }
                    if(mFallingObjectList[i].fallingVelocity == velocity2) { scoreBar.value += 2; }                    
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
        //Invoke("ChangeScene", 4.00f);
    }
    void ChangeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
