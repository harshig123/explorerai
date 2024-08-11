using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class Heart_Text : MonoBehaviour
{
    //public GameObject GameBeat;
    public int heartrateInput;

    private TextMeshProUGUI heartRateTMP;
    private TextMeshPro heart;
    //private Text HR;
    private float timer;

    // Particle photon cloud URL
    private const string ParticleApiEndpoint = "https://api.particle.io/v1/devices/0a10aced202194944a054140/heart?access_token=1579de344d07142d920b9b07f21c7abdbc3d2491";

    // Number of requests to send per second
    private const int frequency = 10;

    private void Start()
    {
        timer = 0;
        heartrateInput = 74;
        heartRateTMP = GetComponent<TextMeshProUGUI>();
        heart = GetComponent<TextMeshPro>();
        //HR = GetComponent<Text>();
        StartCoroutine(WaitForRequest());
    }

    private void Update()
    {
        // Increment the timer by the time passed since the last frame
        timer += Time.deltaTime;

        // Check if enough time has passed to send new request
        if (timer >= 1f / frequency)
        {
            StartCoroutine(WaitForRequest());
            timer = 0;
        }
    }

    IEnumerator WaitForRequest()
    {
        using UnityWebRequest www = UnityWebRequest.Get(ParticleApiEndpoint);

        yield return www.SendWebRequest();

        //if successful in getting request from web
        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Heart rate detected");
            string work = www.downloadHandler.text;
            _Particle fields = JsonUtility.FromJson<_Particle>(work);
            string jsonRate = fields.result;

            float tempHR = float.Parse(jsonRate);
            heartrateInput = Mathf.FloorToInt(tempHR);
            Debug.Log(heartrateInput);

            heartRateTMP.text = heartrateInput.ToString("f0") + " BPM";
            //heart.text = heartrateInput.ToString("f0") + " BPM";
            //HR.text = heartrateInput.ToString("f0") + " BPM";
        }
        else
        {
            Debug.LogError("Error from web request: " + www.error);
        }
    }

    [System.Serializable]
    public class _Particle {
        public string name;
        public string result;
    }
}
