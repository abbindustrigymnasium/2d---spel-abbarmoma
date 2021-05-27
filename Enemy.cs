using UnityEngine;
using System.Collections;

//Fiende �rver fr�n MovingObject, v�r basklass f�r objekt som kan r�ra sig, Player �rver ocks� fr�n detta.
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


    // �sidos�tt AttemptMove-funktionen f�r MovingObject f�r att inkludera funktioner som beh�vs f�r att Enemy ska hoppa �ver sv�ngar.
    
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