using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : PunBehaviour {

    public static GameObject LocalPlayerInstance;
    public CameraWork cameraWork;
    
    Animator animator;

    private void Awake()
    {
        if (photonView.isMine)
        {
            Player.LocalPlayerInstance = this.gameObject;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();

        if (cameraWork != null)
        {
            if (photonView.isMine)
            {
                cameraWork.OnStartFollowing();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * 150.0f, 0);
        transform.Translate(0, 0, z * Time.deltaTime * 3.0f);

        animator.SetFloat("Speed", z);
	}
}
