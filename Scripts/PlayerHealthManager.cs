
using System;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Random=UnityEngine.Random;


public class PlayerHealthManager : MonoBehaviour
{
    public HealthBarScript HealthBar;
    public float MaxHealth = 100;
    [SerializeField]
    public float currentHealth;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PlayerController>().view;
        //if(view.IsMine){this.enabled = false; return;}
        HealthBar.slider = FindObjectOfType<Canvas>().transform.Find("HealthBar").GetComponent<Slider>();
        currentHealth = MaxHealth;
        HealthBar.SetMaxHealth(MaxHealth);
        HealthBar.SetHealth(currentHealth);
    }

    [PunRPC]
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("current health: " + currentHealth);
        this.currentHealth -= other.GetComponent<ParticleCollisionInstance>().Damage;

        Debug.Log("My health is : " + currentHealth);
        HealthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            //PhotonNetwork.Destroy(transform.parent.gameObject);
            var playerSpawn = FindObjectOfType<SpawnPlayers>();
            var newPos = playerSpawn.spawnPoints[Random.Range(0, playerSpawn.spawnPoints.Count)].position;
            gameObject.transform.parent.gameObject.transform.position = newPos;
            //PhotonNetwork.Instantiate(playerSpawn.playerPrefab.name, playerSpawn.spawnPoints[Random.Range(0, playerSpawn.spawnPoints.Count)].position, Quaternion.identity);
        }
    }
}
