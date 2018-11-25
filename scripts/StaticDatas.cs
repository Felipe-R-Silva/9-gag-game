using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DiceData
{
    public static bool rollCompleat = true;
    public static int roll_1 = -1;
    public static int roll_2 = -1;
}
public class Player
{
    public static int GameStarted = 0; //0 = not started  1=happening 2=ended
    public static bool[,] SkipsLeft = new bool[4,3] { { true, true, true }, { true, true, true }, { true, true, true }, { true, true, true } };
    public static bool[] CanPlay = new bool[] { true,true, true,true };
    public static bool IsPlayTime = true;
    public static bool BuildTime = false;
    public static int numberOfPlayers = 4;
    public static int numberOfPlayersCurentlyPlaying = 4;
    public static int numberOfPlayersCanPlay = 4;
    public static int Playing = 0;
    public static string[] playerNames = new string[] { "P1", "P2", "P3", "P4" };
    public static int[] playerPoints = new int[] { 0, 0, 0, 0 };
}
public class StaticDatas : MonoBehaviour {

    [SerializeField]
    Text[] P;

    private void Start()
    {
        for (int x = 0; x < Player.numberOfPlayers; x++)
        {
            P[x].text = Player.playerNames[x] + "Turn";
        }
    }


    private void FixedUpdate()
    {

        P[Player.Playing].gameObject.SetActive(true);

        for (int y = 0; y < Player.numberOfPlayers; y++)
        {

            if (y != Player.Playing)
            {
                P[y].gameObject.SetActive(false);

            }
            else
            {
                P[y].text = Player.playerNames[y] + "Turn";
            }

        }

    }


    public void RestartScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
