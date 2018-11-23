using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectClass {//6 variables

    GameObject startReference;//ref1 
    GameObject targetReference; //bar
    GameObject RedbullCan_def, RedbullCan_anim; //defaultRedbullCan, flyingRedbulCan
    float fallingVelocity;
    Vector3 tempPosition;
    bool eventler = false;

    public FallingObjectClass(GameObject _startReference, GameObject _targetReference, GameObject _RedbullCan_def, GameObject _RedbullCan_anim, float _fallingVelocity, Vector3 _tempPosition)
    {
        startReference = _startReference;
        targetReference = _targetReference;
        RedbullCan_def = _RedbullCan_def;
        RedbullCan_anim = _RedbullCan_anim;
        fallingVelocity = _fallingVelocity;
        tempPosition = _tempPosition;
        instantiate();
    }

    void instantiate()
    {
        GameObject.Instantiate(RedbullCan_def, startReference.transform.position, Quaternion.identity);
        tempPosition = startReference.transform.position;
    }

    public void loop()
    {
        if (!eventler)
        {
            tempPosition.y += (-1) * Time.deltaTime;
            RedbullCan_def.transform.position = tempPosition;
        }
        else
        {
            var diffofx = targetReference.transform.position.x - tempPosition.x;
            var diffofy = targetReference.transform.position.y - tempPosition.y;

            var distance = Mathf.Sqrt(diffofx * diffofx + diffofy * diffofy);

            tempPosition.x += diffofx*fallingVelocity*Time.deltaTime / distance;
            tempPosition.y += diffofy*fallingVelocity*Time.deltaTime / distance;
            RedbullCan_anim.transform.position = tempPosition;

        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(RedbullCan_anim.transform.position);
            Debug.Log(RedbullCan_def.transform.position);

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Checks whether the mouse click hits a mesh collider
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if(hit.transform.gameObject.tag == "RedbullCan_Default")
                    {
                        eventler = true;

                        GameObject.Destroy(hit.transform.gameObject);
                        GameObject.Instantiate(RedbullCan_anim, hit.transform.position, Quaternion.identity);
                    }
                    

                }
            }
        }


    }
}
