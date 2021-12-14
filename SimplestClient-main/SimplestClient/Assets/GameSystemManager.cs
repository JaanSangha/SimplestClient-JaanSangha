using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject UsernameInputField, PasswordInputField, UsernameText, PasswordText, SubmitButton, LoginToggle, CreateToggle, GameScreen, ObserverText, CustomMessageInput, SendMessageButton;
    GameObject NetworkedClient;
    GameObject JoinGameRoomButton;
    GameObject TicTacToeSquareULButton;
    Button Button0, Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8;
    Text ChatBoxOne, ChatBoxTwo, ChatBoxThree;
    public Sprite X, O;
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
            else if (go.name == "CustomMessageInput")
            {
                CustomMessageInput = go;
            }
            else if (go.name == "SendMessageButton")
            {
                SendMessageButton = go;
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

        Button[] allButtons = UnityEngine.Object.FindObjectsOfType<Button>();
        foreach (Button go in allButtons)
        {
            if (go.name == "Button0")
            {
                Button0 = go;
            }
            else if (go.name == "Button1")
            {
                Button1 = go;
            }
            else if (go.name == "Button2")
            {
                Button2 = go;
            }
            else if (go.name == "Button3")
            {
                Button3 = go;
            }
            else if (go.name == "Button4")
            {
                Button4 = go;
            }
            else if (go.name == "Button5")
            {
                Button5 = go;
            }
            else if (go.name == "Button6")
            {
                Button6 = go;
            }
            else if (go.name == "Button7")
            {
                Button7 = go;
            }
            else if (go.name == "Button8")
            {
                Button8 = go;
            }
        }

            SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
       // TicTacToeSquareULButton.GetComponent<Button>().onClick.AddListener(TicTacToeSquareButtonPressed);
        SendMessageButton.GetComponent<Button>().onClick.AddListener(SendMessageButtonPressed);
        Button0.GetComponent<Button>().onClick.AddListener(SlotZeroButtonPressed);
        Button1.GetComponent<Button>().onClick.AddListener(SlotOneButtonPressed);
        Button2.GetComponent<Button>().onClick.AddListener(SlotTwoButtonPressed);
        Button3.GetComponent<Button>().onClick.AddListener(SlotThreeButtonPressed);
        Button4.GetComponent<Button>().onClick.AddListener(SlotFourButtonPressed);
        Button5.GetComponent<Button>().onClick.AddListener(SlotFiveButtonPressed);
        Button6.GetComponent<Button>().onClick.AddListener(SlotSixButtonPressed);
        Button7.GetComponent<Button>().onClick.AddListener(SlotSevenButtonPressed);
        Button8.GetComponent<Button>().onClick.AddListener(SlotEightButtonPressed);
       

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

    public void SendMessageButtonPressed()
    {
        string txt = CustomMessageInput.GetComponent<InputField>().text;
        string msg;
        msg = ClientToServerSignifier.MessageSent + "," + NetworkedClient.GetComponent<NetworkedClient>().Username + "," + txt;
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
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
        ChatBoxThree.text = ("Player: Good Luck!");
    }

    public void QuickChatTwoObserver()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: Close One!");
    }

    public void QuickChatThreeObserver()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: Oh No!");
    }
    public void QuickChatFourObserver()
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: Good Game!");
    }

    public void MessageRecieved(string txt)
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Opponent: " + txt);
    }

    public void MessageSent(string txt)
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("You: " + txt);
    }

    public void MessageObserver(string txt)
    {
        ChatBoxOne.text = ChatBoxTwo.text;
        ChatBoxTwo.text = ChatBoxThree.text;
        ChatBoxThree.text = ("Player: " + txt);
    }

    public void SlotZeroButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonZero + "");
    }
    public void SlotOneButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonOne + "");
    }
    public void SlotTwoButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonTwo + "");
    }
    public void SlotThreeButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonThree + "");
    }
    public void SlotFourButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonFour + "");
    }
    public void SlotFiveButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonFive + "");
    }
    public void SlotSixButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonSix + "");
    }
    public void SlotSevenButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonSeven + "");
    }
    public void SlotEightButtonPressed()
    {
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButtonEight + "");
    }


    public void SlotZeroButtonX()
    {
        if (Button0.image.sprite == null)
            Button0.image.sprite = X;
        //buttons.Add(Button0);
    }
    public void SlotZeroButtonO()
    {
        if (Button0.image.sprite == null)
            Button0.image.sprite = O;
       // buttons.Add(Button0);
    }
    public void SlotOneButtonX()
    {
        if (Button1.image.sprite == null)
            Button1.image.sprite = X;
       // buttons.Add(Button9);
    }
    public void SlotOneButtonO()
    {
        if (Button1.image.sprite == null)
            Button1.image.sprite = O;
       // buttons.Add(Button9);
    }
    public void SlotTwoButtonX()
    {
        if (Button2.image.sprite == null)
            Button2.image.sprite = X;
       // buttons.Add(Button2);
    }
    public void SlotTwoButtonO()
    {
        if (Button2.image.sprite == null)
            Button2.image.sprite = O;
        //buttons.Add(Button2);
    }

    public void SlotThreeButtonX()
    {
        if (Button3.image.sprite == null)
            Button3.image.sprite = X;
       // buttons.Add(Button3);

    }
    public void SlotThreeButtonO()
    {
        if (Button3.image.sprite == null)
            Button3.image.sprite = O;
        //buttons.Add(Button3);

    }

    public void SlotFourButtonX()
    {
        if (Button4.image.sprite == null)
            Button4.image.sprite = X;
       // buttons.Add(Button4);

    }
    public void SlotFourButtonO()
    {
        if (Button4.image.sprite == null)
            Button4.image.sprite = O;
        //buttons.Add(Button4);

    }

    public void SlotFiveButtonX()
    {
        if (Button5.image.sprite == null)
            Button5.image.sprite = X;
        //buttons.Add(Button5);

    }
    public void SlotFiveButtonO()
    {
        if (Button5.image.sprite == null)
            Button5.image.sprite = O;
        //buttons.Add(Button5);

    }

    public void SlotSixButtonX()
    {
        if (Button6.image.sprite == null)
            Button6.image.sprite = X;
       // buttons.Add(Button6);

    }
    public void SlotSixButtonO()
    {
        if (Button6.image.sprite == null)
            Button6.image.sprite = O;
        //buttons.Add(Button6);

    }

    public void SlotSevenButtonX()
    {
        if (Button7.image.sprite == null)
            Button7.image.sprite = X;
        //buttons.Add(Button7);

    }
    public void SlotSevenButtonO()
    {
        if (Button7.image.sprite == null)
            Button7.image.sprite = O;
        //buttons.Add(Button7);

    }

    public void SlotEightButtonX()
    {
        if (Button8.image.sprite == null)
            Button8.image.sprite = X;
        //buttons.Add(Button8);

    }
    public void SlotEightButtonO()
    {
        if (Button8.image.sprite == null)
            Button8.image.sprite = O;
        //buttons.Add(Button8);

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

