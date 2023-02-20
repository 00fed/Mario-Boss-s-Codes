using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public Player player;
    public PlayerMovement playermovement;
    public Text coiner;
    public GameObject coinerr;
    public int coin;
    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public GameObject supermario;
    public GameObject invisible;
    public GameObject start;
    public GameObject mushroom;
    public GameObject Mario;
    public GameObject world11;
    public GameObject X;
    public Text live;
    public GameObject Live;
    public Camera came;
    public Text scorer;
    public Text timer;
    public GameObject timeup;
    public GameObject BlackScreen;
    public GameObject timee;
    public GameObject scoree;
    public GameObject gameover;
    public GameObject m;
    public GameObject x;
    public GameObject c;
    public GameObject World;
    public GameObject t;
    public GameObject quit;
    public GameObject goa;
    public GameObject nine;
    public Transform mushroom1;
    public Transform mushroom2;
    public Transform pl;
    public Transform nin;
    public float time = 0;
    public float stime = 464;
    public bool tpme => time < 100;
    public bool tame => time < 99;
    public int score;
    public float score2;
    
    
   

     
    public void Awake() {
      if (Instance != null)
        {DestroyImmediate(gameObject);}
        else { Instance = this;
        DontDestroyOnLoad(gameObject);
        }
        goa.SetActive(false);
        Pause();
        
        
        
     }

     private void OnDestroy() {
        if (Instance == this) 
        {
            Instance = null;
        }
     }

     public void Play()
     {
      
      NewGame();
      
      


     }
    
    

     private void Update() {
       time = time - 1 * Time.deltaTime;
       timer.text = time.ToString("0");
       if (time < 1)
       {
        Timeup();
       }
       
       
     }
     public void Pause() 
    {   
        
        
        supermario.SetActive(true);
        start.SetActive(true);
        mushroom.SetActive(true);
        quit.SetActive(true);
        coin = 00;
        playermovement.enabled = false;
        Time.timeScale = 0f;
        time = stime;
        t.SetActive(false);
    }

     public void AddCoin()
     {
       coin++;
       coiner.text = coin.ToString("00");

       score = score + 200;
       scorer.text = score.ToString("000000");

     }

     public void AddLife()
     {
       lives++;
     }

     

     public void NewGame()
     {
      
       lives = 3;
       StartCoroutine(a());
       Application.targetFrameRate =60;
       LoadLevel(1, 1);
     }
    private void LoadLevel(int world, int stage)
    {
      this.world = world;
      this.stage = stage;
      
      

      SceneManager.LoadScene($"{world}-{stage}");
    }
    public void NextLevel() 
    {
        LoadLevel(world, stage + 1);
    }
    public void ResetLevel(float delay) {
        Invoke(nameof(ResetLevel), delay);
    }
    public void ResetLevel() {
        lives--;
        live.text = lives.ToString();
        

        if (lives > 0) {
            
            StartCoroutine(a());
            LoadLevel(world, stage);
            
        }
        else {GameOver();}
    }
    
    public IEnumerator a()
    {
      Lives();
      goa.SetActive(false);

      yield return new WaitForSecondsRealtime(4f);
       
       
       X.SetActive(false);
        Mario.SetActive(false);
        world11.SetActive(false);
        Live.SetActive(false);
        BlackScreen.SetActive(false);
        start.SetActive(false);
        mushroom.SetActive(false);
        supermario.SetActive(false);
        quit.SetActive(false);
        Time.timeScale = 1f;
        t.SetActive(true);
        coin = 0;
        coiner.text = coin.ToString("00");
        score = 0;
        scorer.text = score.ToString("000000");
        time = stime;
       
    }
    public void Death()
      {
        
        LoadLevel(world, stage);
        Awake();
      }
    public void Lives()
    {   
        X.SetActive(true);
        Mario.SetActive(true);
        world11.SetActive(true);
        Live.SetActive(true);
        BlackScreen.SetActive(true);
        start.SetActive(false);
        mushroom.SetActive(false);
        supermario.SetActive(false);
        quit.SetActive(false);
        Time.timeScale = 0f;
        goa.SetActive(false);
        t.SetActive(false);
    }
    
    private void GameOver()
    {   
        gameover.SetActive(true);
        X.SetActive(false);
        Mario.SetActive(false);
        world11.SetActive(false);
        Live.SetActive(false);
        supermario.SetActive(false);
        coinerr.SetActive(false);
        timee.SetActive(false);
        scoree.SetActive(false);
        start.SetActive(false);
        quit.SetActive(false);
        mushroom.SetActive(false);
        m.SetActive(false);
        x.SetActive(false);
        c.SetActive(false);
        t.SetActive(false);
        World.SetActive(false);
        BlackScreen.SetActive(true);
        Time.timeScale = 0f;
        goa.SetActive(true);
        t.SetActive(false);


        

    }

    private void Timeup()
    {   
        gameover.SetActive(false);
        X.SetActive(false);
        Mario.SetActive(false);
        world11.SetActive(false);
        Live.SetActive(false);
        supermario.SetActive(false);
        coinerr.SetActive(false);
        timee.SetActive(false);
        scoree.SetActive(false);
        start.SetActive(false);
        quit.SetActive(false);
        mushroom.SetActive(false);
        m.SetActive(false);
        x.SetActive(false);
        c.SetActive(false);
        t.SetActive(false);
        World.SetActive(false);
        BlackScreen.SetActive(true);
        timeup.SetActive(true);
        Time.timeScale = 0f;
        t.SetActive(false);
    }

    public void Mushroom()
    {
      if(mushroom != null)

      {Instantiate(mushroom, mushroom1.position, mushroom1.rotation);}
    }
    public void Mushroom2()
    {
      if(mushroom != null)

      {Instantiate(mushroom, mushroom2.position, mushroom2.rotation);}
    }
    
    public void Castle()
    {

      score = score + 100;
       scorer.text = score.ToString("000000"); 
    }
    public void Fifty()
    {
      score = score + 50;
      scorer.text = score.ToString("000000");
    }
    public void Hundred()
    {
      score = score + 000100;
      scorer.text = score.ToString("000000");

    }
    public void TwoHundred()
    {
      score = score + 000200;
      scorer.text = score.ToString("000000");

    }
    public void FourHundred()
    {
      score = score + 000400;
      scorer.text = score.ToString("000000");

    }
    public void FiveHundred()
    {
      score = score + 000500;
      scorer.text = score.ToString("000000");

    }
    public void Thousand()
    {
      score = score + 001000;
      scorer.text = score.ToString("000000");


    }

    public void Quit()
    {
      Application.Quit();
      Debug.Log("Quit");
    }

    
}
