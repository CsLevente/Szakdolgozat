using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifetime;
    public bool isEnemyBullet = false;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;

    void Start()
    {
        if(!isEnemyBullet){
        StartCoroutine(EndDelay());
        transform.localScale = new Vector2(GameController.BulletSize,GameController.BulletSize);
        }
        
    }

    void Update()
    {
        if(isEnemyBullet){
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
            if(curPos == lastPos){
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
    }

    public void GetPlayer(Transform player){
        playerPos = player.position;
    }
    public void GetPos(Vector2 pos){
        playerPos = pos;
    }

    IEnumerator EndDelay(){
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag=="Enemy" && !isEnemyBullet){
            col.gameObject.GetComponent<EnemyController>().DamageEnemy(1);
            Destroy(gameObject);
        }
        if(col.tag=="Door" && !isEnemyBullet){
            col.gameObject.GetComponent<Door>().Death();
            Destroy(gameObject);
        }

        if(col.tag == "Player" && isEnemyBullet){
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}
