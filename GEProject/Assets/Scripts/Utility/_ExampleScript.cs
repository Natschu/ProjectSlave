using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ExampleScript : MonoBehaviour {

	public int numParticles = 9;
	public float input = 90.0f;

	void Update()
	{
	}


	#region BitmaskHelper
	private void testBitmaskHelperSingle()
	{
		int[] value = new int[4];
		value[0] = 0;
		value[1] = 1;
		value[2] = 2;
		value[3] = 3;

		int bitmask;

		for(int i = 0; i<value.Length; i++)
		{
			
			if(BitmaskHelper.GetBitmask(value[i], out bitmask, false))
			{
				Debug.Log("(Value " + i + ") Bitmask: " + bitmask +
				" [" + BitmaskHelper.ConvertBitmaskToString(bitmask) + "]");
			}
			if (BitmaskHelper.GetBitmask(value[i], out bitmask))
			{
				Debug.Log("(Value " + i + ") Reversed Bitmask: " + bitmask +
				" [" + BitmaskHelper.ConvertBitmaskToString(bitmask) + "]");
			}
		}

	}

	private void testBitmaskHelperArr()
	{
		//Create array with input between 1 and 31
		int[] arr = new int[4];
		arr[0] = 0;
		arr[1] = 1;
		arr[2] = 2;
		arr[3] = 3;

		int bitmask; //bitmask is set to 0 at the start of GetBitmask()

		//Put out reversed and normal bitmask
		if (BitmaskHelper.GetBitmask(arr, out bitmask, false))
		{
			Debug.Log("(Array) Bitmask: " + bitmask +
				" [" + BitmaskHelper.ConvertBitmaskToString(bitmask) + "]");
		}
		if (BitmaskHelper.GetBitmask(arr, out bitmask))
		{
			Debug.Log("(Array) Reversed Bitmask: " + bitmask +
				   " [" + BitmaskHelper.ConvertBitmaskToString(bitmask) + "]");
		}
	}

	private void testBitmaskHelperDict()
	{
		//Creates 'empty' bitdict with values [1-31] = false (default)
		Dictionary<int, bool> bitDict = BitmaskHelper.CreateEmptyDictionary();
		int bitmask;

		bitDict[0] = true;
		bitDict[1] = true;
		bitDict[2] = true;
		bitDict[3] = true;

		if (BitmaskHelper.GetBitmask(bitDict, out bitmask, false))
		{
			Debug.Log("(Dictionary) Bitmask: " + bitmask +
				" [" + BitmaskHelper.ConvertBitmaskToString(bitmask) + "]");
		}
		if (BitmaskHelper.GetBitmask(bitDict, out bitmask))
		{
			Debug.Log("(Dictionary) Reversed Bitmask: " + bitmask +
				   " [" + BitmaskHelper.ConvertBitmaskToString(bitmask) + "]");
		}		
	}
	#endregion
}
