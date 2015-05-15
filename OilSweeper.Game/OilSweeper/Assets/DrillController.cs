using UnityEngine;
using System.Collections;

public class DrillController : MonoBehaviour {



    // klik
    // na klik mu se pojave 2 gumba - trenutno nebitno gdje
    // 1. gumb poveća radijus
    // 2. gumb poveća transparency

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        var tranf = gameObject.GetComponent<Transform>();
        Debug.Log("Kliknuo na njega" + tranf.position.x + " " + tranf.position.y);
    }
}
