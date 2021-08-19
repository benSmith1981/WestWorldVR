using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePlayState {PLAYING, PAUSED, BETWEENSCENES};

public class GamePlayHandler : MonoBehaviour
{
    public GamePlayState currentState = GamePlayState.PAUSED;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    	if(currentState == GamePlayState.PLAYING) {
            gameObject.GetComponent<WaveSpawner>().enabled = true;
        } else if(currentState == GamePlayState.PAUSED) {
            gameObject.GetComponent<WaveSpawner>().enabled = false;
        }
    }

}
