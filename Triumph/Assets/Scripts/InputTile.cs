using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTile : MonoBehaviour {

    public enum Type { Movement, Attack, Heal }
    Type type;
	
	public void SetType(Type type)
    {
        this.type = type;
    }
}
