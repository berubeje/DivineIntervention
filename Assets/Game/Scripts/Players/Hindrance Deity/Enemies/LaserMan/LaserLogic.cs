﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{

    public float growSpeed = 5.0f;
    public float lifeTime = 3.0f;
    public float damage = 5.0f;
    public float destroySpeed = 2.0f;

    private bool blockCollide;

    private BoxCollider currentBox;


    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponentInParent<PhotonView>();
    }


    void Update()
    {
        GrowLaser();
        ReduceLifeTime();
    }

    private void GrowLaser()
    {
        if (blockCollide == false)
        {
            transform.parent.localScale += new Vector3(0.0f, growSpeed * Time.deltaTime, 0.0f);
        }
    }

    public void SetCurrentBlock(BoxCollider box)
    {
        currentBox = box;
    }

    private void ReduceLifeTime()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0.0f)
        {
            DestroyLaser();
        }
    }

    private void DestroyLaser()
    {
        transform.parent.localScale = new Vector3(transform.parent.localScale.x - (destroySpeed * Time.deltaTime), transform.parent.localScale.y, transform.parent.localScale.z - (destroySpeed * Time.deltaTime));


        if (NetworkManager.Instance.IsViewMine(photonView) == false)
        {
            return;
        }

        if (transform.parent.localScale.x <= 0.0f)
        {
            NetworkManager.Instance.DestroyGameObject(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block" && other != currentBox)
        {
            blockCollide = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (NetworkManager.Instance.IsViewMine(photonView))
        {
            WingedSpiritController spiritCon = other.gameObject.GetComponent<WingedSpiritController>();

            if (spiritCon != null)
            {
                spiritCon.SpiritTakeDamageCall(damage);
            }
        }
    }
}
