using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouseToGameWindow : MonoBehaviour
{
    static LockMouseToGameWindow instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
// Nothing
#else
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }
}
