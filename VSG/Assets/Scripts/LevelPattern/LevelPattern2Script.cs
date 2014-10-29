/*
Script name : LevelPattern2Script
Author : Fabien Sacriste
Summary : Pattern composed of 6 blocks of wall. 2 Columns of 3 walls on top of each other.
	The walls from a column move as a wave, and the ohter walls from the other column move symmetrically
*/


using UnityEngine;
using System.Collections;

public class LevelPattern2Script : MonoBehaviour {

	private GameObject[] arrayOfWalls = new GameObject[6];
	private Vector3 startPosLeftSide = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 endPosLeftSide = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 startPosRightSide = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 endPosRightSide = new Vector3(0.0f, 0.0f, 0.0f);
	
	private float wayToGoTop = 1.0f;
	private float wayToGoMiddle = 1.0f;
	private float wayToGoBottom = 1.0f;
	
	public float wallMovingSpeed = 0.0f;
	private float distance = 4.0f;
	
	// Use this for initialization
	void Start () {
		
		// Get all the walls
		arrayOfWalls[0] = this.transform.GetChild (0).gameObject;
		arrayOfWalls[1] = this.transform.GetChild (1).gameObject;
		arrayOfWalls[2] = this.transform.GetChild (2).gameObject;
		arrayOfWalls[3] = this.transform.GetChild (3).gameObject;
		arrayOfWalls[4] = this.transform.GetChild (4).gameObject;
		arrayOfWalls[5] = this.transform.GetChild (5).gameObject;
		
		// Define the start and end postions
		startPosLeftSide = arrayOfWalls[0].transform.localPosition;
		endPosLeftSide = startPosLeftSide;
		endPosLeftSide.x += distance;
		
		startPosRightSide = arrayOfWalls[3].transform.localPosition;
		endPosRightSide = startPosRightSide;
		endPosRightSide.x -= distance;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		/*********************
			Bottom walls
		*********************/
		// Move the wall with the speed set toward the direction set
		arrayOfWalls[0].transform.localPosition = new Vector3(arrayOfWalls[0].transform.localPosition.x + (wallMovingSpeed * Time.deltaTime * wayToGoBottom), arrayOfWalls[0].transform.localPosition.y, arrayOfWalls[0].transform.localPosition.z);
		arrayOfWalls[3].transform.localPosition = new Vector3(arrayOfWalls[3].transform.localPosition.x + (wallMovingSpeed * Time.deltaTime * wayToGoBottom * -1), arrayOfWalls[3].transform.localPosition.y, arrayOfWalls[3].transform.localPosition.z);
		
		// The moving walls have reached their end position
		if(arrayOfWalls[0].transform.localPosition.x >= endPosLeftSide.x)
		{
			// Set the position of the moving walls to endPos in case of they went a bit farther
			arrayOfWalls[0].transform.localPosition = new Vector3(endPosLeftSide.x, arrayOfWalls[0].transform.localPosition.y, arrayOfWalls[0].transform.localPosition.z);
			arrayOfWalls[3].transform.localPosition = new Vector3(endPosRightSide.x, arrayOfWalls[3].transform.localPosition.y, arrayOfWalls[3].transform.localPosition.z);
			// Change the direction
			wayToGoBottom *= -1;
		}
		// The moving walls have reached their start position
		else if(arrayOfWalls[0].transform.localPosition.x <= startPosLeftSide.x)
		{
			// Set the position of the moving walls to startPos in case of they went a bit farther
			arrayOfWalls[0].transform.localPosition = new Vector3(startPosLeftSide.x, arrayOfWalls[0].transform.localPosition.y, arrayOfWalls[0].transform.localPosition.z);
			arrayOfWalls[3].transform.localPosition = new Vector3(startPosRightSide.x, arrayOfWalls[3].transform.localPosition.y, arrayOfWalls[3].transform.localPosition.z);
			// Change the direction
			wayToGoBottom *= -1;
		}
		
		/*********************
			Middle walls
		*********************/
		arrayOfWalls[1].transform.localPosition = new Vector3(arrayOfWalls[1].transform.localPosition.x + (wallMovingSpeed * Time.deltaTime * wayToGoMiddle), arrayOfWalls[1].transform.localPosition.y, arrayOfWalls[1].transform.localPosition.z);
		arrayOfWalls[4].transform.localPosition = new Vector3(arrayOfWalls[4].transform.localPosition.x + (wallMovingSpeed * Time.deltaTime * wayToGoMiddle * -1), arrayOfWalls[4].transform.localPosition.y, arrayOfWalls[4].transform.localPosition.z);
		
		// The moving walls have reached their end position
		if(arrayOfWalls[1].transform.localPosition.x >= endPosLeftSide.x)
		{
			// Set the position of the moving walls to endPos in case of they went a bit farther
			arrayOfWalls[1].transform.localPosition = new Vector3(endPosLeftSide.x, arrayOfWalls[1].transform.localPosition.y, arrayOfWalls[1].transform.localPosition.z);
			arrayOfWalls[4].transform.localPosition = new Vector3(endPosRightSide.x, arrayOfWalls[4].transform.localPosition.y, arrayOfWalls[4].transform.localPosition.z);
			// Change the direction
			wayToGoMiddle *= -1;
		}
		// The moving walls have reached their start position
		else if(arrayOfWalls[1].transform.localPosition.x <= startPosLeftSide.x)
		{
			// Set the position of the moving walls to startPos in case of they went a bit farther
			arrayOfWalls[1].transform.localPosition = new Vector3(startPosLeftSide.x, arrayOfWalls[1].transform.localPosition.y, arrayOfWalls[1].transform.localPosition.z);
			arrayOfWalls[4].transform.localPosition = new Vector3(startPosRightSide.x, arrayOfWalls[4].transform.localPosition.y, arrayOfWalls[4].transform.localPosition.z);
			// Change the direction
			wayToGoMiddle *= -1;
		}
		
		
		/*********************
			Top walls
		*********************/
		arrayOfWalls[2].transform.localPosition = new Vector3(arrayOfWalls[2].transform.localPosition.x + (wallMovingSpeed * Time.deltaTime * wayToGoTop), arrayOfWalls[2].transform.localPosition.y, arrayOfWalls[2].transform.localPosition.z);
		arrayOfWalls[5].transform.localPosition = new Vector3(arrayOfWalls[5].transform.localPosition.x + (wallMovingSpeed * Time.deltaTime * wayToGoTop * -1), arrayOfWalls[5].transform.localPosition.y, arrayOfWalls[5].transform.localPosition.z);
		
		// The moving walls have reached their end position
		if(arrayOfWalls[2].transform.localPosition.x >= endPosLeftSide.x)
		{
			// Set the position of the moving walls to endPos in case of they went a bit farther
			arrayOfWalls[2].transform.localPosition = new Vector3(endPosLeftSide.x, arrayOfWalls[2].transform.localPosition.y, arrayOfWalls[2].transform.localPosition.z);
			arrayOfWalls[5].transform.localPosition = new Vector3(endPosRightSide.x, arrayOfWalls[5].transform.localPosition.y, arrayOfWalls[5].transform.localPosition.z);
			// Change the direction
			wayToGoTop *= -1;
		}
		// The moving walls have reached their start position
		else if(arrayOfWalls[2].transform.localPosition.x <= startPosLeftSide.x)
		{
			// Set the position of the moving walls to startPos in case of they went a bit farther
			arrayOfWalls[2].transform.localPosition = new Vector3(startPosLeftSide.x, arrayOfWalls[2].transform.localPosition.y, arrayOfWalls[2].transform.localPosition.z);
			arrayOfWalls[5].transform.localPosition = new Vector3(startPosRightSide.x, arrayOfWalls[5].transform.localPosition.y, arrayOfWalls[5].transform.localPosition.z);
			// Change the direction
			wayToGoTop *= -1;
		}
		
	}
}
