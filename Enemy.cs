using UnityEngine;
using System.Collections;

//Fiende ärver från MovingObject, vår basklass för objekt som kan röra sig, Player ärver också från detta.
public class Enemy : MovingObject
{
    public int playerDamage;                            


    private Animator animator;                             
    private Transform target;                          
    private bool skipMove;                               


    protected override void Start()
    {
        
        GameManage.instance.AddEnemyToList(this);

       
        animator = GetComponent<Animator>();

      
        target = GameObject.FindGameObjectWithTag("Player").transform;

        
        base.Start();
    }


    // Åsidosätt AttemptMove-funktionen för MovingObject för att inkludera funktioner som behövs för att Enemy ska hoppa över svängar.
    
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
       
        if (skipMove)
        {
            skipMove = false;
            return;

        }

        
        base.AttemptMove<T>(xDir, yDir);

       
        skipMove = true;
    }


    
    public void MoveEnemy()
    {
       
        int xDir = 0;
        int yDir = 0;

       
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)

           
            yDir = target.position.y > transform.position.y ? 1 : -1;

        else
          
            xDir = target.position.x > transform.position.x ? 1 : -1;

      
        AttemptMove<Player>(xDir, yDir);
    }


    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;

        hitPlayer.LoseFood(playerDamage);

        animator.SetTrigger("enemyAttack");

    }
}