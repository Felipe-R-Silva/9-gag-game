using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseRay : MonoBehaviour {

    public Camera camera;

    public Vector2 coordinates;
    bool ismapObj=false;

    private void Start()
    {
        coordinates =new Vector2(-1,-1);
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //if (Input.GetMouseButtonDown(0)){
            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                
                var sStrings = objectHit.name.Split(","[0]);

                coordinates.x = float.Parse(sStrings[0]);
                coordinates.y = float.Parse(sStrings[1]);

            //show the number of the cell you are hovering over
                //print(sStrings[0]);
                //print(sStrings[1]);
            }
        //}
    }

}
