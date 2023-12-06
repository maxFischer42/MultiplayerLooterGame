using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockoutSettings : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    public List<Behaviour> lockBehaviors = new List<Behaviour>();
    public List<GameObject> lockObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        if(!view.IsMine)
        {
            foreach(Behaviour behaviour in lockBehaviors)
            {
                behaviour.enabled = false;
            }
            foreach(GameObject gameObject in lockObjects) { 
                gameObject.SetActive(false);
            }
        }
    }

}
