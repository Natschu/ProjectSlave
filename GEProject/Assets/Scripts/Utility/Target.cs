using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Modified RaycastHit with most important information directly accesible.
/// </summary>
public class Target {

	public readonly static float distanceFail = -1f;

	//readonly keyword?
	//difference between Constructor and direct things (bla = bla2)
	private bool isHit;
	private string tag;
	private float distance;
	private GameObject target;

	public bool Hit
	{
		get { return isHit; }
	}
	public string Tag
	{
		get { return tag; }
	}
	public float Distance
	{
		get { return distance; }
	}
	public GameObject Object
	{
		get { return target; }
	}

	public Target()
	{
		isHit = false;
		tag = "";
		distance = distanceFail;
		target = null;
	}

	public void Set(RaycastHit hit)
	{
		isHit = true;
		tag = hit.transform.gameObject.tag;
		distance = hit.distance;
		target = hit.transform.gameObject;
	}

	public void Clear()
	{
		isHit = false;
		tag = "";
		distance = -1f;
		target = null;
	}

	public string DebugLine()
	{
		return "Target: " + target.name + " [Hit: " + isHit.ToString() + 
										  " | Tag: " + tag +
										  " | Distance: " + distance.ToString() + "]";
	}

}
