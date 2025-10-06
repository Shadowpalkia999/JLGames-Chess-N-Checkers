using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessEngine : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake Stockfish");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting the Chess Engine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        Debug.Log("Hello Stockfish");
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy Stockfish");
    }
}
