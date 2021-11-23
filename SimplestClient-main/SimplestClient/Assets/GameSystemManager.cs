using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject UsernameInputField, PasswordInputField, UsernameText, PasswordText, SubmitButton, LoginToggle, CreateToggle, GameScreen, ObserverText;
    GameObject NetworkedClient;
    GameObject JoinGameRoomButton;
    GameObject TicTacToeSquareULButton;
    Text ChatBoxOne, ChatBoxTwo, ChatBoxThree;
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
            else if (go.name == "TicTacToeSquareULButton")
            {
                TicTacToeSquareULButton = go;
            }
            else if (go.name == "GameScreen")
            {
                GameScreen = go;
            }
            else if (go.name == "ObserverText")
            {
                ObserverText = go;
            }
        }
        Text[] allTexts = UnityEngine.Object.FindObjectsOfType<Text>();
        foreach (Text go in allTexts)
        {
            if (go.name == "ChatTextOne")
            {
                ChatBoxOne = go;
            }
            else if (go.name == "ChatTextTwo")
            {
                ChatBoxTwo = go;
            }
            else if (go.name == "ChatTextThree")
            {
                ChatBoxThree = go;
            }
        }

        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
        TicTacToeSquareULButton.GetComponent<Button>().onClick.AddListener(TicTacToeSquareButtonPressed);

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
        GameScreen.SetActive(false);
        ObserverText.SetActive(false);

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
            TicTacToeSquareULButton.SetActive(true);
            GameScreen.SetActive(true);
        }
        else if (newState == gameStates.GameObserver)
        {
            GameScreen.SetActive(true);
            ObserverText.SetActive(true);
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

    public void QuickChatOneRecieved()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: Good Luck!");
    }
    public void QuickChatTwoRecieved()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: Close One!");
    }
    public void QuickChatThreeRecieved()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: Oh No!");
    }
    public void QuickChatFourRecieved()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: Good Game!");
    }
    public void QuickChatOneSent()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: Good Luck!");
    }
    public void QuickChatTwoSent()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: Close One!");
    }
    public void QuickChatThreeSent()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: Oh No!");
    }
    public void QuickChatFourSent()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: Good Game!");
    }

    public void QuickChatOneObserver()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: Good Game!");
    }

    public void QuickChatTwoObserver()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: Good Game!");
    }

    public void QuickChatThreeObserver()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: Good Game!");
    }
    public void QuickChatFourObserver()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: Good Game!");
    }
}

static public class gameStates
{
    public const int LoginMenu = 1;

    public const int MainMenu = 2;

    public const int WaitingInQueueForOtherPlayer = 3;

    public const int TicTacToe = 4;

    public const int GameObserver = 5;
}

