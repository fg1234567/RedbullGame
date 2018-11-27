using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingObject{//6 variables

    GameObject startReference, targetReference, RedbullCan_def, RedbullCan_anim;//where the model will be first instanciated, where will be the model moving towards after clicked,  first model, second model with wings
    GameObject go;//local game object to assign position
    Vector3 tempPosition;// used to get position of key locations 
    bool isHit = false;
    public bool hasReached = false, hasUpdatedScore = false, isDestroyed = false;
    int id;//list order
    public float fallingVelocity;
    public int scoreValue;
    GameObject displayedScoreGameObject;
    FloatingText mFloatingText;

    public FallingObject(GameObject _startReference, GameObject _targetReference, GameObject _RedbullCan_def, GameObject _RedbullCan_anim, float _fallingVelocity, int _score, GameObject _displayedScoreGameObject, int _id)
    {
        startReference = _startReference;
        targetReference = _targetReference;
        RedbullCan_def = _RedbullCan_def;
        RedbullCan_anim = _RedbullCan_anim;
        fallingVelocity = _fallingVelocity;
        scoreValue = _score;
        displayedScoreGameObject = _displayedScoreGameObject;
        id = _id;
        InstanciateGameObject();
    }

    void InstanciateGameObject(){
        go = GameObject.Instantiate(RedbullCan_def, startReference.transform.position, Quaternion.identity);
        tempPosition = startReference.transform.position;
        go.name = "RedbullCan_Default" + id;
    }

    public void move()
    {
        if (!isHit && !hasReached && !isDestroyed){
            tempPosition.y += fallingVelocity*(-1) * Time.deltaTime;
            go.transform.position = tempPosition;

            if (go.transform.position.y < -6.00f){
                DestroyObject();
            }
        }
        else if(!hasReached && !isDestroyed){
            var diffofx = targetReference.transform.position.x - tempPosition.x;
            var diffofy = targetReference.transform.position.y - tempPosition.y;

            var distance = Mathf.Sqrt(diffofx * diffofx + diffofy * diffofy);

            tempPosition.x += diffofx*fallingVelocity*Time.deltaTime / distance;
            tempPosition.y += diffofy*fallingVelocity*Time.deltaTime / distance;
            go.transform.position = tempPosition;

            if (diffofx < 0.01f && diffofy < 0.01f){
                ObjectReachedTarget();
            }
        }
    }

    public void ChangeModel(RaycastHit touchedObject){
        isHit = true;
        GameObject.Destroy(touchedObject.transform.gameObject);
        go = GameObject.Instantiate(RedbullCan_anim, touchedObject.transform.position, Quaternion.identity);
    }

    public void DisplayScore(RaycastHit touchedObject){

        displayedScoreGameObject.GetComponentInChildren<TextMesh>().text = "+" + scoreValue.ToString();
        mFloatingText = new FloatingText(displayedScoreGameObject, touchedObject);
        Debug.Log("3" + touchedObject.transform.position);
    }

    void ObjectReachedTarget(){
        hasReached = true;
        DestroyObject();
    }

    void DestroyObject(){
        isDestroyed = true;
        GameObject.Destroy(go);
    }
}
