using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject platfrom;

    private float minX = -2.2f, maxX = 2.2f, minY = -4.7f, maxY = -3.7f;

    private bool lerpCamera;
    private float lerpTime = 1.5f;
    private float lerpX;


    [SerializeField] private Text scoreText;
    private int score;

    public int Score
    {
        get => score;
        set => value = score;
    }
    void Awake() {
        MakeInstance();
        CreateInitialPlatforms ();
    }

    void Update() {
        if (lerpCamera) {
            LerpTheCamera();		
        }
    }

    void MakeInstance() {
        if (instance == null)
            instance = this;
    }

    void CreateInitialPlatforms() {
        Vector3 temp = new Vector3 (Random.Range(minX, minX + 1.2f), Random.Range(minY, maxY), 0);

        Instantiate (platfrom, temp, Quaternion.identity);

        temp.y += 2f;

        Instantiate (player, temp, Quaternion.identity);

        temp = new Vector3 (Random.Range(maxX, maxX - 1.2f), Random.Range(minY, maxY), 0);

        Instantiate (platfrom, temp, Quaternion.identity);

    } // CreateInitialPlatforms

    void LerpTheCamera() {
        float x = Camera.main.transform.position.x;

        x = Mathf.Lerp (x, lerpX, lerpTime * Time.deltaTime);

        Camera.main.transform.position = new Vector3 (x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        if(Camera.main.transform.position.x >= (lerpX - 0.07f)) {
            lerpCamera = false;
        }

    }

    public void CreateNewPlatformAndLerp(float lerpPosition) {

        CreateNewPlatform ();

        lerpX = lerpPosition + maxX;
        lerpCamera = true;
    }

    void CreateNewPlatform() {

        float cameraX = Camera.main.transform.position.x;

        float newMaxX = (maxX * 2) + cameraX;

        Instantiate (platfrom, new Vector3(Random.Range(newMaxX, newMaxX - 1f), Random.Range(maxY, maxY - 1.2f), 0), Quaternion.identity);

    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    

} // GameManager

