using UnityEngine;

public class _8MouseInput : MonoBehaviour {

	/// <summary>
	/// OnMouseDrag is called when the user has clicked on a GUIElement or Collider
	/// and is still holding down the mouse.
	/// </summary>
	void OnMouseDrag()
	{
		float distanceToScreen = Camera.main
			.WorldToScreenPoint(transform.position).z;
		transform.position = Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, 
			Input.mousePosition.y, distanceToScreen));
	}
}
