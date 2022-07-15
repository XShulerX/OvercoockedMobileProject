using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private Controllers _controllers;

    private void Start()
    {
        _controllers = new Controllers();
        new GameInitialization(_controllers);
        _controllers.Initilazation();
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        _controllers.Execute(deltaTime);
    }

    private void OnDestroy()
    {
        _controllers.Cleanup();
    }
}
