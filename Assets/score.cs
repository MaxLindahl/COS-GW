using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class score : MonoBehaviour {


    public int currentscore;
	// Use this for initialization
	void Start () {
        currentscore = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void givescore(int sco)
    {
        currentscore = currentscore + sco;
    }
}
