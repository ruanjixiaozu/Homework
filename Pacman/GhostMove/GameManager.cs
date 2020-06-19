using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;



    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    //public Vector3 m;

    //public Vector3 n;
    public float magnetTime = 0;
    public int intTimer = 0;
    public GameObject pacman;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject inky;
    public GameObject pinky;
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject shopPanel;
    public GameObject startCountDownPrefab;
    public GameObject gameoverPrefab;
    public GameObject winPrefab;
    public AudioClip startClip;
    public Text remainText;
    public Text nowText;
    public Text scoreText;
    public Text finalScore;
    public Text buySuccessfull;
    public Text goldText;  //显示金币数目

    public bool isSuperPacman = false;
    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    private List<GameObject> pacdotGos = new List<GameObject>();
    private int pacdotNum = 0;
    private int nowEat = 0;
    public int score = 0;
    private float timer = 100f;
    public GameObject time;
    private bool isStart = false;

    public int bulletCount;

    private int GunPurchaseNum = 0;
    private int MagPurchaseNum = 0;

    static int gold;


    private void Awake()
    {
        _instance = this;
        Screen.SetResolution(1024, 768, false);
        int tempCount = rawIndex.Count;

        bulletCount = 0;

        for (int i = 0; i < tempCount; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }

        foreach (Transform t in GameObject.Find("Maze").transform)
        {
            pacdotGos.Add(t.gameObject);
        }

        pacdotNum = GameObject.Find("Maze").transform.childCount;
    }

    private void Start()
    {
        SetGameState(false);
    }


    private void Update()
    {

        if (PacmanMove.isMagnet == true)
        {
            magnetTime = magnetTime + 0.02f;
            intTimer = (int)magnetTime;
            if (intTimer > 5)
            {
                PacmanMove.isMagnet = false;
            }
        }

        if ((nowEat == pacdotNum && pacman.GetComponent<PacmanMove>().enabled != false))
        {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            SetGameState(false);
        }

        if (GhostMove.isWin == true)
        {
            gold = gold + 100;
            Invoke("ReStart", 6f);
            GameObject.Destroy(pacman);
            GameManager.Instance.gamePanel.SetActive(false);
            Instantiate(GameManager.Instance.gameoverPrefab);

        }

        if (nowEat == pacdotNum)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(0);
            }
        }

        if (gamePanel.activeInHierarchy)
        {
            remainText.text = "Remain:\n\n" + (pacdotNum - nowEat);
            nowText.text = "Eaten:\n\n" + nowEat;
            scoreText.text = "Score:\n\n" + score;
            goldText.text = ":" + gold;

        }
        if (isStart)
        {
            Invoke("startTime", 4f);
            //startTime();
        }
    }

    public void OnStartButton()
    {
        StartCoroutine(PlayStartCountDown());

        AudioSource.PlayClipAtPoint(startClip, new Vector3(0, 0, -5));

        startPanel.SetActive(false);

        isStart = true;
    }

    public void OnExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnShopButton()
    {
        Time.timeScale = 0;
        shopPanel.SetActive(true);
    }

    public void OnBuyGunButton()
    {
        if (gold >= 150)
        {
            if (GunPurchaseNum >= 5)
            {
                buySuccessfull.text = "You can only buy fifth!";
            }
            else if (GunPurchaseNum < 5)
            {
                gold = gold - 150;
                buySuccessfull.text = "You've bought 5 bullets!";
                Weapon.IsShooting = true;
                GunPurchaseNum += 1;
                bulletCount = bulletCount + 5;
            }
        }
        else
        {
            buySuccessfull.text = "Coins are not enough!";
        }

    }

    public void AutoAdd()
    {
        bulletCount--;

    }
    public void OnBuyMagnetButton()
    {
        if (gold >= 300)
        {
            if (MagPurchaseNum != 0)
            {
                buySuccessfull.text = "You can only buy once!";
            }
            else if (MagPurchaseNum == 0)
            {
                gold = gold - 300;
                PacmanMove.isMagnet = true;
                buySuccessfull.text = "Buy successfully!";
                MagPurchaseNum += 1;

            }
        }
        else
        {
            buySuccessfull.text = "Coins are not enough!";
        }
    }

    public void OnContinueButton()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1;
        buySuccessfull.text = "";
    }

    IEnumerator PlayStartCountDown()
    {
        GameObject go = Instantiate(startCountDownPrefab);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);

        Invoke("CreateSuperPacdot", 10f);
        Invoke("CreateMidPacdot", 15f);

        gamePanel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void OnEatPacdot(GameObject go)
    {
        nowEat++;
        score += 100;
        GhostMove.nowScore += 100;
        gold += 3;
        pacdotGos.Remove(go);
    }

    public void OnEatSuperPacdot()
    {
        score += 200;

        GhostMove.nowScore += 200;

        gold += 30;

        Invoke("CreateSuperPacdot", 10f);

        isSuperPacman = true;

        FreezeEnemy();

        StartCoroutine(RecoveryEnemy());
    }


    public void OnEatMidPacdot()
    {
        score += 150;

        GhostMove.nowScore += 150;

        gold += 20;

        Invoke("CreateMidPacdot", 15f);

        timer += 20;
    }

    IEnumerator RecoveryEnemy()
    {
        yield return new WaitForSeconds(3f);

        DisFreezeEnemy();

        isSuperPacman = false;
    }

    private void CreateSuperPacdot()
    {
        if (pacdotGos.Count < 5)
        {
            return;
        }

        int tempIndex = Random.Range(0, pacdotGos.Count);

        pacdotGos[tempIndex].transform.localScale = new Vector3(7, 7, 7);

        pacdotGos[tempIndex].GetComponent<Pacdot>().isSuperPacdot = true;
    }


    private void CreateMidPacdot()
    {
        if (pacdotGos.Count < 5)
        {
            return;
        }

        int tempIndex = Random.Range(0, pacdotGos.Count);

        pacdotGos[tempIndex].transform.localScale = new Vector3(3, 3, 3);

        pacdotGos[tempIndex].GetComponent<Pacdot>().isMidPacdot = true;
    }

    private void FreezeEnemy()
    {
        blinky.GetComponent<GhostMove>().enabled = false;
        clyde.GetComponent<GhostMove>().enabled = false;
        inky.GetComponent<GhostMove>().enabled = false;
        pinky.GetComponent<GhostMove>().enabled = false;

        blinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        inky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
        pinky.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 0.7f);
    }


    private void DisFreezeEnemy()
    {
        blinky.GetComponent<GhostMove>().enabled = true;
        clyde.GetComponent<GhostMove>().enabled = true;
        inky.GetComponent<GhostMove>().enabled = true;
        pinky.GetComponent<GhostMove>().enabled = true;

        blinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        clyde.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        inky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        pinky.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }


    private void SetGameState(bool state)
    {
        pacman.GetComponent<PacmanMove>().enabled = state;
        blinky.GetComponent<GhostMove>().enabled = state;
        clyde.GetComponent<GhostMove>().enabled = state;
        inky.GetComponent<GhostMove>().enabled = state;
        pinky.GetComponent<GhostMove>().enabled = state;
    }


    private void startTime()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            time.GetComponent<Text>().text = timer.ToString("00");
        }

        else
        {
            GameManager.Instance.gamePanel.SetActive(false);
            Instantiate(GameManager.Instance.gameoverPrefab);
            FreezeEnemy();
            GameObject.Destroy(pacman);
            UICon.nowScore = score;
            finalScore.text = "Score：" + score;
            finalScore.color = new Color(1, 1, 1, 1);
            //Instantiate(GameManager.Instance.finallyScroe);
            Invoke("ReStart", 3f);
            SceneManager.LoadScene("Score");
        }
    }

    private void ReStart()
    {
        SceneManager.LoadScene(0);
    }

}
