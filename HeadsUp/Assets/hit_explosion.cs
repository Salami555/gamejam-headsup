using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_explosion : MonoBehaviour {

    //to be enhanced as you please
    //I mean, it's an explosion, that thing should leave room for a sh*tload of fancy code
	void Awake () {
        Destroy(this.gameObject, 0.1f);
	}

}