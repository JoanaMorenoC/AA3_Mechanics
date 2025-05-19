using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class gerstnerWaves : MonoBehaviour
{

    //element
    private Mesh mesh;
    public Vector3[] baseVertices;
    public Vector3[] displacedVertices;
    public int numberOfNodes;
    public float distanceNodes;


    //wave position
    /*
    private Vector3 position;
    public Vector2 initialPosition = new Vector2(0,0);
    public Vector2[] allInitialPos;*/


    //wave properties
    public float waveLenght = 2f; //lambda
    public float amplitude = 1f;// A
    public float frequency; // omega
    public float phase = 0f; //phi
    private float angle = Mathf.PI/2; //theta
    private Vector2 waveVector; //vector k



    //time
    public float time = 0;

    //gravity
    private float gravity = 9.81f;


    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        baseVertices = mesh.vertices;
        displacedVertices = new Vector3[baseVertices.Length];

        waveVector = (2*Mathf.PI/waveLenght)* new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        frequency =Mathf.Sqrt(waveVector.magnitude*gravity);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            Vector2 worldXZ = new Vector2(vertex.x, vertex.z);
            Vector2 displacedXZ = worldXZ - (waveVector.normalized * amplitude * Mathf.Sin(Vector2.Dot(waveVector, worldXZ) - frequency * time + phase));
            float height = amplitude * Mathf.Cos(Vector2.Dot(waveVector, worldXZ) - frequency * time + phase);

            displacedVertices[i] = new Vector3(displacedXZ.x, height, displacedXZ.y);
        }

        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals(); 
    }
}