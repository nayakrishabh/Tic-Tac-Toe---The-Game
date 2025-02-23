using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Tile_Connector tileConnetor;

    [SerializeField]private Sprite XSprite;
    [SerializeField]private Sprite OSprite;
    [SerializeField]private GameObject winMC;

    private List<Button> buttonList;
    
    private Button[,] posButtons = new Button[3,3];

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start()
    {
        buttonList = tileConnetor.getTilelist();
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                int index = i * 3 + j;
                posButtons[i, j] = buttonList[index];
            }
        }
        addListeners();
    }

    private void addListeners() {
        for (int i = 0; i < posButtons.GetLength(0); i++) {
            for (int j = 0; j < posButtons.GetLength(1); j++) {
                int x = i;
                int y = j;
                posButtons[i,j].onClick.AddListener(() => tagButtons(x, y));
            }
        }
        winMC.GetComponent<WinMConnector>().playAgainButton.onClick.AddListener(() => playAgain());
    }

    private void playAgain() {
        for (int i = 0; i < posButtons.GetLength(0); i++) {
            for (int j = 0; j < posButtons.GetLength(1); j++) {
                posButtons[i, j].GetComponent<Image>().sprite = null;
                posButtons[i, j].GetComponent<ButtonTag>().resetTag();
                posButtons[i, j].interactable = true;
            }
        }
        TurnBasedSystem.Instance.gameState = GameState.PLAYERXTURN;
        TurnBasedSystem.Instance.getGameObjectO().GetComponent<Image>().color = Color.white;
        TurnBasedSystem.Instance.getGameObjectX().GetComponent<Image>().color = Color.green;
        TurnBasedSystem.Instance.getTextComponentO().gameObject.SetActive(false);
        TurnBasedSystem.Instance.getTextComponentX().gameObject.SetActive(true);
        winMC.SetActive(false);
    }

    private void tagButtons(int x, int y) {
        GameObject clickedButton = posButtons[x, y].gameObject;
        buttonUpdate(clickedButton);
        TurnBasedSystem.Instance.winCheck(posButtons);
    }

    private void buttonUpdate(GameObject clickedButton) {
        if (TurnBasedSystem.Instance.GetGameState() == GameState.PLAYERXTURN) {
            clickedButton.GetComponent<Image>().sprite = XSprite;
            clickedButton.GetComponent<ButtonTag>().SetTag(UnitType.X);
            clickedButton.GetComponent<Button>().interactable = false;
            SetupForAfterXTurn();
        }
        else if (TurnBasedSystem.Instance.GetGameState() == GameState.PLAYEROTURN) {
            clickedButton.GetComponent<Image>().sprite = OSprite;
            clickedButton.GetComponent<ButtonTag>().SetTag(UnitType.O);
            clickedButton.GetComponent<Button>().interactable = false;
            SetupForAfterOTurn();
        }
    }

    private void SetupForAfterXTurn() {
        TurnBasedSystem.Instance.gameState = GameState.PLAYEROTURN;
        TurnBasedSystem.Instance.getGameObjectX().GetComponent<Image>().color = Color.white;
        TurnBasedSystem.Instance.getGameObjectO().GetComponent<Image>().color = Color.green;
        TurnBasedSystem.Instance.getTextComponentX().gameObject.SetActive(false);
        TurnBasedSystem.Instance.getTextComponentO().gameObject.SetActive(true);
    }
    private void SetupForAfterOTurn() {
        TurnBasedSystem.Instance.gameState = GameState.PLAYERXTURN;
        TurnBasedSystem.Instance.getGameObjectO().GetComponent<Image>().color = Color.white;
        TurnBasedSystem.Instance.getGameObjectX().GetComponent<Image>().color = Color.green;
        TurnBasedSystem.Instance.getTextComponentX().gameObject.SetActive(true);
        TurnBasedSystem.Instance.getTextComponentO().gameObject.SetActive(false);
    }

    public GameObject getWinMC() {
        return winMC;
    }
    
}
