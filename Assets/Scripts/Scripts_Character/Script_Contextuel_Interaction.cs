using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Contextuel_Interaction : MonoBehaviour
{
    public static Script_Contextuel_Interaction Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Interaction()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
