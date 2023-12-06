using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;


public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPos;

    private void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos.transform.position, spawnPos.rotation);

    }
}
