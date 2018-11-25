using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingObject{//6 variables

    GameObject startReference, targetReference, RedbullCan_def, RedbullCan_anim;//where the model will be first instanciated, where will be the model moving towards after clicked,  first model, second model with wings
    GameObject go;//local game object to assign position
    Vector3 tempPosition;// used to get position of key locations 
    bool isHit = false;
    public bool hasReached = false, hasUpdatedScore = false;
    int id;//list order
    float fallingVelocity;

    public FallingObject(GameObject _startReference, GameObject _targetReference, GameObject _RedbullCan_def, GameObject _RedbullCan_anim, float _fallingVelocity, int _id)
    {
        startReference = _startReference;
        targetReference = _targetReference;
        RedbullCan_def = _RedbullCan_def;
        RedbullCan_anim = _RedbullCan_anim;
        fallingVelocity = _fallingVelocity;
        id = _id;
        instantiate();
    }

    void instantiate(){
        go = GameObject.Instantiate(RedbullCan_def, startReference.transform.position, Quaternion.identity);
        tempPosition = startReference.transform.position;
        go.name = "RedbullCan_Default" + id;
    }

    public void move()
    {
        if (!isHit && !hasReached){
            tempPosition.y += fallingVelocity*(-1) * Time.deltaTime;
            go.transform.position = tempPosition;

            if (go.transform.position.y < -6.00f){
                objectReachedTarget();
            }
        }
        else if(!hasReached){
            var diffofx = targetReference.transform.position.x - tempPosition.x;
            var diffofy = targetReference.transform.position.y - tempPosition.y;

            var distance = Mathf.Sqrt(diffofx * diffofx + diffofy * diffofy);

            tempPosition.x += diffofx*fallingVelocity*Time.deltaTime / distance;
            tempPosition.y += diffofy*fallingVelocity*Time.deltaTime / distance;
            go.transform.position = tempPosition;

            if (diffofx < 0.01f && diffofy < 0.01f){
                objectReachedTarget();
            }
        }
    }

    public void changeModel(RaycastHit touchedObject){
        isHit = true;
        GameObject.Destroy(touchedObject.transform.gameObject);
        go = GameObject.Instantiate(RedbullCan_anim, touchedObject.transform.position, Quaternion.identity);
    }

    void objectReachedTarget(){
        hasReached = true;
        GameObject.Destroy(go);
    }
}
