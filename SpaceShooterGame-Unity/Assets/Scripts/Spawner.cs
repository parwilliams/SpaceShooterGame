using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float MaxRadius = 1f;
    public float Interval = 5f;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform spawnPoint4;
    public GameObject ObjToSpawn = null;
    private Transform Origin = null;
    [SerializeField] private Vector3 SpawnPos3D;
    private Transform[] spawnPositions;
    //private static Random rnd = new Random();

    private int randInt;

    void Awake() {
        Origin = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start() {
        InvokeRepeating("Spawn", 0f, Interval);
    }

    void Spawn() {

        if (Origin == null)
            return;



        spawnPositions = new Transform[] { spawnPoint1, spawnPoint2, spawnPoint3, spawnPoint4 };
        randInt = Random.Range(0, 4);

        // SpawnPos3D = Origin.position + Random.onUnitSphere * MaxRadius;
        //Vector2 SpawnPos = new Vector2(SpawnPos3D.y, SpawnPos3D.x);
        //Vector2 SpawnPos = new Vector2(Random.RandomRange(-3f, 3.85f), Random.RandomRange(-7.75f, 7.75f));
        Vector2 SpawnPos = new Vector2(spawnPositions[randInt].position.x, spawnPositions[randInt].position.y);
        Instantiate(ObjToSpawn, SpawnPos, Quaternion.identity);
    }
}
