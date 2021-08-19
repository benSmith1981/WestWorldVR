using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	public enum SpawnState {SPAWNING, WAITING, COUNTING};
	[System.Serializable]
    public class Wave{
    	public string name;
    	public GameObject enemy;
    	public int count;
    	public float rate;

    }
	[System.Serializable]
    public class StartEndPoint{
	    public Transform StartPoint;
	    public Transform EndPoint;

	    // public override string ToString() => $"({X}, {Y})";
	}

    public Wave[] waves;
    public StartEndPoint[] startEndPoints;
    private int nextWave = 0;

    public float timeBetweenWaves = 2f;
    public float waveCountDown;

    public float searchCountDown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    void Start() {
    	waveCountDown = timeBetweenWaves;
    }


    void Update() {

    	if(state == SpawnState.WAITING) {
    		//check if enemies are alive
    		if(!EnemyIsAlive()){ //check any enemies are all dead
    			//begin new round
    			Debug.Log("wave completed");
    			state = SpawnState.COUNTING;
    			WaveCompleted();
    		} else { //else atleast one alive
    			return;
    		}
    	}
    	if (waveCountDown <= 0) {
    		if (state !=  SpawnState.SPAWNING) {
    			Debug.Log("SPAWNING");
				StartCoroutine( SpawnWave ( waves[nextWave]));
    		}
    	}
    	else {
    		Debug.Log("Count down");
    		waveCountDown -= Time.deltaTime;
    	}
    }

    bool EnemyIsAlive() {
    	searchCountDown -= Time.deltaTime;
    	if(searchCountDown <= 0){
    		searchCountDown = 1f;
    		if (GameObject.FindGameObjectWithTag("Enemy") == null){
	    		return false;
	    	} else {
	    		return true;
	    	}
    	}
    	return true;
    }

    IEnumerator SpawnWave(Wave _wave) {
    	Debug.Log("spawning wave" + _wave.name);
    	state = SpawnState.SPAWNING;
    	//spawn
		for(int i = 0 ; i < _wave.count; i++){
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( 1f/_wave.rate );
		}
    	state = SpawnState.WAITING;
    	yield break;
    }

    void WaveCompleted(){
    	waveCountDown = timeBetweenWaves;
    	if(nextWave+1 > waves.Length - 1){
    		nextWave = 0 ;
    		Debug.Log("All waves Complete");
    	}
    	nextWave++;
    }

    void SpawnEnemy(GameObject _enemy) {
    	Debug.Log("spawning enemy" + _enemy.name);
    	int randomPoint = Random.Range(0, startEndPoints.Length-1);
    	GameObject enemyBad = Instantiate(_enemy, startEndPoints[randomPoint].StartPoint.position, Quaternion.identity);
	 	enemyBad.GetComponent<EnemyAiTutorial>().shootFromPosition = startEndPoints[randomPoint].EndPoint;
    }

}
