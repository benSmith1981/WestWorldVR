using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
	public Transform spawnPosition;
	public Transform shootPosition;
	public GameObject character;
	public bool isDead = false;

    public Enemy(GameObject character, Transform spawnPosition, Transform shootPosition){

    	this.character = character;
    	this.spawnPosition = spawnPosition;
    	this.shootPosition = shootPosition;
    }
}
