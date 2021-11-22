using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public static int currentHealth;
    public float knockBackForce;
    public GameObject MinoDeadBody, MinoHead;
    private Rigidbody2D theRB;
    public static bool isBoss;
    public PhotonView PV;
    public Slider HealthBar;
    public GameObject EndScreen;
    void Start()
    {
        EndScreen = GameObject.FindGameObjectWithTag("EndScreen");
        theRB = GetComponent<Rigidbody2D>();
        PV = GetComponent<PhotonView>();
        currentHealth = maxHealth;
        HealthBar = GameObject.FindWithTag("BossHealthBar").GetComponent<Slider>();

    }
    /// <summary>
    /// If player 1 hits the enemy, change the health bar and set the new health
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        PV.RPC("HealthChange", RpcTarget.AllBuffered, damage);
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }
    /// <summary>
    /// network logic for health decrease
    /// </summary>
    /// <param name="damage"></param>
    [PunRPC]
    void HealthChange(int damage)
    {
        currentHealth -= damage;
        theRB.velocity = new Vector2(knockBackForce, 0f);
        HealthBar.value = currentHealth;
        Debug.Log(currentHealth);
    }

    void Die()
    {
        if (PV.IsMine)
        {

            if (PhotonNetwork.IsMasterClient)
            {

                //spawns a death animation and new gameobjects where the boss dies, as well as lets players pass through the wall.
                if (gameObject.tag == "Boss")
                {
                    GameObject mBody = PhotonNetwork.Instantiate(MinoDeadBody.name, transform.position, Quaternion.identity);
                    mBody.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

                    GameObject mHead = PhotonNetwork.Instantiate(MinoHead.name, transform.position, Quaternion.identity);
                    mHead.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
                    mHead.GetComponent<Rigidbody2D>().AddForce(transform.up * 12, ForceMode2D.Impulse);
                    if (mHead.transform.localScale == new Vector3(1, 1, 1))
                    {
                        mHead.GetComponent<Rigidbody2D>().AddForce(transform.right * 5, ForceMode2D.Impulse);

                    }
                    else if (mHead.transform.localScale == new Vector3(-1, 1, 1))
                    {
                        mHead.GetComponent<Rigidbody2D>().AddForce(transform.right * -5, ForceMode2D.Impulse);

                    }

                    BossWallBlock.instance.wallDisable();
                }
            }

            PhotonNetwork.Destroy(gameObject);
            PV.RPC("EndGame", RpcTarget.AllBuffered);
            Debug.Log("Enemy Died");

        }
    }
    [PunRPC]
    public void EndGame()
    {
        StartCoroutine(EndGameCo());
    }

    private IEnumerator EndGameCo()
    {
        yield return new WaitForSeconds(5);

        EndScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
