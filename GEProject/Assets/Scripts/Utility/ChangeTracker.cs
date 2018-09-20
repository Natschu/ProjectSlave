using UnityEngine;

public class ChangeTracker<T> {

	private T _value;			//Value to be tracked
	private bool _hasChanged;	//Flag for changes

	public ChangeTracker() {
		_hasChanged = false;
	}
	public ChangeTracker(T initalValue)	{
		_value = initalValue;
		_hasChanged = true;
	}

	/// <summary>
	/// Set or get value. If value is changed, flag is set to true.
	/// </summary>
	public T value
	{
		get { return _value; }
		set {
			if (Equals(_value, value)) return;

			_value = value;
			_hasChanged = true;
		}
	}

	/// <summary>
	/// Get flag. Is true, if tracked value has changed.
	/// </summary>
	public bool hasChanged
	{
		get { return _hasChanged; }
	}

	/// <summary>
	/// Reset the flag to false.
	/// </summary>
	public void ResetChangedFlag()
	{
		_hasChanged = false;
	}

}