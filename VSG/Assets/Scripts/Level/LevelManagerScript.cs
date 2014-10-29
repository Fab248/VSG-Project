/*
Script name : LevelManagerScript
Author : Fabien Sacriste
Summary : Manages the different block of level in the scene
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManagerScript : MonoBehaviour {

	private GameObject[] arrayLevelBlock = new GameObject[2];
	private Vector3[] arrayLevelBlockSize = new Vector3[2];
	private GameObject[,] arrayPatternsLevelBlock = new GameObject[2,6];
	private int indexHighestLevelBlock = 0;
	private bool playerIsDead = false;
	
	// Use this for initialization
	void Start () {
		
		arrayLevelBlock[0] = this.transform.FindChild("LevelBlockPrefab1").gameObject;
		arrayLevelBlock[1] = this.transform.FindChild("LevelBlockPrefab2").gameObject;
		
		arrayLevelBlockSize[0] = arrayLevelBlock[0].transform.FindChild("Background").transform.localScale;
		arrayLevelBlockSize[1] = arrayLevelBlock[1].transform.FindChild("Background").transform.localScale;
		
		indexHighestLevelBlock = 1;
		
		setPatternsInLevelBlock(0);
		setPatternsInLevelBlock(1);
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
	}
	
	private void setPatternsInLevelBlock(int indexLevelBlock)
	{
		int typeOfPattern = 0;
		int numOfPattern = 0;
		
		for(int i = 0; i < 6; ++i)
		{
			typeOfPattern = Random.Range(0,2);
			numOfPattern = Random.Range(1,5);
			
			if(typeOfPattern == 0)
			{
				if(numOfPattern % 2 == 0 && ((i+1) % 2 == 1))
				{ 
					arrayPatternsLevelBlock[indexLevelBlock,i] = (GameObject)Instantiate(Resources.Load("Prefabs/LevelPattern/LevelPattern" + numOfPattern + "Prefab"));
					arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent = arrayLevelBlock[indexLevelBlock].transform;
					
					Vector3 position = arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent.transform.FindChild("Slot"+(i+2)).transform.localPosition - 
						arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent.transform.FindChild("Slot"+(i+1)).transform.localPosition;
					
					position /=2;
					position += arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent.transform.FindChild("Slot"+(i+1)).transform.localPosition;
					
					arrayPatternsLevelBlock[indexLevelBlock,i].transform.localPosition = new Vector3(position.x,
					                                                                                 arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent.transform.FindChild("Slot"+(i+1)).transform.localPosition.y,
					                                                                                 arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent.transform.FindChild("Slot"+(i+1)).transform.localPosition.z);
					++i;
				}
				else if(numOfPattern % 2 == 1)
				{
					arrayPatternsLevelBlock[indexLevelBlock,i] = (GameObject)Instantiate(Resources.Load("Prefabs/LevelPattern/LevelPattern" + numOfPattern + "Prefab"));
					arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent = arrayLevelBlock[indexLevelBlock].transform;
					arrayPatternsLevelBlock[indexLevelBlock,i].transform.localPosition = arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent.transform.FindChild("Slot"+(i+1)).transform.localPosition;
					
					if(numOfPattern == 1 && (i+1)%2 == 0)
					{
						LevelPattern1Script level1 = arrayPatternsLevelBlock[indexLevelBlock,i].transform.GetComponent<LevelPattern1Script>();
						float speed = level1.getWallMovingSpeed();
						level1.setWallMovingSpeed(speed * -1);
					}
					else if(numOfPattern == 3 && (i+1)%2 == 0)
					{
						LevelPattern3Script level3 = arrayPatternsLevelBlock[indexLevelBlock,i].transform.GetComponent<LevelPattern3Script>();
						level3.setWayToShoot(-1);
					}
				}
				else{
					--i;
				}
			}
			else
			{
				arrayPatternsLevelBlock[indexLevelBlock,i] = (GameObject)Instantiate(Resources.Load("Prefabs/EnemyPattern/EnemyPattern" + numOfPattern + "Prefab"));
			arrayPatternsLevelBlock[indexLevelBlock,i].transform.parent = arrayLevelBlock[indexLevelBlock].transform;
			arrayPatternsLevelBlock[indexLevelBlock,i].transform.position = arrayLevelBlock[indexLevelBlock].transform.FindChild("Slot"+(i+1)).transform.position;

			}
		}
	}
	
	// Gather all the enemies present in the patterns and send them in a list
	public List<GameObject> getListOfEnemies()
	{
		List<GameObject> tempListEnemies = new List<GameObject>();
		List<GameObject> finalListEnemies = new List<GameObject>();
		
		// Go through the both array of pattern 
		for(int i = 0; i < 2; ++i)
		{
			for(int j = 0; j < 6; ++j)
			{
				// Check if the current pattern is not null
				if(arrayPatternsLevelBlock[i,j] != null)
				{
					// Check if the current pattern is one with enemies in it.
					if(arrayPatternsLevelBlock[i,j].GetComponent<EnemyPatternScript>() != null)
					{
						tempListEnemies = arrayPatternsLevelBlock[i,j].GetComponent<EnemyPatternScript>().getListOfEnemies();
						
						for(int k =0; k < tempListEnemies.Count; ++k)
							finalListEnemies.Add (tempListEnemies[k]);
					}
				}
			}
		}
		
		return finalListEnemies;
		
	}
	
	// Move the blocks of level
	public void moveLevels(float speed)
	{
		bool isObjectVisible = false;
		GameObject background;
		
		// Move all the blocks of level down
		for(int i = 0; i < 2; ++i)
		{
			background = arrayLevelBlock[i].transform.FindChild("Background").gameObject;
			isObjectVisible = arrayLevelBlock[i].GetComponent<LevelBlockScript>().isVisibleFrom(background.renderer, Camera.main);
			
			arrayLevelBlock[i].GetComponent<LevelBlockScript>().moveBlock(speed);
			
			// If a block of level is not visible anymore, it goes on top of the highest block, and become then the hisghest block
			if(isObjectVisible && !arrayLevelBlock[i].GetComponent<LevelBlockScript>().isVisibleFrom(arrayLevelBlock[i].transform.FindChild("Background").renderer, Camera.main))
			{
				Vector3 newPosition = new Vector3(arrayLevelBlock[i].transform.localPosition.x, 0.0f, arrayLevelBlock[i].transform.localPosition.z);
				newPosition.y = arrayLevelBlock[indexHighestLevelBlock].transform.localPosition.y + arrayLevelBlockSize[indexHighestLevelBlock].y;
				
				arrayLevelBlock[i].GetComponent<LevelBlockScript>().setBlockPosition(newPosition);
				indexHighestLevelBlock = i;
			}
		}
	}
	
	//Set if the player is still alive
	public void setIsPlayerDead(bool dead)
	{
		playerIsDead = true;
		
		// Go through the both array of pattern 
		for(int i = 0; i < 2; ++i)
		{
			for(int j = 0; j < 6; ++j)
			{
				// Check if the current pattern is not null
				if(arrayPatternsLevelBlock[i,j] != null)
				{
					// Check if the current pattern is one with enemies in it.
					if(arrayPatternsLevelBlock[i,j].GetComponent<EnemyPatternScript>() != null)
					{
						// Tell the enemies the player is dead
						arrayPatternsLevelBlock[i,j].GetComponent<EnemyPatternScript>().setPlayerIsDead(playerIsDead);

					}
				}
			}
		}
	}
	
}
