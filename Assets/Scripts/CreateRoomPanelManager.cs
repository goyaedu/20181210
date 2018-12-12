using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomPanelManager : MonoBehaviour
{
    public InputField roomNameInputField;
    public Button okButton;
    public Button cancelButton;
    public Text errorText;

    public delegate void CreateRoomDelegate(string roomName);
    public CreateRoomDelegate createRoomDelegate;

	void Start ()
    {
        errorText.text = "";
        okButton.interactable = false;
        roomNameInputField.onValueChanged.
            AddListener(delegate { CheckRoomName(); });
	}

    void CheckRoomName() {
        if (!string.IsNullOrEmpty(roomNameInputField.text))
        {
            okButton.interactable = true;
        }
        else
        {
            okButton.interactable = false;
        }
    }

    public void OnClickOKButton()
    {
        errorText.text = "";

        if (createRoomDelegate == null)
        {
            return;
        }

        okButton.interactable = false;
        cancelButton.interactable = false;
        roomNameInputField.interactable = false;

        createRoomDelegate(roomNameInputField.text);
    }

    public void OnClickCancelButton()
    {
        Destroy(this.gameObject);
    }

    public void CreateRoomFailed()
    {
        errorText.text = "방 생성 실패.";
        cancelButton.interactable = true;
        roomNameInputField.interactable = true;
        roomNameInputField.text = "";
    }

    public void CreateRoomSuccess()
    {
        Destroy(this.gameObject);
    }
}
