using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] RectTransform creditsObject;
    [SerializeField] float startYPosition;
    [SerializeField] float endYPosition;
    [SerializeField] float totalTimeForCredits;

    float sceneStartTime;

    private void Awake()
    {
        sceneStartTime = Time.time;
    }
    void Update()
    {
        float t = (Time.time - sceneStartTime) / totalTimeForCredits;
        if (t > 1.0f)
            t = 1.0f;

        creditsObject.localPosition = new Vector3(0.0f, Mathf.Lerp(startYPosition, endYPosition, t), 0.0f);

        if (Input.anyKeyDown)
            SceneManager.LoadScene(0);
    }
}
