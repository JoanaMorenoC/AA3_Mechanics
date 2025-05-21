using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum Method { GERSTNER, SINUSOIDAL };

public class WavesController : MonoBehaviour
{

    //element
    private Mesh mesh;
    public Vector3[] baseVertices;
    public Vector3[] displacedVertices;
    public int numberOfNodes;
    public float distanceNodes;

    //wave properties
    public Method method;
    public GerstnerWave gerstner;
    public SinusoidalWave sinusoidal;

    public float waveLenght = 2f; //lambda
    public float amplitude = 1f;// A
    float frequency; // omega
    public float phase = 0f; //phi
    private float angle = Mathf.PI/2; //theta
    Vector2 waveVector; //vector k
    Wave waveInfo;


    //time
    public float time = 0;

    //gravity
    private float gravity = 9.81f;

    void Awake()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = Instantiate(mf.mesh);
        mf.mesh = mesh;

        baseVertices = mesh.vertices;
        displacedVertices = new Vector3[baseVertices.Length];

        waveVector = (2 * Mathf.PI / waveLenght) * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        frequency = Mathf.Sqrt(waveVector.magnitude * gravity);

        waveInfo.waveLenght = waveLenght;
        waveInfo.amplitude = amplitude;
        waveInfo.frequency = frequency;
        waveInfo.phase = phase;
        waveInfo.angle = angle;
        waveInfo.vector = waveVector;
    }

    void Update()
    {
        time += Time.deltaTime;

        switch(method)
        {
            case Method.GERSTNER:
                for (int i = 0; i < baseVertices.Length; i++)
                    displacedVertices[i] = gerstner.CalculatePoint(baseVertices[i], waveInfo, time);
                break;
            case Method.SINUSOIDAL:
                for (int i = 0; i < baseVertices.Length; i++)
                    displacedVertices[i] = sinusoidal.CalculatePoint(baseVertices[i], waveInfo, time);
                break;
        }
        
        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    }

    public Wave GetWaveInfo()
    {
        return waveInfo;
    }

    public float GetTime()
    {
        return time;
    }

    public Method GetCurrentMethod()
    {
        return method;
    }
}