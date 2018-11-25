using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGetvaluefromdie : MonoBehaviour {

    public int value;
    public GameObject die;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        value = die.GetComponent<Die_d6>().value;
    }
}
