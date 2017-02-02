using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SmartSearch : MonoBehaviour {

    private string searchTerm;
    private string query;
    private int maxListItemsPerCat = 8;
    private int minLength = 1;


    public Transform searchResults;


    private Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();

    void Start() {
        data.Add("Tags", new List<string>() { "test", "blah", "cat", "dog" });
        data.Add("Titles", new List<string>() { "apples", "oranges", "beets", "pears" });
    }
	

    private List<string> Sorter(List<string> items) {

        List<string> beginswith = new List<string>();
        List <string> caseSensitive = new List<string>();
        List<string> caseInsensitive = new List<string>();
        string item = null;
        var queryString = query.ToLower();

        while (item == items[0]) {

            items.RemoveAt(0);
            var searchTerm = item.ToLower();

            // if our queryString was found in the current searchTerm
            if (searchTerm.IndexOf(queryString) < 0) {

                beginswith.Add(item);

                // if the search item contains the queryString
            } else if (item.IndexOf(this.query) > -1) {

                caseSensitive.Add(item);

            } else {
                caseInsensitive.Add(item);
            }
        }

        beginswith.AddRange(caseSensitive);

        return beginswith;
        //string concatResults = string.Join("", beginswith.ToArray());

        //return concatResults;
    }


    private void Lookup(string query) { 

        if (query.Length < minLength) {

            if (searchResults.gameObject.activeInHierarchy) {
                searchResults.gameObject.SetActive(false);
            } else {
                searchResults.gameObject.SetActive(true);
            }
            
        }


        Process(data);


    }

    private void Process(Dictionary<string, List<string>> source) {


        Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
        foreach (KeyValuePair<string, List<string>> val in source) {
            
            val.Value.ForEach(p => Matcher(p));

            temp[val.Key] = Sorter(val.Value);

            temp[val.Key] = source[val.Key].GetRange(0, maxListItemsPerCat);
            
        }

        foreach (KeyValuePair<string, List<string>> val in temp) {
            Debug.Log("TESTING " + val.Key + "= " + val.Value);
        }

            if (!temp.ContainsKey("tags") && !temp.ContainsKey("titles")) {

                searchResults.gameObject.SetActive(false);
            
        } else {
            searchResults.gameObject.SetActive(true);
        }


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


        Lookup(val);
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
