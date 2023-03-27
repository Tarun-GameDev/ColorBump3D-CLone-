using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameCompledted = false;

    public Material[] mats;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {

        foreach (Material _mat in mats)
        {
            _mat.mainTextureOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }

    public IEnumerator LevelCompleted()
    {
        gameCompledted = true;
        CameraController.instance.canmove = false;
        Player.instance.levelCompleted = true;
        yield return new WaitForSeconds(.5f);
        UIManager.instance.LevelCompleted();
    }

}
