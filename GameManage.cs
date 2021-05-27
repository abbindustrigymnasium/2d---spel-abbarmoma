using UnityEngine;
using System.Collections;


using System.Collections.Generic;        

public class GameManage : MonoBehaviour
{
    public float levelStartDelay = 2f;                   
    public float turnDelay = 0.1f;                           
    public int playerFoodPoints = 100;                        
    public static GameManage instance = null;               
    [HideInInspector] public bool playersTurn = true;       


    private BoardManager boardScript;                       
    private int level = 1;                                  
    private List<Enemy> enemies;                           
    private bool enemiesMoving;                              
    private GameObject levelImage;



    void Awake()
    {
        if (instance == null)

            instance = this;

        else if (instance != this)

            
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        enemies = new List<Enemy>();

        boardScript = GetComponent<BoardManager>();

        InitGame();
    }

    void OnLevelWasLoaded(int index)
    {
        level++;
        InitGame();
    }

    void InitGame()
    {

        enemies.Clear();

        boardScript.SetupScene(level);

    }


    void Update()
    {
        if (playersTurn || enemiesMoving)

            return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }


    public void GameOver()
    {

        levelImage.SetActive(true);

        enabled = false;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            //Call the MoveEnemy function of Enemy at index i in the enemies List.
            enemies[i].MoveEnemy();

            //Wait for Enemy's moveTime before moving next Enemy, 
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        //Once Enemies are done moving, set playersTurn to true so player can move.
        playersTurn = true;

        //Enemies are done moving, set enemiesMoving to false.
        enemiesMoving = false;
    }
}