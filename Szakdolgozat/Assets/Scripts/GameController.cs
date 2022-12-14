using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public  Text healthText;

    public OverScreen gameOverScreen;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if(Health<=0){
            GameOver();
        }
    }

    public static void DamagePlayer(int damage){
        health -= damage;

        if(Health <= 0){
            KillPlayer();
        }else{
            FindObjectOfType<SettingManager>().Play("player_damaged");
        }
    }

    public static void HealPlayer(float healAmount){
        health = Mathf.Min(maxHealth, health + healAmount);
        if(healAmount!=0){
            FindObjectOfType<SettingManager>().Play("can_open");
            Debug.Log("can_open played" );
        }
    }

    public static void MoveSpeedChange(float speed){
        moveSpeed += speed;
        if(speed!=0){
            FindObjectOfType<SettingManager>().Play("item_pickup");
            Debug.Log("item_pickup played" );
        }
        
    }
    public static void FireRateChange(float rate){
        fireRate -= rate;
        if(rate!=0){
            FindObjectOfType<SettingManager>().Play("item_pickup");
            Debug.Log("item_pickup played" );
        }
    }
    public static void BulletSizeChange(float size){
        bulletSize += size;
        if(size!=0){
            FindObjectOfType<SettingManager>().Play("item_pickup");
            Debug.Log("item_pickup played" );
        }
    }
    public static void ResetStats(){
        health = 6;
        moveSpeed = 5f;
        fireRate = 0.5f;
        bulletSize = 0.5f;
    }

    private static void KillPlayer(){
        FindObjectOfType<SettingManager>().Play("player_dead");
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
    public void GameOver(){
        gameOverScreen.Setup();
    }
}
