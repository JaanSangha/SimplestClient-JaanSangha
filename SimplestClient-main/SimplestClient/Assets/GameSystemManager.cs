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
    public Button ReplayButton;
    public List<int> temp;
    public List<Button> tempButtons;
    List<int> moveOrder;
    Text ChatBoxOne, ChatBoxTwo, ChatBoxThree;
    public Sprite X, O;
    public Button[] allButtons;
    public int[] playerIndex;
   // public int[] temp = new int[9];
    bool replayed = false;
    float count = 0f;
    float max = 30f;
    int index = 0;
    int listIndex = 8;
    // static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        // instance = this.gameObject;
        temp = new List<int>();
        moveOrder = new List<int>();
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

        //allButtons = UnityEngine.Object.FindObjectsOfType<Button>();
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

        for (int i = 0; i <= 9; i++)
        {
            temp.Add(0);
            moveOrder.Add(0);
        }
        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);
        JoinGameRoomButton.GetComponent<Button>().onClick.AddListener(JoinGameRoomButtonPressed);
        SendMessageButton.GetComponent<Button>().onClick.AddListener(SendMessageButtonPressed);
        ReplayButton.GetComponent<Button>().onClick.AddListener(ReplayButtonPressed);

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
    void FixedUpdate()
    {
        if (replayed)
        {
            count++;
            if (count == max)
            {
                foreach (var x in moveOrder)
                {
                    Debug.Log(x.ToString());
                }

                if (temp[index] == 1)
                {
                    allButtons[index].image.sprite = X;

                }
                else if (temp[index] == 2)
                {
                    allButtons[index].image.sprite = O;

                }
                index++;
                count = 0;
                if (index >= 9)
                {
                    replayed = false;
                    index = 0;
                }
            }
        }
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

    public void PlayReplay(int playerID, int slot, int arrayNum)
    {
        moveOrder.Add(slot);
        for (int i = 0; i < 9; i++)
        {
            allButtons[i].image.sprite = null;
        }
        temp[slot] = playerID;

        replayed = true;
        // Debug.Log(arrayNum);
        //playerIndex.add
        //buttonPicked.Add(slot);
        //playerPicked.Add(playerID);
    }
    public void ReplayButtonPressed()
    {
        
        //for (int i = 0; i < 9; i++)
        //{
        //    buttonList.Add(-1);
        //}
        //for (int i = 0; i < 9; i++)
        //{

        //    if (allButtons[i].image.sprite == O)
        //    {
        //        buttonList[i] = 0;
        //    }
        //    else if (allButtons[i].image.sprite == X)
        //    {
        //        buttonList[i] = 1;
        //    }
        //}
        //foreach (Button button in allButtons)
        //{
        //    button.image.sprite = null;
        //}
        //replayed = true;
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.ReplayButton + "");
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
            //TicTacToeSquareULButton.SetActive(true);
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
        tempButtons.Add(Button0);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 0 + ",");
    }
    public void SlotOneButtonPressed()
    {
        tempButtons.Add(Button1);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 1 + ",");
    }
    public void SlotTwoButtonPressed()
    {
        tempButtons.Add(Button2);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 2 + ",");
    }
    public void SlotThreeButtonPressed()
    {
        tempButtons.Add(Button3);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 3 + ",");
    }
    public void SlotFourButtonPressed()
    {
        tempButtons.Add(Button4);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 4 + ",");
    }
    public void SlotFiveButtonPressed()
    {
        tempButtons.Add(Button5);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 5 + ",");
    }
    public void SlotSixButtonPressed()
    {
        tempButtons.Add(Button6);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 6 + ",");
    }
    public void SlotSevenButtonPressed()
    {
        tempButtons.Add(Button7);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 7 + ",");
    }
    public void SlotEightButtonPressed()
    {
        tempButtons.Add(Button8);
        NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifier.SendButton + "," + 8 + ",");
    }

    public void SlotButtonPressed(int player, int index)
    {
        if (allButtons[index].image.sprite == null)
        {
            if (player == 1)
            {
                allButtons[index].image.sprite = X;
            }
            else if (player == 2)
            {
                allButtons[index].image.sprite = O;
            }
        }
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

