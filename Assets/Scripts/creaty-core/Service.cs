using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class Service<T> : MonoBehaviour where T : Service<T>
{
    public static T instance { get; protected set; }

    void Start()
    {
        if (instance != null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0) {
                DestroyImmediate(instance.gameObject);
                Start();

                return;
            }

            if (instance != this)
            {
                Destroy(this);
                throw new System.Exception($"[{instance.GetType().Name}] Service already exists!");
            }
        }
        else
        {
            instance = (T)this;
        }


        Initialize();
    }

    void Initialize()
    {
        print($"[{instance.GetType().Name}] Service initialized");
        name = $"{instance.GetType().Name}${Random.Range(10000, 99999)}";

        DontDestroyOnLoad(this);
        Execute();
    }

    public static T GetInstance() {
        if (instance == null)
            return Instantiate(new GameObject()).AddComponent<T>();

        return instance;
    }

    public abstract void Execute();
}