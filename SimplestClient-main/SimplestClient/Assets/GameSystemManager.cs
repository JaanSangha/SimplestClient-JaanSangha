using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject UsernameInputField, PasswordInputField, UsernameText, PasswordText, SubmitButton, LoginToggle, CreateToggle;
    GameObject NetworkedClient;
    GameObject JoinGameRoomButton;
    GameObject TicTacToeSquareButton;
   // static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
       // instance = this.gameObject;

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "UsernameInputField")
            {
                UsernameInputField = go;
            }
            else if (go.name == "PasswordInputField")
            {
                PasswordInputField = go;
            }
            else if (go.name == "UsernameText")
            {
                UsernameText = go;
            }
            else if (go.name == "PasswordText")
            {
                PasswordText = go;
            }
            else if (go.name == "SubmitButton")
            {
                SubmitButton = go;
            }
            else if (go.name == "LoginToggle")
            {
                LoginToggle = go;
            }
            else if (go.name == "CreateToggle")
            {
                CreateToggle = go;
            }
            else if (go.name == "NetworkedClient")
            {
                NetworkedClient = go;
            }
            else if (go.name == "JoinGameRoomButton")
            {
                JoinGameRoomButton = go;
            }
            else if (go.name == "TicTacToeSquareButton")
            {
                TicTacToeSquareButton = go;
            }
        }
        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
        TicTacToeSquareButton.GetComponent<Button>().onClick.AddListener(TicTacToeSquareButtonPressed);

        ChangeState(gameStates.LoginMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SubmitButtonPressed()
    {
        Debug.Log("AS");
        string p = PasswordInputField.GetComponent<InputField>().text;
        string un = UsernameInputField.GetComponent<InputField>().text;
        string msg;
        if (CreateToggle.GetComponent<Toggle>().isOn)
        {
            msg = ClientToServerSignifier.CreateAccount + "," + un + "," + p;
        }
        else
        {
            msg = ClientToServerSignifier.Login + "," + un + "," + p;
        }
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
    }
    public void LoginToggleChanged(bool changed)
    {
        CreateToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!changed);
    }
    public void CreateToggleChanged(bool changed)
    {
        LoginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!changed);
    }

    public void ChangeState(int newState)
    {
        JoinGameRoomButton.SetActive(false);

        UsernameInputField.SetActive(false);
        PasswordInputField.SetActive(false);
        UsernameText.SetActive(false);
        PasswordText.SetActive(false);
        SubmitButton.SetActive(false);
        LoginToggle.SetActive(false);
        CreateToggle.SetActive(false);
        TicTacToeSquareButton.SetActive(false);

        if (newState == gameStates.LoginMenu)
        {
            UsernameInputField.SetActive(true);
            PasswordInputField.SetActive(true);
            UsernameText.SetActive(true);
            PasswordText.SetActive(true);
            SubmitButton.SetActive(true);
            LoginToggle.SetActive(true);
            CreateToggle.SetActive(true);
        }
        else if (newState == gameStates.MainMenu)
        {
            JoinGameRoomButton.SetActive(true);
        }
        else if (newState == gameStates.WaitingInQueueForOtherPlayer)
        {

        }
        else if (newState == gameStates.TicTacToe)
        {
            //set tictactoe game stuffs
            TicTacToeSquareButton.SetActive(true);
        }
    }

    public void JoinGameRoomButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.JoinQueueForGame + "");
        ChangeState(gameStates.WaitingInQueueForOtherPlayer);
    }

    public void TicTacToeSquareButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.TicTacToeSomethingPlay + "");
        //ChangeState(gameStates.WaitingInQueueForOtherPlayer);
    }
}

static public class gameStates
{
    public const int LoginMenu = 1;

    public const int MainMenu = 2;

    public const int WaitingInQueueForOtherPlayer = 3;

    public const int TicTacToe = 4;
}

