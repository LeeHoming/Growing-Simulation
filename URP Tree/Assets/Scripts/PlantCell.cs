using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCell : MonoBehaviour
{
    //Plant alive or not
    public bool HasPlant = false;

    //0-Seedling 1-Maturing 2-Reproducing 3-Dead 4-Empty
    public int PlantStatus = 4;

    public Animator animator;

    //plants' living condition
    public float DeadRate = 2f;

    public float WaterRate = 100f;

    //how long has it live
    public int liveLength = 0;

    public int listLenght = 0;

    public bool inWater = false;

    //create a arraylist for storaging nearby cell
    public List<Vector2> CrowdPlants = new List<Vector2>();

    //number of seeding neighbour
    public int ClosestUnit;

    public float CrowdFactor = 5f;

    //number of too close neighbour
    public int CrowdNum;

    public float SeaLevel;

    public List<Vector2> SeedPlants = new List<Vector2>();

    //Period of live from GOP
    public int

            T0,
            T1,
            T2,
            T3;

    //------------------------------------------//
    private void Awake()
    {
        animator = GetComponent<Animator>();
        transform.rotation =
            Quaternion
                .Euler(new Vector3(0, UnityEngine.Random.Range(0, 360), 0));
        transform.localScale =
            new Vector3(1.3f, UnityEngine.Random.Range(1.1f, 1.5f), 1.3f);
    }

    private void Start()
    {
        // T0 = game.GetComponent<GameOfPlants>().LiveParameters.SeedlingTime;
        // T1 = game.GetComponent<GameOfPlants>().LiveParameters.MaturingTime;
        // T2 = game.GetComponent<GameOfPlants>().LiveParameters.ReproducingTime;
        listLenght = SeedPlants.Count;
        ClosestUnit = CrowdPlants.Count;
    }

    public void Update()
    {
        //ColorUpdate();
        // listLenght = SeedPlants.Count;
        // ClosestUnit = CrowdPlants.Count;
    }

    //------------------------------------------//
    //wake up by GOP
    public void SetAlive(bool alive)
    {
        HasPlant = alive;
    }

    //Plants clock
    public void LiveLengthCount()
    {
        if (HasPlant)
        {
            liveLength++;
        }
        else
        {
            liveLength = 0;
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    //Accidental die
    public void randomDead()
    {
        float rand = UnityEngine.Random.Range(0f, 100f);
        if (rand <= DeadRate)
        {
            HasPlant = false;
            liveLength = 0;
            animator.SetTrigger("ClearPlant");
        }
    }

    //Dead calculation
    public void DeadRateCourter()
    {
        if (PlantStatus == 0)
        {
            //Water is important in seedling age
            DeadRate = 5f * (WaterRate / 100f);

            if (inWater)
            {
                DeadRate = 100f;
            }
        }

        if (PlantStatus == 1)
        {
            //Distance with nearby plants matters
            DeadRate = 2f + CrowdNum * 3f;
        }

        if (PlantStatus == 2)
        {
            DeadRate = 0;
        }
    }

    //Dynamic dead rate
    public void StatusUpdate()
    {
        if (liveLength > 0 && liveLength <= T0)
        {
            PlantStatus = 0;
            liveLength++;
            animator.SetTrigger("GrowSeed");
        }
        else if (liveLength > T0 && liveLength <= T0 + T1)
        {
            PlantStatus = 1;
            liveLength++;
            animator.SetTrigger("GrowYoung");
        }
        else if (liveLength > T0 + T1 && liveLength <= T0 + T1 + T2)
        {
            PlantStatus = 2;
            liveLength++;
            animator.SetTrigger("GrowFruit");
        }
        else if (liveLength > T0 + T1 + T2 && liveLength <= T0 + T1 + T2 + T3)
        {
            PlantStatus = 3;
            liveLength++;
            animator.SetTrigger("GrowDead");
        }
        else
        {
            HasPlant = false;
            liveLength = 0;
            animator.SetTrigger("ClearPlant");
        }
    }

    // public void ColorUpdate()
    // {
    //     if (HasPlant)
    //     {
    //         GetComponentInChildren<MeshRenderer>().enabled = true;
    //         if (PlantStatus == 0)
    //         {
    //             GetComponentInChildren<TreeRender>().numModel = 0;
    //         }
    //         if (PlantStatus == 1)
    //         {
    //             GetComponentInChildren<TreeRender>().numModel = 1;
    //         }
    //         if (PlantStatus == 2)
    //         {
    //             GetComponentInChildren<TreeRender>().numModel = 2;
    //         }
    //         if (PlantStatus == 3)
    //         {
    //             GetComponentInChildren<TreeRender>().numModel = 3;
    //         }
    //     }
    //     else
    //     {
    //         //TargetColor = C4;
    //         GetComponentInChildren<MeshRenderer>().enabled = false;
    //     }
    // }

    // public void TestActivate()
    // {
    //     if (!HasPlant)
    //     {
    //         int rand = UnityEngine.Random.Range(0, 100);

    //         if (rand > 96)
    //         {
    //             HasPlant = true;
    //             liveLength++;
    //         }
    //         else
    //         {
    //             HasPlant = false;
    //         }
    //     }
    // }
}
