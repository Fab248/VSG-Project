/*
Script name : LevelPattern1Script
Author : Fabien Sacriste
Summary : Pattern composed of 3 blocks of wall on top of each other. The middle one moves from right to left indefinitely.
*/

using UnityEngine;
using System.Collections;

public class LevelPattern1Script : MonoBehaviour {

	private GameObject movingWall;
	private Vector3 startPos = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 endPos = new Vector3(0.0f, 0.0f, 0.0f);
	private float wayToGo = 1.0f;
	private int direction = 1;
	
	public float wallMovingSpeed = 0.0f;
	public float distance = 0.0f;
	
	// Use this for initialization
	void Start () {
		
		// Get the moving wall
		movingWall = this.transform.FindChild("Wall2").gameObject;
		
		startPos = movingWall.transform.localPosition;
		endPos = startPos;
		endPos.x += distance;
		
		// Define the direction of the moving wall
		wayToGo = ((endPos.x - startPos.x) > 0)?1:-1;
		direction = (int)wayToGo;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Move the wall with the speed set toward the direction set
		movingWall.transform.localPosition = new Vector3(movingWall.transform.localPosition.x + (wallMovingSpeed * Time.deltaTime * wayToGo), startPos.y, startPos.z);
		
		// Move to the right
		if(direction == 1)
		{
			// The moving wall has reached its end position
			if(movingWall.transform.localPosition.x >= endPos.x)
			{
				// Set the position of the moving wall to endPos in case of it went a bit farther
				movingWall.transform.localPosition = new Vector3(endPos.x, movingWall.transform.localPosition.y, movingWall.transform.localPosition.z);
				// Change the direction
				wayToGo *= -1;
			}
			else if(movingWall.transform.localPosition.x <= startPos.x)
			{
				// Set the position of the moving wall to startPos in case of it went a bit farther
				movingWall.transform.localPosition = new Vector3(startPos.x, movingWall.transform.localPosition.y, movingWall.transform.localPosition.z);
				// Change the direction
				wayToGo *= -1;
			}
		}
		//Move to the left
		else
		{
			if(movingWall.transform.localPosition.x <= endPos.x)
			{
				// Set the position of the moving wall to endPos in case of it went a bit farther
				movingWall.transform.localPosition = new Vector3(endPos.x, movingWall.transform.localPosition.y, movingWall.transform.localPosition.z);
				// Change the direction
				wayToGo *= -1;
			}
			else if(movingWall.transform.localPosition.x >= startPos.x)
			{
				// Set the position of the moving wall to startPos in case of it went a bit farther
				movingWall.transform.localPosition = new Vector3(startPos.x, movingWall.transform.localPosition.y, movingWall.transform.localPosition.z);
				// Change the direction
				wayToGo *= -1;
			}
		}	
	}
	
	public void setWallMovingSpeed(float speed)
	{
		int directionCurrentSpeed = (wallMovingSpeed > 0)?1:-1;
		int directionNewSpeed = (speed > 0)?1:-1;
		
		wallMovingSpeed = speed;
		
		// If the new speed is in the other direction of the current one, endPos and startPos have to be reset
		if(directionCurrentSpeed != directionNewSpeed)
		{
			distance *= -1;
			
			endPos = startPos;
			endPos.x += distance;
			
			wayToGo = ((endPos.x - startPos.x) > 0)?1:-1;
			direction = (int)wayToGo;
		}
		
	}
	
	public float getWallMovingSpeed()
	{
		return wallMovingSpeed;
	}
}
