/*
Script name : LevelManagerScript
Author : Fabien Sacriste
Summary : Manages the different block of level in the scene
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManagerScript : MonoBehaviour {

	private List<LevelBlockScript> listLevelBlock = new List<LevelBlockScript>();
	private List<Vector3> listLevelBlockSize = new List<Vector3>();
	private int numberLevelBlock = 0;
	private int indexHighestLevelBlock = 0;
	
	public float movingLevelBlocksSpeed = 0.0f;
	
	// Use this for initialization
	void Start () {
	
		// Get the blocks of level in the scene
		LevelBlockScript[] levelBlocks = GameObject.FindObjectsOfType<LevelBlockScript>();
		
		foreach(LevelBlockScript levelBlocksInScene in levelBlocks)
		{
			listLevelBlock.Add(levelBlocksInScene);
			listLevelBlockSize.Add(new Vector3(levelBlocksInScene.transform.localScale.x, levelBlocksInScene.transform.localScale.y, levelBlocksInScene.transform.localScale.z));
		}
		
		numberLevelBlock = listLevelBlock.Count;
		indexHighestLevelBlock = numberLevelBlock - 1;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		bool isObjectVisible = false;
		
		// Move all the blocks of level down
		for(int i = 0; i < numberLevelBlock; ++i)
		{
			isObjectVisible = listLevelBlock[i].isVisibleFrom(Camera.main);
			
			listLevelBlock[i].moveBlock(movingLevelBlocksSpeed);
			
			// If a block of level is not visible anymore, it goes on top of the highest block, and become then the hisghest block
			if(isObjectVisible && !listLevelBlock[i].isVisibleFrom(Camera.main))
			{
				Vector3 newPosition = new Vector3(listLevelBlock[i].transform.localPosition.x, 0.0f, listLevelBlock[i].transform.localPosition.z);
				newPosition.y = listLevelBlock[indexHighestLevelBlock].transform.localPosition.y + listLevelBlockSize[indexHighestLevelBlock].y;
				
				listLevelBlock[i].setBlockPosition(newPosition);
				indexHighestLevelBlock = i;
			}
		}
		
	}
}
