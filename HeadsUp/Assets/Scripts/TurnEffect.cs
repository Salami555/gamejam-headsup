using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEffect : MonoBehaviour {

    public Quaternion toRotate; //hier die Rotation, um die rotiert werden soll

    private float effect_duration = 0.25f; //sollte in etwa die Dauer der Particlesystems sein
    //Dazu sollte man die Lebensdauer der Partikel beachten
    
    
    private float creation_time;
    private Quaternion start_rot;

	void Start () {
        creation_time = Time.time;
        start_rot = transform.rotation;
        Destroy(this.gameObject, 1f);
	}
	
	void Update () {
        transform.rotation = Quaternion.Lerp(start_rot, start_rot * toRotate, (Time.time - creation_time)/effect_duration);
	}
}
