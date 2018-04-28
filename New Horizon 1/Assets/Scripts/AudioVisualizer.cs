using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVisualizer : MonoBehaviour {

    AudioSource source;
    float[] samples;
    float[] spectrum;
    float sampleRate;

    Image[] bar;


    float maxVisualScale = .1f;
    float visualModifier = 250f;
    float smoothSpeed = .25f;
    float keepPercentage = .1f;

    Transform[] visualList = new Transform[18];
    
    int numOfVisuals = 18;
    float[] visualScale = new float[18];

    float xScale;
    const int SAMPLE_SIZE = 256;

	// Use this for initialization
	void Start () {

        source = GetComponent<AudioSource>();
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[256];
        sampleRate = AudioSettings.outputSampleRate;

        SpawnVisual();
	}
	
	// Update is called once per frame
	void Update () {

        GetSoundData();
        UpdateVisualization();

        if (!source.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// scales each image according to collected sound data
    /// </summary>
    void UpdateVisualization()
    {
        int index = 0;
        int spectrumIndex = 0;
        int averageSize = (int)((SAMPLE_SIZE * keepPercentage) / numOfVisuals);

        while (index < numOfVisuals)
        {
            int j = 0;
            float sum = 0;
            while (j < averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }
            float scaleY = (sum / averageSize) * 2f;
            visualScale[index] -= Time.deltaTime * smoothSpeed;
            if (visualScale[index] < scaleY)
            {
                visualScale[index] = scaleY;
            }
            if (visualScale[index] > maxVisualScale)
            {
                visualScale[index] = maxVisualScale;
            }
            visualList[index].localScale = new Vector3(xScale, 2f * visualScale[index], 0f);
            index++;
        }
    }
    /// <summary>
    /// gets the data that we need from the sound source, each frame
    /// </summary>
    void GetSoundData()
    {
        source.GetOutputData(samples, 0);

        // Get sound spectrum
        source.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
    }
    /// <summary>
    /// draws visualizer 
    /// </summary>
    void SpawnVisual()
    {
        bar = GameObject.FindGameObjectWithTag("tutorialUI").GetComponentsInChildren<Image>();
        for (int i = 3; i < 21; i++)
        {
            visualList[(i - 3)] = bar[i].transform;
        }
        xScale = visualList[2].localScale.x; // arbitrarily chose index 2
    }
}
