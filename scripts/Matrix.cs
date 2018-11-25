using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour {

    //int size=10;
    // Use this for initialization
    public GameObject gridPrefab;

    public GameObject materialObj;

    public int sizeX;
    public int sizeY;

    int distanceBetwinPices;
    [SerializeField]
    public GameObject[,] boardMatrix = new GameObject[30,20];
    public int[,] PlayerControlMatrix = new int[30, 20];

    void Start () {
        distanceBetwinPices = 10;
        sizeX = 30;
        sizeY = 20;
        for (int i=0;i< sizeX; i++) {
            for (int j = 0; j < sizeY; j++)
            {
                //print all the matrix values
                //print(i + "-" + j);
                PlayerControlMatrix[i, j] = -1;
                boardMatrix[i, j] = Instantiate(gridPrefab, new Vector3(i * distanceBetwinPices, 0, j * distanceBetwinPices), transform.rotation);
                boardMatrix[i, j].name = i + "," + j;
            }
        }

        //boardMatrix[29, 0].GetComponent<Renderer>().material = materialObj.GetComponent<changecolor>().MaterialRef[0];
        //boardMatrix[29, 0].GetComponent<referencetoText>().Mtext.text = 0.ToString();
        //PlayerControlMatrix[29, 0] = 0;

        //boardMatrix[10, 10].GetComponent<Renderer>().material = materialObj.GetComponent<changecolor>().MaterialRef[0];
        //boardMatrix[10, 10].GetComponent<referencetoText>().Mtext.text = 0.ToString();
        //PlayerControlMatrix[10, 10] = 0;

        //boardMatrix[29, 19].GetComponent<Renderer>().material = materialObj.GetComponent<changecolor>().MaterialRef[2];
        //boardMatrix[29, 19].GetComponent<referencetoText>().Mtext.text = 2.ToString();
        //PlayerControlMatrix[29, 19] = 2;

        //boardMatrix[0, 19].GetComponent<Renderer>().material = materialObj.GetComponent<changecolor>().MaterialRef[1];
        //boardMatrix[0, 19].GetComponent<referencetoText>().Mtext.text = 1.ToString();
        //PlayerControlMatrix[0, 19] = 1;

        //boardMatrix[0, 0].GetComponent<Renderer>().material = materialObj.GetComponent<changecolor>().MaterialRef[3];
        //boardMatrix[0, 0].GetComponent<referencetoText>().Mtext.text = 3.ToString();
        //PlayerControlMatrix[0, 0] = 3;

    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {

                    Destroy(boardMatrix[i, j]);
                }
            }
        }
       

    }
}
