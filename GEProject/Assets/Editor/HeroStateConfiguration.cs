using UnityEngine;
using UnityEditor;

public class HeroStateConfiguration : EditorWindow {

	[MenuItem("Window/Hero States Configuration")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow<HeroStateConfiguration>("Hero States");
	}

	void OnGUI()
	{
		
	}

}
