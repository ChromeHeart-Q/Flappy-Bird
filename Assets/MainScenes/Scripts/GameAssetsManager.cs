using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssetsManager : MonoBehaviour
{
    private static GameAssetsManager Instance;
    public static GameAssetsManager GetInstance()
    {
        return Instance;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public GameObject PipePrefab;
}
