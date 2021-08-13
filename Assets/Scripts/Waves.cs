using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
	public List<Enemy> enemiesInWave = new List<Enemy>();
	public bool _waveCompleted = false;

    public int numberOfBadGuysInWave = 0;
	public List<Enemy> enemyTypes;
    public Wave(List<Enemy> enemyTypes, 
    	int numberOfBadGuysInWave) {

    	for(int i = 0; i < numberOfBadGuysInWave; i++) {
    		int randomEnemy = Random.Range(0, enemyTypes.Count-1);
    		enemiesInWave.Add(enemyTypes[randomEnemy]);
    	}
    }

    public bool waveCompleted {
    	get {
    		return _waveCompleted;
    	}

    	set {
    		_waveCompleted = value;
    	}
    }
}
