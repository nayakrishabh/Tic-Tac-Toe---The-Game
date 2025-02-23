using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState {
    START,
    PLAYERXTURN,
    PLAYEROTURN,
    PLAYEROAITURN,
    WINX,
    WINO,
    DRAW,
}

public enum winCondition {
    ROW,
    COLUMN,
    DIAGONAL,
    ANTI_DIAGONAL,
}

public class TurnBasedSystem : MonoBehaviour
{
    public GameObject PlayerX;
    public GameObject PlayerO;

    private GameObject PlayerXOB;
    private GameObject PlayerOOB;

    public Transform PlayerXPH;
    public Transform PlayerOPH;
    private Transform textComponentX;
    private Transform textComponentO;

    public GameState gameState;

    public static TurnBasedSystem Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        gameState = GameState.START;
        StartCoroutine(setUpMatch());
        Debug.Log("Game Started");
    }

    private IEnumerator setUpMatch() {

        PlayerXOB = Instantiate(PlayerX, PlayerXPH);
        PlayerOOB = Instantiate(PlayerO, PlayerOPH);

        textComponentX = PlayerXOB.transform.GetChild(0);
        textComponentO = PlayerOOB.transform.GetChild(0);

        yield return new WaitForSeconds(0.1f);

        gameState = GameState.PLAYERXTURN;

        if(gameState == GameState.PLAYERXTURN) {
            PlayerXOB.GetComponent<Image>().color = Color.green;
            textComponentO.gameObject.SetActive(false);
            textComponentX.gameObject.SetActive(true);
        }
        else if (gameState == GameState.PLAYEROTURN) {
            PlayerXOB.GetComponent<Image>().color = Color.green;
            textComponentX.gameObject.SetActive(false);
            textComponentO.gameObject.SetActive(true);
        }
    }
    
    public void winCheck(Button[,] posButtons) {
        GameObject winMesOB = GameManager.instance.getWinMC();
        if (checkWinConditionX(posButtons)){
            gameState = GameState.WINX;
            winMesOB.SetActive(true);
            winMesOB.GetComponent<WinMConnector>().winText.text = "X Wins";
            Debug.Log("X Wins");
        }
        else if(checkWinConditionO(posButtons)) {
            gameState = GameState.WINO;
            winMesOB.SetActive(true);
            winMesOB.GetComponent<WinMConnector>().winText.text = "O Wins";
            Debug.Log("O Wins");
        }
        else if (checkDrawCondition(posButtons)) {
            gameState = GameState.DRAW;
            winMesOB.SetActive(true);
            winMesOB.GetComponent<WinMConnector>().winText.text = "Draw Match";
            Debug.Log("Draw Match");

        }
    }
    private bool checkDrawCondition(Button[,] pButtons) {
        for(int i = 0;i <pButtons.GetLength(0); i++) {
            for(int j = 0; j < pButtons.GetLength(1); j++) {
                if (pButtons[i,j].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.UNTAGGED) {
                    return false;
                }
            }
        }
        return true;
    }

    private bool checkWinConditionX(Button[,] posButtons) {
        for(int i = 0; i <3; i++) {
            if (posButtons[i, 0].GetComponent<ButtonTag>().ButtonTG == posButtons[i,1].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[i, 1].GetComponent<ButtonTag>().ButtonTG == posButtons[i,2].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[i,2].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.X) {//Checks Row
                return true; 
            }
            else if (posButtons[0, i].GetComponent<ButtonTag>().ButtonTG == posButtons[1, i].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[1, i].GetComponent<ButtonTag>().ButtonTG == posButtons[2, i].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[2, i].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.X) {//Checks Column
                return true;
            }
        }

        if (posButtons[0, 0].GetComponent<ButtonTag>().ButtonTG == posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG == posButtons[2, 2].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[2, 2].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.X) {//Checks Diagonal
            return true;
        }
        else if (posButtons[0, 2].GetComponent<ButtonTag>().ButtonTG == posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG &&
            posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG == posButtons[2, 0].GetComponent<ButtonTag>().ButtonTG &&
            posButtons[2, 0].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.X) {//Checks Anti-Diagonal
            return true;
        }

        return false;
    }
    private bool checkWinConditionO(Button[,] posButtons) {
        for (int i = 0; i < 3; i++) {
            if (posButtons[i, 0].GetComponent<ButtonTag>().ButtonTG == posButtons[i, 1].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[i, 1].GetComponent<ButtonTag>().ButtonTG == posButtons[i, 2].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[i, 2].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.O) {//Checks Row
                return true;
            }
            else if (posButtons[0, i].GetComponent<ButtonTag>().ButtonTG == posButtons[1, i].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[1, i].GetComponent<ButtonTag>().ButtonTG == posButtons[2, i].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[2, i].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.O) {//Checks Column
                return true;
            }
        }

        if (posButtons[0, 0].GetComponent<ButtonTag>().ButtonTG == posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG == posButtons[2, 2].GetComponent<ButtonTag>().ButtonTG &&
                posButtons[2, 2].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.O) {//Checks Diagonal
            return true;
        }
        else if (posButtons[0, 2].GetComponent<ButtonTag>().ButtonTG == posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG &&
            posButtons[1, 1].GetComponent<ButtonTag>().ButtonTG == posButtons[2, 0].GetComponent<ButtonTag>().ButtonTG &&
            posButtons[2, 0].GetComponent<ButtonTag>().ButtonTG == ButtonTagENUM.O) {//Checks Anti-Diagonal
            return true;
        }

        return false;
    }

    public GameState GetGameState() {
        return gameState;
    }
    public GameObject getGameObjectX() {
        return PlayerXOB;
    }
    public GameObject getGameObjectO() {
        return PlayerOOB;
    }
    public Transform getTextComponentX() {
        return textComponentX;
    }
    public Transform getTextComponentO() {
        return textComponentO;
    }
}
