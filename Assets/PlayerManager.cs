using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public Player player;
    public static PlayerManager Instance { get { return _instance; } }
    [Serializable]
    public class Player
    {
        private int lives = 5;

        public int getLives()
        {
            return lives;
        }

        public void subtractLife()
        {
            lives--;
        }
    };
    
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            player = new Player();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
