using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        Connect();
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Play()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a room and failed.");
        // Most likely no room
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions rm = new RoomOptions();
        rm.IsVisible = true;
        rm.MaxPlayers = 4;

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Succesfully joined a room");
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke(nameof(InvokeLoad), 10f);
        }
    }

    void InvokeLoad()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
