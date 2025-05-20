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

    //wave properties
    public float waveLenght = 2f; //lambda
    public float amplitude = 1f;// A
    public float frequency; // omega
    public float phase = 0f; //phi
    private float angle = Mathf.PI/2; //theta
    public Vector2 waveVector; //vector k



    //time
    public float time = 0;

    //gravity
    private float gravity = 9.81f;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        mesh = Instantiate(mf.mesh);
        mf.mesh = mesh;

        baseVertices = mesh.vertices;
        displacedVertices = new Vector3[baseVertices.Length];

        waveVector = (2 * Mathf.PI / waveLenght) * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        frequency = Mathf.Sqrt(waveVector.magnitude * gravity);
    }



    void Update()
    {
        time += Time.deltaTime;

        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            Vector2 worldXZ = new Vector2(vertex.x, vertex.z); // original position
            float wave = Vector2.Dot(waveVector, worldXZ) - frequency * time + phase;

            // Calculate new position
            float displacementY = amplitude * Mathf.Cos(wave);
            Vector2 displacementXZ = waveVector.normalized * amplitude * Mathf.Sin(wave);

            displacedVertices[i] = new Vector3(
                vertex.x + displacementXZ.x,
                displacementY,
                vertex.z + displacementXZ.y
            );
        }

        mesh.vertices = displacedVertices;
        mesh.RecalculateNormals();
    }

}