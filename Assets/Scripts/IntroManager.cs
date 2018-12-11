using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class IntroManager : PunBehaviour
{
    public Button createButton;                 // 방 생성 버튼
    public GameObject createRoomPanelPrefab;    // 방 생성 팝업
    public Canvas canvas;

    string serverVer = "0.1";
    CreateRoomPanelManager createRoomPanelManager;

    private void Awake()
    {
        createButton.interactable = false;

        // Photon 초기화
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.automaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();
    }

    // 서버 접속
    void Connect()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(serverVer);
        }
    }

    void CreateRoom(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            return;
        }

        RoomOptions option = new RoomOptions();
        option.IsOpen = true;
        option.IsVisible = true;
        option.MaxPlayers = 10;

        PhotonNetwork.CreateRoom(roomName, option, TypedLobby.Default);
    }

    public void OnClickCreateButton()
    {
        if (createRoomPanelManager == null)
        {
            GameObject createRoomPanelGameObject = Instantiate(createRoomPanelPrefab);
            createRoomPanelGameObject.transform.SetParent(canvas.transform, false);
            createRoomPanelManager =
                createRoomPanelGameObject.GetComponent<CreateRoomPanelManager>();
            createRoomPanelManager.createRoomDelegate = CreateRoom;
        }
    }

    public override void OnJoinedLobby()
    {
        // TODO: Create Room 버튼 활성화
        createButton.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        // TODO: Game Scene으로 전환
        // 방 만들기 위한 팝업 창 닫기

        Debug.Log("## OnJoinedRoom()");
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        // TODO: 방을 선택할 수 있게 UI 갱신
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        // TODO: 유저에게 방 생성 실패 알리기
    }
}
