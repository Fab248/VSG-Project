/*
Script name : LevelBlockScript
Author : Fabien Sacriste
Summary : Manages a block of level
*/
using UnityEngine;
using System.Collections;

public class LevelBlockScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Tells if a block is visible frome a specific camera
	// (Usefull to avoid getting a wrong value because of the editor camera)
	public bool isVisibleFrom(Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, this.renderer.bounds);
	}
	
	public void moveBlock(float speed)
	{
		this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + (Time.deltaTime * speed), this.transform.localPosition.z);
	}
	
	public void setBlockPosition(Vector3 position)
	{
		this.transform.localPosition = position;
	}
	
}
