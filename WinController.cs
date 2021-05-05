using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinController : MonoBehaviour
{
    public GameObject Targets;
    public Text enemyText;
    public Text infoText;


    public Text timerText;

    private float secondsCount;
    private int minuteCount;
    private bool win = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!win)
            UpdateTimerUI();

        //Display Counter for Targets Remaining
        int aliveEnemies = 0;
        foreach (TargetScript Target in Targets.GetComponentsInChildren<TargetScript>())
        {
            if (Target.isHit == false)
            {
                aliveEnemies++;
            }
        }
        enemyText.text = "Targets Remaining: " + aliveEnemies;

        if (aliveEnemies == 0)
        {
            win = true;
            infoText.gameObject.SetActive(true);
            infoText.text = "All targets down! Good jor\nYour time was " +(minuteCount + "m:" + (int)secondsCount + "s.") +"\n\nPress Y to try and improve your time, or press Space to continue.";
      
            
            

            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (SceneManager.GetActiveScene().buildIndex < 6)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                else
                    SceneManager.LoadScene(0);
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }
        }

        

    }
    public void UpdateTimerUI()
    {
        
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        
            
    }

}
