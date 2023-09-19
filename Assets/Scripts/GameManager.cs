using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instant;

    [Header("PLayer")]
    [SerializeField] PlayerMovement player;
    [SerializeField] float respawnTime = 3f;

    [Header("Blackout Setting")]
    [SerializeField] float lightOffMinInterval = 5f;
    [SerializeField] float lightOffMaxInterval = 10f;

    [SerializeField] float lightOffTimeMin = 2f;
    [SerializeField] float lightOffTimeMax = 5f;

    [Header("UI")]
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject winScreen;

    private float blackOutTime;
    private bool isRandom = false;
    private float timer;
    private bool turnOffLight;
    private float deadClock = 0f;
    private bool isOver = false;
    private bool isWin = false;
    private int keys = 0;

    void Start()
    {
        if (Instant == null)
        {
            Instant = this;
        }
        else
        {
            Destroy(gameObject);
        }

        turnOffLight = false;
        timer = Random.Range(lightOffMinInterval, lightOffMaxInterval);
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            turnOffLight = true;
        }

        if(turnOffLight)
        {
            if(!isRandom)
            {
                blackOutTime = Random.Range(lightOffTimeMin, lightOffTimeMax);
                isRandom = true;
            }
            blackOutTime -= Time.deltaTime;
        }

        if(turnOffLight && blackOutTime <= 0)
        {
            turnOffLight = false;
            timer = Random.Range(lightOffMinInterval, lightOffMaxInterval);
            isRandom = false;
        }

        if(player.isDead())
        {
            isOver = player.isDead();
            deathScreen.SetActive(isOver);
            deadClock += Time.deltaTime;
            if(deadClock >= respawnTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(isWin)
        {
            winScreen.SetActive(isWin);
        }
    }

    public bool isTurnOffLight()
    {
        return turnOffLight;
    }

    public bool isGameOver()
    {
        return isOver;
    }

    public void addKey()
    {
        keys++;
    }

    public void useKey()
    {
        keys--;
    }

    public bool hadKey()
    {
        return keys > 0;
    }

    public void setWin(bool win)
    {
        isWin = win;
    }
}
