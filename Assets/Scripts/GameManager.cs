using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class GameManager : PunBehaviour
{
    public GameObject playerPrefab;

	void Start ()
    {
        if (playerPrefab)
        {
            if (Player.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate(this.playerPrefab.name,
                    new Vector3(0, 0, 0), Quaternion.identity, 0);
            }
        }
		
	}
	
}
