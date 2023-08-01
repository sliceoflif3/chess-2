using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    private void Awake()
    {
        instance = this; 
    }

    public Slider explvlSlider;
    public TMP_Text explvlText;

    public LevelUpSelectionButton[] levelUpButtons;

    public GameObject levelUpPanel;

    public TMP_Text coinText;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponUpgradeDisplay;

    public TMP_Text timeText;

    public GameObject levelEndScreen;
    public TMP_Text endTimeText;

    public string mainMenuName;

    public GameObject pauseScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLvl)
    {
        explvlSlider.maxValue = levelExp;
        explvlSlider.value = currentExp;

        explvlText.text = "Level: " + currentLvl;
    } 

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void updateCoin()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }

    public void PurchaseMoveSpeed()
    {
        PlayerStatController.instance.PurchaseMoveSpeed();
        SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        PlayerStatController.instance.PurchaseHealth();
        SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        PlayerStatController.instance.PurchasePickupRange();
        SkipLevelUp();
    }

    public void PurchaseMaxWeapon()
    {
        PlayerStatController.instance.PurchaseMaxWeapon();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minute = Mathf.FloorToInt(time / 60f);
        float second = Mathf.FloorToInt(time % 60);

        timeText.text = "Time: " + minute + ":" + second.ToString("00"); 
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        if(pauseScreen.activeSelf == false) { 
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            
            pauseScreen.SetActive(false);
            if (levelUpPanel.activeSelf == false) { 
                Time.timeScale = 1f;
            }
        }
    }
}
