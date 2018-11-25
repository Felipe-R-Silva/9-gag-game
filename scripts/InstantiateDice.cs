using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class InstantiateDice : MonoBehaviour {


    public GameObject Blue_Dice;
    public GameObject Red_Dice;
    public GameObject Green_Dice;
    public GameObject Yellow_Dice;

    [SerializeField]
    GameObject colorchangescript;

    [SerializeField]
    GameObject[] Dice_P;

    public int thrust;

    // Use this for initialization
    bool switchd;
    void Start() {
        switchd = true;

    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Mouse0)&& Player.GameStarted==1)
        {
            if (Player.IsPlayTime == true && DiceData.rollCompleat == true && Player.BuildTime==false)
            {
                InstantiateDiceFunction();
               
            }
        }

    }
    public void InstantiateDiceFunction()
    {
        DiceData.rollCompleat = false;

        GameObject instantiated;
        GameObject instantiated2;
        //instantiate different colors based on the player playing

        instantiated = Instantiate(Dice_P[Player.Playing], transform.position, transform.rotation);


        float rand1 = Random.Range(1000.0f, 4000.0f);
        float rand2 = Random.Range(-2000.0f, 2000.0f);
        float rand3 = Random.Range(-2000.0f, 2000.0f);
        instantiated.GetComponent<Rigidbody>().AddForce(instantiated.transform.forward * thrust, ForceMode.Impulse);

        instantiated.GetComponent<Rigidbody>().AddTorque(instantiated.transform.up * rand1, ForceMode.Impulse);
        instantiated.GetComponent<Rigidbody>().AddTorque(instantiated.transform.right * rand2, ForceMode.Impulse);
        instantiated.GetComponent<Rigidbody>().AddTorque(instantiated.transform.forward * rand3, ForceMode.Impulse);



        instantiated2 = Instantiate(Dice_P[Player.Playing], transform.position + new Vector3(30, 0, 0), transform.rotation);




        rand1 = Random.Range(1000.0f, 4000.0f);
        rand2 = Random.Range(-2000.0f, 2000.0f);
        rand3 = Random.Range(-2000.0f, 2000.0f);
        instantiated2.GetComponent<Rigidbody>().AddForce(instantiated2.transform.forward * thrust, ForceMode.Impulse);

        instantiated2.GetComponent<Rigidbody>().AddTorque(instantiated2.transform.up * rand1, ForceMode.Impulse);
        instantiated2.GetComponent<Rigidbody>().AddTorque(instantiated2.transform.right * rand2, ForceMode.Impulse);
        instantiated2.GetComponent<Rigidbody>().AddTorque(instantiated2.transform.forward * rand3, ForceMode.Impulse);



        StartCoroutine(GetDiceNumber(instantiated, instantiated2));

    }

    public IEnumerator GetDiceNumber( GameObject Dice1OBJ ,  GameObject Dice2OBJ)
    {
       

        yield return new WaitForSeconds(5);

        int value1 = Dice1OBJ.GetComponent<Die_d6>().value;
        int value2 = Dice2OBJ.GetComponent<Die_d6>().value;

        DiceData.roll_1 = value1;
        DiceData.roll_2 = value2;

        print("x :" + DiceData.roll_1 + "y :" + DiceData.roll_2);
        Destroy(Dice1OBJ, 1);
        Destroy(Dice2OBJ, 1);

        if ((DiceData.roll_1>0 && DiceData.roll_1 <= 6) && (DiceData.roll_2 > 0 && DiceData.roll_2 <= 6)) {
            Player.BuildTime = true;
        }
        else
        {
            InstantiateDiceFunction();
        }
        


        


    }
}












//void Update()
//{

//    if (Input.GetKeyDown(KeyCode.Mouse0))
//    {
//        if (Player.IsPlayTime == true)
//        {
//            if (switchd)
//            {

//                instantiated = Instantiate(Blue_Dice, transform.position, transform.rotation);
//                Destroy(instantiated, 5);
//                //instantiated = Instantiate(Blue_Dice, transform.position, transform.rotation);
//            }
//            else
//            {
//                instantiated = Instantiate(Red_Dice, transform.position, transform.rotation);
//                Destroy(instantiated, 5);
//                // instantiated =Instantiate(Red_Dice, transform.position, transform.rotation);
//            }
//            switchd = !switchd;
//            float rand1 = Random.Range(1000.0f, 4000.0f);
//            float rand2 = Random.Range(-2000.0f, 2000.0f);
//            float rand3 = Random.Range(-2000.0f, 2000.0f);
//            instantiated.GetComponent<Rigidbody>().AddForce(instantiated.transform.forward * thrust, ForceMode.Impulse);

//            instantiated.GetComponent<Rigidbody>().AddTorque(instantiated.transform.up * rand1, ForceMode.Impulse);
//            instantiated.GetComponent<Rigidbody>().AddTorque(instantiated.transform.right * rand2, ForceMode.Impulse);
//            instantiated.GetComponent<Rigidbody>().AddTorque(instantiated.transform.forward * rand3, ForceMode.Impulse);
//        }
//    }
//}
