﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public float scrollSpeed;
    public GameObject wingedSpirit;
    //public Text healthTxt;

    // Start is called before the first frame update
    void Start()
    {
        SceneLoader.Instance.SetActiveScene(gameObject.scene);
        //Instantiate(wingedSpirit, new Vector3(0f, 10f, 0f), Quaternion.Euler(Vector3.zero));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(float health)
    {
        //healthTxt.text = health.ToString();
    }

    public void StartGame()
    {
       if(NetworkManager.Instance.IsMasterClient())
       {
            NetworkManager.Instance.InstantiateGameObject("WingedSpirit", new Vector3(0, 10, 0), Quaternion.identity);
       }
    }

    public void EndGame()
    {
        
    }
}