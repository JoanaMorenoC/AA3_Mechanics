using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class gerstnerWaves : MonoBehaviour
{

    //element

    public Transform[] nodes;
    public int numberOfNodes;
    public float distanceNodes;


    //wave position
    private Vector3 position;
    public Vector2 initialPosition = new Vector2(0,0);
    public Vector2[] allInitialPos;


    //wave properties
    public float waveLenght = 2f; //lambda
    public float amplitude = 1f;// A
    public float frequency; // omega
    public float phase = 0f; //phi
    private float angle = Mathf.PI/2; //theta
    private Vector2 waveVector; //vector k



    //time

    public float time = 0;
    public float stepTime = 0.01f;

    //gravity
    private float gravity = 9.81f;


    // Start is called before the first frame update
    void Start()
    {
        generateStartpositions();
        setInitialTransform();

        waveVector = (2*Mathf.PI/waveLenght)* new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        frequency =Mathf.Sqrt(waveVector.magnitude*gravity);
    }

    // Update is called once per frame
    void Update()
    {
        time += stepTime;
        for(int i = 0; i< numberOfNodes; i++)
        {
            nodes[i].position = calculateGerstnerWave(allInitialPos[i], time);
        }
    }

    Vector3 calculateGerstnerWave(Vector2 initialPoint, float time1)
    {
        Vector2 planePosition; //vector x
        float height; //y

        Vector3 newPosition;

        planePosition = initialPoint - (waveVector /waveVector.magnitude)*amplitude*Mathf.Sin(Vector2.Dot(waveVector, initialPoint) - frequency * time+ phase);
        height = amplitude * Mathf.Cos(Vector2.Dot(waveVector, initialPoint) - frequency * time + phase);

        newPosition = new Vector3(planePosition.x, height, planePosition.y);

        return newPosition;
    }

    void generateStartpositions()
    {
        allInitialPos =  new Vector2[numberOfNodes];

        float distance = 0;

        for (int i = 0; i < numberOfNodes; i++)
        {
            allInitialPos[i] = new Vector2(initialPosition.x, initialPosition.y +distance);
            distance += distanceNodes;
        }
    }
    void setInitialTransform()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            nodes[i].position = new Vector3(allInitialPos[i].x, 0, allInitialPos[i].y);
        }
    }
}
