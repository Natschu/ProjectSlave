public static class UtilityFunctions {

	public static float Remap(float value, float lowInput, float highInput, float lowResult, float highResult)
	{
		//Example:
		//Input	->	5 | 0, 10 | -1, 0
		//			-1 + (5 - 0) * (0 - -1) / (10 - 0)
		//		=>	-0.5
		return lowResult + (value - lowInput) * (highResult - lowResult) 
				/ (highInput - lowInput);
	}
	
}