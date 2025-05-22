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

    //wave properties
    public Method method;
    public GerstnerWave gerstner;
    public SinusoidalWave sinusoidal;

    [Header("Sinusoidal Settings")]
    public float sinusoidalLenght = 2f; //lambda
    public float sinusoidalAmplitude = 1f;// A
    public float sinusoidalSpeed = 1f;// A
    private float sinusoidalAngle = Mathf.PI/2; //theta

    [Header("Gerstner Settings")]
    public float gerstnerLenght = 2f; //lambda
    public float gerstnerAmplitude = 1f;// A
    public float sinusoidalPhase = 0f; //phi

    float frequency; // omega
    Vector2 gerstnerWaveVector; //vector k

    Wave sinusoidalInfo;
    Wave gerstnerInfo;


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

        gerstnerWaveVector = (2 * Mathf.PI / gerstnerLength) * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        frequency = Mathf.Sqrt(waveVector.magnitude * gravity);

        waveInfo.waveLenght = waveLenght;
        waveInfo.amplitude = amplitude;
        waveInfo.speed = speed;
        waveInfo.frequency = frequency;
        waveInfo.phase = phase;
        waveInfo.angle = angle;
        waveInfo.vector = waveVector;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (method == Method.GERSTNER)
                method = Method.SINUSOIDAL;
            else
                method = Method.GERSTNER;
        }

        switch(method)
        {
            case Method.GERSTNER:
                for (int i = 0; i < baseVertices.Length; i++)
                    displacedVertices[i] = gerstner.CalculatePoint(baseVertices[i], gerstnerInfo, time);
                break;
            case Method.SINUSOIDAL:
                for (int i = 0; i < baseVertices.Length; i++)
                    displacedVertices[i] = sinusoidal.CalculatePoint(baseVertices[i], sinusoidalInfo, time);
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