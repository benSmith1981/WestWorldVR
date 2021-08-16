using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
	public List<Enemy> enemies;
    public int numberOfWaves;

	public List<Wave> waves = new List<Wave>();
	public int currentWave = 0;
	private bool _levelCompleted = false;

    // Start is called before the first frame update
    public Level(int numberOfWaves, 
		    	List<Enemy> enemies)
    {
    	this.numberOfWaves = numberOfWaves;
    	this.enemies = enemies;
    	
    }

    public bool levelCompleted {
    	get {
    		return _levelCompleted;
    	}

    	set {
    		_levelCompleted = value;
    	}
    }

    public void createWaves(){
    	for(int i = 0; i < numberOfWaves; i++) {
    		waves.Add(new Wave(enemies, i+1));
    	}
    	// if(currentWave == 1){
    	// 	badGuys[0] = Instantiate(badGuys[0], spawnPoint1.position, Quaternion.identity);
     //    	badGuys[0].GetComponent<EnemyAiTutorial>().shootFromPosition = shootPoints[0];
    	// } else if(currentWave == 2){
    	// 	badGuys[0] = Instantiate(badGuys[0], spawnPoints[1].position, Quaternion.identity);
     //    	badGuys[0].GetComponent<EnemyAiTutorial>().shootFromPosition = shootPoints[1];
    	// }
    }

}
