using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JokesFinder : MonoBehaviour
{

    [System.Serializable]
    public class Joke
    {
        public string category;
        public string type;
        public string joke;
        public string setup;
        public string delivery;
    }
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("https://v2.jokeapi.dev/joke/Any?lang=en"));

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
                    Joke joke = JsonUtility.FromJson<Joke>(text);
                    if (joke.type == "twopart")
                    {
                        Debug.Log(joke.setup);
                        Debug.Log(joke.delivery);
                    }
                    else
                    {
                        Debug.Log(joke.joke);
                    }
                    break;
            }
        }
    }
}
