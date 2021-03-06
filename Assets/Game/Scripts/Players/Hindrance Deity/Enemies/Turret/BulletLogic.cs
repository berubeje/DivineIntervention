﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletLogic : MonoBehaviour
{
    public float speed = 5.0f;
    public float damage = 5.0f;

    private Rigidbody rigid;
    private Vector3 startPos;

    private PhotonView photonView;


    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

    }


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.MovePosition(transform.position + (transform.up * speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        WingedSpiritController spiritController = other.gameObject.GetComponent<WingedSpiritController>();

        if (spiritController != null)
        {
            spiritController.SpiritTakeDamageCall(damage);
        }

        if(!other.GetComponent<BlockChanger>() || Vector3.Distance(transform.position, startPos) > 40)
        {
            if(NetworkManager.Instance.IsViewMine(photonView))
            {
                NetworkManager.Instance.DestroyGameObject(this.gameObject);
            }
        }
        
    }
}
