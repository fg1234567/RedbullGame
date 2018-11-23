using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public GameObject defaultRedbullCan, flyingRedbulCan;
    public GameObject ref1, bar;
    //public Animator triggered;
    Vector3 tempPosition;
    Vector3 t = new Vector3(0, 0, 0);

    bool eventler = false;
    List<FallingObjectClass> mylist = new List<FallingObjectClass>();
    FallingObjectClass mynewobject;

    void dooo()
    {
        mynewobject = new FallingObjectClass(ref1, bar, defaultRedbullCan, flyingRedbulCan, 1.00f, t);
        mylist.Add(mynewobject);
    }

    void Start()
    {
        InvokeRepeating("dooo", 1.00f, 22.00f);
        /*Instantiate(defaultRedbullCan, ref1.transform.position, Quaternion.identity, ref1.transform);
        tempPosition = ref1.transform.position;  */
    }

    /*void DoThings(RaycastHit hit)
    {

        eventler = true;
        Destroy(hit.transform.gameObject);
        Instantiate(flyingRedbulCan, ref1.transform.position, Quaternion.identity, ref1.transform);
        //ref1.GetComponent<Animator>().SetBool("end", true);        
    }*/

    void Update()
    {

        foreach (FallingObjectClass foc in mylist)
        {
            foc.loop();
        }

        /* if (!eventler)
         {
             tempPosition.y += (-1)*Time.deltaTime;
             ref1.transform.position = tempPosition;
         }
         else
         {
             var diffofx = bar.transform.position.x - tempPosition.x;
             var diffofy = bar.transform.position.y - tempPosition.y;

             var distance = Mathf.Sqrt(diffofx*diffofx + diffofy*diffofy);

             tempPosition.x += diffofx*Time.deltaTime / distance;
             tempPosition.y += diffofy*Time.deltaTime / distance;
             ref1.transform.position = tempPosition;
         }


         if (Input.GetMouseButtonDown(0))
         {

             RaycastHit hit;
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

             //Checks whether the mouse click hits a mesh collider
             if (Physics.Raycast(ray, out hit, 100.0f))
             {
                 if (hit.transform != null)
                 {
                     DoThings(hit);
                 }
             }
         }*/


    }
}
