using System.Collections.Generic;

public static class BitmaskHelper {

	private const int layerMaskMin = 0;
	private const int layerMaskMax = 31;

	/// <summary>Creates bitmask from a single value.</summary>
	/// <param name="bitvalue">Insert layer index, that should be converted to bitmask.</param>
	/// <param name="bitmask">Insert bitmask variable to write on. The value will be overwritten.</param>
	/// <param name="reverseBitmask">True: Only ignore bitvalue. False: Ignore everything but bitvalue.</param>
	/// <returns>If bitvalue is outside of layer index (0-31) return false, else return true.</returns>
	public static bool GetBitmask(int bitvalue, out int bitmask, bool reverseBitmask = true)
	{
		bitmask = 0;

		//Check values validity
		if (bitvalue < layerMaskMin || bitvalue > layerMaskMax)
		{
			return false;
		}

		//Create bitmask
		bitmask = 1 << bitvalue;
		if (reverseBitmask)
		{
			bitmask = ~bitmask;
		}

		return true;
	}

	/// <summary>Creates bitmask from array.</summary>
	/// <param name="bitarray">Insert layer index array, that should be converted to bitmask.</param>
	/// <param name="bitmask">Insert bitmask variable to write on. The value will be overwritten.</param>
	/// <param name="reverseBitmask">True: Only ignore bitarray. False: Ignore everything but bitvalue.</param>
	/// <returns>If bitarray values are outside of layer index (0-31) return false, else return true.</returns>
	public static bool GetBitmask(int[] bitarray, out int bitmask, bool reverseBitmask = true)
	{
		bitmask = 0;

		//Check values validity
		for (int i=0; i<bitarray.Length; i++)
		{
			if (bitarray[i] < layerMaskMin || bitarray[i] > layerMaskMax)
			{
				return false;
			}
		}

		//Create bitmask
		for(int i = 0; i<bitarray.Length; i++)
		{
			bitmask = bitmask | (1 << bitarray[i]);
		}
		if (reverseBitmask)
		{
			bitmask = ~bitmask;
		}

		return true;
	}

	/// <summary>Creates bitmask from disctionary with layerIndex & boolean value. Recommended: use CreateEmptyDictionary()</summary>
	/// <param name="bitdict">Insert layer index dictionary, that should be converted to bitmask. int: layerIndex, bool: value</param>
	/// <param name="bitmask">Insert bitmask variable to write on. The value will be overwritten.</param>
	/// <param name="reverseBitmask">False: Ignore layerIndex with value 0. False: Ignore layerIndex with value 1.</param>
	/// <returns>If bitarray values are outside of layer index (0-31) return false, else return true.</returns>
	public static bool GetBitmask(Dictionary<int, bool> bitdict, out int bitmask, bool reverseBitmask = false)
	{
		bitmask = 0;
		
		//Check values validity
		for (int i = layerMaskMin; i <= layerMaskMax; i++)
		{
			if (!(bitdict.ContainsKey(i)))
			{
				return false;
			}
		}

		//Create bitmask
		for (int i = layerMaskMin; i <= layerMaskMax; i++)
		{
			if (bitdict[i])
			{
				bitmask = bitmask | (1 << i);
			}
		}
		if (reverseBitmask)
		{
			bitmask = ~bitmask;
		}

		return true;
	}

	/// <summary>
	/// Create an empty Dictionary with 31 integer keywords' with boolean value.
	/// </summary>
	/// <param name="value">Default value for boolean value.</param>
	/// <returns>Get back filled Dictionary with 31 true or false values.</returns>
	public static Dictionary<int, bool> CreateEmptyDictionary(bool value = false)
	{
		Dictionary<int, bool> tempDict = new Dictionary<int, bool>();
		for(int i = layerMaskMin; i<= layerMaskMax; i++)
		{
			tempDict.Add(i, value);
		}

		return tempDict;
	}

	/// <summary>
	/// Take an bitmask and write it as string with 1's and 0's. Good for Debugging.
	/// </summary>
	/// <param name="bitmask">Input bitmask to convert.</param>
	/// <returns>String with 1's and 0's representing the bitmask.</returns>
	public static string ConvertBitmaskToString(int bitmask)
	{
		const int shift = 1;
		var binary = string.Empty;

		if(bitmask > 0)
		{
			while (bitmask > 0)
			{
				binary = (bitmask & shift) + binary;
				bitmask = bitmask >> 1;
			}
		}
		else if(bitmask < 0)
		{
			uint tempBitmask = (uint)bitmask;
			while (tempBitmask > 0)
			{
				binary = (tempBitmask & shift) + binary;
				tempBitmask = tempBitmask >> 1;
			}
		}
		else
		{
			binary = "0";
		}

		return binary;
	}
}
