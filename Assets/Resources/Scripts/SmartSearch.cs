using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SmartSearch : MonoBehaviour {

    public Transform searchResults;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTextChange(string val) {
        Debug.Log("TEXT CHANGED: " + val);

        GameObject result = new GameObject("Result", typeof(Text));

        string resultText = "BABABOOEY";
        result.name = resultText;
        result.GetComponent<Text>().text = resultText;
        
        result.transform.SetParent(searchResults);

    }

    public void OnTextSubmit(string val) {
        Debug.Log("SEARCH SUBMITTED: " + val);
    }
}
