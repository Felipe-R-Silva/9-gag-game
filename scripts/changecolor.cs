using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changecolor : MonoBehaviour {

    public GameObject GameOver;
    public GameObject matrixOBJ;
    public GameObject ScoreUI;
    public Material MouseHoverMaterialRef;


    [SerializeField]
    public Material[] MaterialRef;

    public Material baseMaterialRef;

    //coordinate mouse is pointing
    public Vector2 curentCoo;
    public Vector2 newCoo;

    private Vector2 BadErraseFix;//this is for the first move of mouse after the dice roll so it wont erase previous board(exeption handler)
    //curent player
    int curentPlayer;

    bool intersecOcured = true;//this is true if the curent matrix is overlaying the played tyles of other players
    bool colorConected = false;// this is true if the non diagonal surounding of the curent matrix has a color equal to the curent playing player
    bool isEdgeMap = false;// this is true if the player matrix is conected to the corner of the map that belongs to the curent player
    bool validPlacement = false;
    public bool firsterase = true;
    private object mySkips;

    // Use this for initialization
    void Start () {
        intersecOcured = true;
        colorConected = false;
        isEdgeMap = false;

        curentPlayer = Player.Playing;
        DiceData.roll_1 = -1;
        DiceData.roll_2 = -1;

        BadErraseFix= new Vector2(-1, -1);
        curentCoo = new Vector2(-1,-1);
        newCoo = curentCoo;
    }
	
	// Update is called once per frame
	void Update () {
        newCoo=this.GetComponent<MouseRay>().coordinates;
        int x = Mathf.RoundToInt(newCoo.x);
        int y = Mathf.RoundToInt(newCoo.y);
        int ox = Mathf.RoundToInt(curentCoo.x);
        int oy = Mathf.RoundToInt(curentCoo.y);

        if (curentCoo!= newCoo && Player.BuildTime || curentPlayer != Player.Playing && Player.BuildTime)
        {
            //avoid unitial state
            if (curentCoo != new Vector2(-1, -1))
            {
                //avoid problem in second state
                //avoid erasing board because of trash memory
                print("erase Time(x,y) " + ox + "," + oy);
                //erase
                    Erase(ox, oy, intersecOcured, colorConected, isEdgeMap);



               //SET FLAGS
               //check if is intersecting with other elements
               intersecOcured = IntersecOtherElem(x, y);
                //check if is in the edge of the map
                isEdgeMap = EdgeMap(x, y);
                //check if is close to same color (4 for loops)
                colorConected = Getsurounding(DiceData.roll_1, DiceData.roll_2, ref matrixOBJ.GetComponent<Matrix>().boardMatrix, x, y);
                //'''''''''''

                //draw
                DrawOnly(x, y, intersecOcured, colorConected, isEdgeMap);
                //>= because is a condition create a condition <

            }
            else {  }
        }
        //
        curentCoo = newCoo;
        curentPlayer = Player.Playing;
        //
        if (Input.GetKeyDown(KeyCode.Mouse1) && Player.BuildTime == true && validPlacement==true && Player.GameStarted==1)
        {
            Draw(x, y, intersecOcured, colorConected, isEdgeMap);
            AddPoints(ScoreUI.GetComponent<Score>().textDisplayPlayerPoints);
            //numberOfPlayersCurentlyPlaying I belive this should be 4 no matehr what.
            Player.Playing = (Player.Playing + 1) % Player.numberOfPlayersCurentlyPlaying;

            //check end condition
            while (CountSkipsLeft() <= 0 && Player.numberOfPlayersCanPlay > 0)
            {
                print("n of skips"+CountSkipsLeft()+"-ActivePlayers:"+ Player.numberOfPlayersCanPlay);
                //remove players that cant play
                if (Player.CanPlay[Player.Playing])
                {
                    print("remove 1 player");
                    Player.CanPlay[Player.Playing] = false;
                    Player.numberOfPlayersCanPlay--;
                }
                Player.Playing = (Player.Playing + 1) % Player.numberOfPlayersCurentlyPlaying;

            }
            if (Player.numberOfPlayersCanPlay > 0)
            {
                DiceData.rollCompleat = true;
                Player.BuildTime = false;
                validPlacement = false;
                curentCoo = new Vector2(-1, -1);
            }
            else
            {
                print("Game Is Over");
                Player.GameStarted = 3;
                GameOver.SetActive(true);
            }
            


        }

    }

    public bool Getsurounding( int D1 , int D2, ref GameObject[,] boardMatrix, int x , int y)
    {
        print("inside cc");
        //check if is close to same color (4 for loops)
        bool closetosamecolor = false;
        //x=30 y=20
        // first and second
        if (y + D2 <= 20) {
            for (int c = y; c < y + D2; c++)
            {
                if (x - 1 >= 0 )
                {
                    //matrixOBJ.GetComponent<Matrix>().boardMatrix[x-1, c].GetComponent<Renderer>().material = MaterialRef[Player.Playing];
                    //print(matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[x - 1, c]);
                    if (matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[x - 1, c] == Player.Playing)
                    {
                        closetosamecolor = true;
                        return closetosamecolor;
                    }
                }
                if (x + D1 < 30 ) {
                    //matrixOBJ.GetComponent<Matrix>().boardMatrix[x + D1, c].GetComponent<Renderer>().material = MaterialRef[Player.Playing];
                    //print(matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[x + D1, c]);
                    if (matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[x + D1, c] == Player.Playing)
                    {
                        closetosamecolor = true;
                        return closetosamecolor;
                    }
                }
            }
        }
        //third and forth
        if (x + D1 <= 30)
        {
            for (int k = x; k < x + D1; k++)
            {

                if (y - 1 >= 0)
                {
                    //matrixOBJ.GetComponent<Matrix>().boardMatrix[k, y - 1].GetComponent<Renderer>().material = MaterialRef[Player.Playing];
                    //print(matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, y - 1]);

                    if (matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, y - 1] == Player.Playing)
                    {
                        closetosamecolor = true;
                        return closetosamecolor;
                    }
                }
                if (y + D2 < 20)
                {
                    //matrixOBJ.GetComponent<Matrix>().boardMatrix[k, y + D2 ].GetComponent<Renderer>().material = MaterialRef[Player.Playing];
                    //print(matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, y + D2]);

                    if (matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, y + D2] == Player.Playing)
                    {
                        closetosamecolor = true;
                        return closetosamecolor;
                    }
                }
            }
        }
        return closetosamecolor;
    }

    public bool EdgeMap( int x, int y)
    {
        bool canto = false;
        //x=30 y=20

        //x=0  y=0  Yellow 4
        //x=0  y=19 Red 2
        //x=29 y=0  Blue  1
        //x=29 y=19 Green 3

        for (int k = x; k < x + DiceData.roll_1; k++)
        {
            
            for (int j = y; j < y + DiceData.roll_2; j++)
            {//k=x j=y
                if ((k == 0 && j == 0) && Player.Playing == 3)
                {
                    canto = true;
                    return canto;
                }
                if ((k == 0 && j == 19) && Player.Playing == 1)
                {
                    canto = true;
                    return canto;
                }
                if ((k == 29 && j == 0) && Player.Playing == 0)
                {
                    canto = true;
                    return canto;
                }
                if ((k == 29 && j == 19) && Player.Playing == 2)
                {
                    canto = true;
                    return canto;
                }
            }
        }
        return canto;
    }

    private bool IntersecOtherElem(int x, int y)
    {   bool isIntersecting=false;
        for (int k = x; k < x + DiceData.roll_1; k++)
        {
            for (int j = y; j < y + DiceData.roll_2; j++)
            {
                if (matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, j] != -1)
                {
                    isIntersecting = true;
                    break;
                }
            }
            if (isIntersecting)
            {
                break;
            }
        }
        return isIntersecting;
    }

    private void Erase(int ox, int oy , bool intersecOcured, bool colorConected, bool isEdgeMap)
    {
        if ((ox + DiceData.roll_1 <= 30) && (oy + DiceData.roll_2 <= 20))
        {
            if (!intersecOcured)
            { //remove color and data display to default
              //errase generic color
                for (int k = ox; k < ox + DiceData.roll_1; k++)
                {
                    for (int j = oy; j < oy + DiceData.roll_2; j++)
                    {
                        if (colorConected || isEdgeMap)
                        {
                            //update matrix data
                            //matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, j] = -1;
                        }
                        if (matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, j] == -1)
                        {
                            //change text display
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<referencetoText>().Mtext.text = " ";//"-1"
                            //errase old matrix color
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<Renderer>().material = baseMaterialRef;
                        }
                    }
                }
            }
        }
    }

    private void Draw(int x, int y, bool intersecOcured, bool colorConected, bool isEdgeMap)
    {
        if ((x + DiceData.roll_1 <= 30) && (y + DiceData.roll_2 <= 20))
        {

            if (!intersecOcured)
            {
                if (colorConected || isEdgeMap)
                {
                    for (int k = x; k < x + DiceData.roll_1; k++)
                    {
                        for (int j = y; j < y + DiceData.roll_2; j++)
                        {
                            
                            //update matrix data
                            matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, j] = Player.Playing;
                            //cahnge text display
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<referencetoText>().Mtext.text = Player.Playing.ToString();
                            //paint matrix
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<Renderer>().material = MaterialRef[Player.Playing];
                        }
                    }
                }
                else//draw a mousehover matrix
                {
                    for (int k = x; k < x + DiceData.roll_1; k++)
                    {
                        for (int j = y; j < y + DiceData.roll_2; j++)
                        {
                            //update matrix data
                            //matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, j] = Player.Playing;
                            //cahnge text display
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<referencetoText>().Mtext.text = " ";
                            //paint matrix
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<Renderer>().material = MouseHoverMaterialRef;
                        }
                    }

                }
            }
        }

    }

    private void DrawOnly(int x, int y, bool intersecOcured, bool colorConected, bool isEdgeMap)
    {
        if ((x + DiceData.roll_1 <= 30) && (y + DiceData.roll_2 <= 20))
        {

            if (!intersecOcured)
            {
                if (colorConected || isEdgeMap)
                {
                    for (int k = x; k < x + DiceData.roll_1; k++)
                    {
                        for (int j = y; j < y + DiceData.roll_2; j++)
                        {
                            validPlacement = true;
                            //update matrix data
                            //matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, j] = Player.Playing;
                            //cahnge text display
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<referencetoText>().Mtext.text = Player.Playing.ToString();
                            //paint matrix
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<Renderer>().material = MaterialRef[Player.Playing];
                        }
                    }
                }
                else//draw a mousehover matrix
                {
                    for (int k = x; k < x + DiceData.roll_1; k++)
                    {
                        for (int j = y; j < y + DiceData.roll_2; j++)
                        {
                            //update matrix data
                            //matrixOBJ.GetComponent<Matrix>().PlayerControlMatrix[k, j] = Player.Playing;
                            //cahnge text display
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<referencetoText>().Mtext.text = " ";
                            //paint matrix
                            matrixOBJ.GetComponent<Matrix>().boardMatrix[k, j].GetComponent<Renderer>().material = MouseHoverMaterialRef;
                        }
                    }

                }
            }
        }

    }

    private bool DontErase(int ox,int oy, Vector2 BadCase)
    {
        bool dontErase=false;

        if (BadCase.x==ox && BadCase.y==oy)
        {
            dontErase = true;
        }
        return dontErase;
    }

    public int CountSkipsLeft()
    {
        int nSkipsLeft=0;
        for (int cskip = 0; cskip < 3; cskip++)
        {
            if (Player.SkipsLeft[Player.Playing, cskip] ==true)
            {
                nSkipsLeft++;
            }
        }
        return nSkipsLeft;
    }
    public int AddPoints(Text[] textDisplayPlayerPoints  )
    {   int playerPoints=Player.playerPoints[Player.Playing];

        print("Added :" + DiceData.roll_1 * DiceData.roll_2 + "points to player" + Player.Playing);
        Player.playerPoints[Player.Playing] += DiceData.roll_1 * DiceData.roll_2;
        playerPoints = Player.playerPoints[Player.Playing];
        textDisplayPlayerPoints[Player.Playing].text =Player.playerNames[Player.Playing]+"-"+playerPoints.ToString();
        return playerPoints;
    }
}
