using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{

	public bool pickedUp;

	// Use this for initialization
	void Start ()
	{
		pickedUp = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//if(pickedUp == true)
		//{
		//	transform.GetChild(0).position = new Vector3 (0, 0, 0);
		//	transform.GetChild(0).position = new Vector3 (0, 0, 0);
		//	transform.GetChild(0).position = new Vector3 (0, 0, 0);
		//}

		if(Input.GetButtonDown("Pickup"))
		{
			if (pickedUp == false)
			{
				PickUp();
				//transform.GetChild(0).rotation = new Quaternion.Euler;
				//transform.GetChild(0).localScale = new Vector3(1.0f, 1.0f, 1.0f);
			}
			else
				Drop();
		}
	}

	void PickUp()
	{
		GameObject[] Corpse = GameObject.FindGameObjectsWithTag("Stunned");

		for (int i = 0; i < Corpse.Length; i++)
		{
			if (Corpse[i].transform.position.x < (transform.position.x + 1.25f) &&
			    Corpse[i].transform.position.x > (transform.position.x - 1.25f) && pickedUp == false)
			{
				//Corpse[i].transform.position = new Vector3 (0, 0, 0);

				Corpse[i].transform.tag = "PickedUp";

				Corpse[i].transform.parent = transform;

				pickedUp = true;
			}
		}
	}

	void Drop()
	{
		//transform.GetChild() = 

		transform.GetChild(0).parent = null;
		
		//Child.transform;

		pickedUp = false;
	}
}
