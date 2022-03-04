using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject DeathParticlesPrefab = null;
    [SerializeField] private HealthBar healthBar;

    public bool ShouldDestroyOnDeath = true;

    [SerializeField] private float _HealthPoints = 100f;

    public float HealthPoints{
        get{
            return _HealthPoints;
        }

        set {
            _HealthPoints = value;

            if(HealthPoints <= 0)
            {
                SendMessage("Die", SendMessageOptions.DontRequireReceiver);

                if(DeathParticlesPrefab != null)
                {
                    Instantiate(DeathParticlesPrefab, transform.position, transform.rotation);
                }

                if (ShouldDestroyOnDeath)
                {
                    if (this.CompareTag("Player"))
                    {
                        FindObjectOfType<AudioManager>().Play("PlayerDeath");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("EnemyDeath");
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    void Update(){
        /*
        if(Input.GetKeyDown(KeyCode.Space)){
            HealthPoints = 0;
        }
        */
    }
}
