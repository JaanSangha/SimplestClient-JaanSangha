using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystemManager : MonoBehaviour
{
    GameObject UsernameInputField, PasswordInputField, UsernameText, PasswordText, SubmitButton, LoginToggle, CreateToggle;
    GameObject NetworkedClient;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        SubmitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        LoginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        CreateToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);

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
}
