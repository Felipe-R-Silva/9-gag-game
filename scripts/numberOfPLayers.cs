using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberOfPLayers : MonoBehaviour {
    public GameObject Button_2playing;
    public GameObject Button_4playing;

    // Use this for initialization
    void Start () {
		if (Player.numberOfPlayers == 4)
        {
            Player.numberOfPlayersCanPlay = 4;
            //Button_4playing.SetActive(false);
            Button_4playing.GetComponent<Button>().interactable = false;

        }
        if (Player.numberOfPlayers == 2)
        {
            Player.numberOfPlayersCanPlay = 2;
            //Button_2playing.SetActive(false);
            Button_2playing.GetComponent<Button>().interactable = false;
        }
    }

    public void startGameHide(GameObject menu)
    {
        Player.GameStarted = 1;
        menu.SetActive(false);
    }
    public void two_2_players()
    {
        //Button_2playing.SetActive(false);
        //Button_4playing.SetActive(true);

        Button_2playing.GetComponent<Button>().interactable = false;
        Button_4playing.GetComponent<Button>().interactable = true;

        Player.numberOfPlayersCurentlyPlaying = 2;
        Player.numberOfPlayersCanPlay = 2;

        Player.CanPlay[0] = true;
        Player.CanPlay[1] = true;
        Player.CanPlay[2] = false;
        Player.CanPlay[3] = false;

    }
    public void four_4_players()
    {

        //Button_2playing.SetActive(true);
        //Button_4playing.SetActive(false);

        Button_2playing.GetComponent<Button>().interactable = true;
        Button_4playing.GetComponent<Button>().interactable = false;

        Player.numberOfPlayersCurentlyPlaying = 4;
        Player.numberOfPlayersCanPlay = 4;

        Player.CanPlay[0] = true;
        Player.CanPlay[1] = true;
        Player.CanPlay[2] = true;
        Player.CanPlay[3] = true;
    }
}
