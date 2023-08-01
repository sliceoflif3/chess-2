using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    public void EndLevel()
    {
        gameActive = false;

        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        float minute = Mathf.FloorToInt(timer / 60f);
        float second = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimeText.text = minute.ToString() + " mins " + second.ToString("00" + " secs");
        UIController.instance.levelEndScreen.SetActive(true);
    }
}
