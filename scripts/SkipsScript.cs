using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipsScript : MonoBehaviour {


    // Use this for initialization
    [SerializeField]
    public GameObject referencetochangecolor;
    public GameObject[] mySkips;
    public GameObject ScoreUI;
    // Update is called once per frame
    void FixedUpdate () {
        for (int cskip=0; cskip< mySkips.Length; cskip++)
        {
            if (Player.SkipsLeft[Player.Playing,cskip] && !mySkips[cskip].activeInHierarchy)
            {
                mySkips[cskip].SetActive(true);
            }
            if (!Player.SkipsLeft[Player.Playing,cskip] && mySkips[cskip].activeInHierarchy)
            {
                mySkips[cskip].SetActive(false);
            }
        }
		
	}
    public void Buttonskipturn(int whatskipIsthis)
    {
        if (Player.BuildTime)
        {

            Player.SkipsLeft[Player.Playing, whatskipIsthis] = false;


           // Player.Playing = (Player.Playing + 1) % Player.numberOfPlayersCurentlyPlaying;
            //curentCoo = new Vector2(-1, -1);
            Player.Playing = (Player.Playing + 1) % Player.numberOfPlayersCurentlyPlaying;
            int count = 0;
            while (referencetochangecolor.GetComponent<changecolor>().CountSkipsLeft()<=0)
            {
                if (count>Player.numberOfPlayersCurentlyPlaying)
                {
                    //game over
                    print("Game Is Over");
                    Player.GameStarted = 3;
                    referencetochangecolor.GetComponent<changecolor>().GameOver.SetActive(true);
                    break;
                }
                Player.Playing = (Player.Playing + 1) % Player.numberOfPlayersCurentlyPlaying;
                count++;
            }
            DiceData.rollCompleat = true;
            Player.BuildTime = false;
        }
    }
}
