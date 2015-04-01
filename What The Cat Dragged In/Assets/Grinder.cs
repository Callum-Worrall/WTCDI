using UnityEngine;
using System.Collections;

public class Grinder : MonoBehaviour {

	public bool filled = false;
	public int corpseCount = 1;
	public int corpsesCollected = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		BloodSated();
	}

	bool BloodSated()
	{
		if (corpsesCollected >= corpseCount)
		{
			filled = true;
			return true;
		}
		return false;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Stunned") && filled == false)
		{
			corpsesCollected += 1;
			Destroy(other.gameObject);
		}
	}
}
