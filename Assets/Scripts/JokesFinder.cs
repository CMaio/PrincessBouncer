using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JokesFinder : MonoBehaviour
{
    //https://sv443.net/jokeapi/v2/
    List<Joke> jokes = new List<Joke>();
    public GameObject jokePrefab; 
    static string url = "https://v2.jokeapi.dev/joke/Programming,Miscellaneous,Pun,Spooky,Christmas?blacklistFlags=nsfw,religious,political,racist,sexist&type=twopart&idRange=0-319&amount=10";

    [System.Serializable]
    public class Joke
    {
        public string category;
        public string type;
        public string joke;
        public string setup;
        public string delivery;
    }

    [System.Serializable]
    public class JokeArray
    {
        public bool error;
        public int amount;
        public Joke[] jokes;
    }
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest(url));    
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Succesful"); 

                    var text = webRequest.downloadHandler.text;

                    JokeArray jokesResponse = JsonUtility.FromJson<JokeArray>(text);

                    foreach (Joke tmpJoke in jokesResponse.jokes)
                    {
                        jokes.Add(tmpJoke);
                    }

                    if (jokes.Count < 50)
                    {
                        StartCoroutine(GetRequest(url));
                    }
                    else
                    {
                        Debug.Log(jokes.Count);
                        generateJokesObjects();
                    }                   
                    break;
            }
        }
    }

    public void generateJokesObjects()
    {
        foreach (Joke joke in jokes)
        {
            GameObject tmpJoke = Instantiate(jokePrefab,new Vector3(0,0,0),Quaternion.identity);
            if(joke.type == "twoparts")
            {
                tmpJoke.GetComponent<JokeInformation>().saveJokeInfoDouble(joke.category, joke.type, joke.setup, joke.delivery);
            }
            else
            {
                tmpJoke.GetComponent<JokeInformation>().saveJokeInfoSingle(joke.category, joke.type,joke.joke);

            }
        }

        
    }
}
