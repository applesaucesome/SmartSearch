using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SmartSearch : MonoBehaviour {

    private string searchTerm;
    private string query;
    public Transform searchResults;
    
    public List<string> data = new List<string>() { "test", "blah", "cat", "dog" };
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private string Matcher(string item) {
        string origSearchTerm = item;
        string searchTerm = item.ToLower();
        string query = this.query.ToLower();

        if(query.Length > 1 && searchTerm.IndexOf(query[0]) > -1) {

            int i = 0;
            return SearchIt(origSearchTerm, query, 0, i);

        } else if(searchTerm.IndexOf(query) > -1) {

            return searchTerm;

        } else {
            return null;
        }

        
    }
    private string SearchIt(string origSearchTerm, string query, int indexFoundAt, int i) {

        searchTerm = searchTerm.Substring(indexFoundAt);

        if(searchTerm.IndexOf(query[i]) > -1) {

            int foundItAt = searchTerm.IndexOf(query[i]);

            i++;
            return SearchIt(origSearchTerm, query, indexFoundAt, i);

        } else if (i == query.Length) {

            return origSearchTerm;

        } else {
            return null;
        }

        

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
