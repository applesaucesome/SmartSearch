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


    public Transform searchResults;
    
    public List<string> data = new List<string>() { "test", "blah", "cat", "dog" };

	

    private string Sorter(List<string> items) {
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

        string concatResults = string.Join("", beginswith.ToArray());

        return concatResults;
    }


    private void Process(Dictionary<string, List<string>> source) {

        foreach (List<string> val in source) {

            string myKey = source.FirstOrDefault(x => x.Value == val).Key;

            // Need to figure out how to pass the param to Matcher()
            source[myKey] = val.Where(Matcher(x));
            source[myKey] = Sorter(val);

            source[myKey] = source[myKey].GetRange(0, maxListItemsPerCat);

        }



        if (!source.ContainsKey("tags") && !source.ContainsKey("titles")) {

                searchResults.gameObject.SetActive(false);
            
        } else {
            searchResults.gameObject.SetActive(true);
        }


    }
    private List<string> Matcher(string item) {
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
