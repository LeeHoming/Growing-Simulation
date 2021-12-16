using System.Collections;
using System.Collections.Generic;
//using System.Math;
using UnityEngine;

public class GameOfPlants : MonoBehaviour
{
    // Screen Size/ Block Num
    public static int SCR_Width = 31;

    public static int SCR_Height = 31;

    public LevelSystem Level;

    public TerrainFormer Terrain;

    //public static class PlantParameters
    //{
    public int SeedlingTime = 5;

    public int MaturingTime = 8;

    public int ReproducingTime = 5;

    public int DeadTime = 4;

    //}
    //public PlantParameters LiveParameters;
    //for random at BEGINNING
    public float randomlive = 10;

    //report alive number
    public int numberAlive = 0;

    //Update speed
    public float speed = 0.1f;

    private float timer = 0f;

    //radius counter
    public float NearbyRadius = 3f;

    //count round number
    public int round = 0;

    public int BlockCount = 0;

    public int CanLiveBlock = 0;

    PlantCell[,] grid = new PlantCell[SCR_Width, SCR_Height];

    //Vector2[,] Radius = new Vector2[SCR_Width, SCR_Height];
    void Start()
    {
        BlockCount = SCR_Width * SCR_Height;
        PlaceCells();
        CellData();
    }

    // Update is called once per frame
    void Update()
    {
        //timer for speed control, unit: Second
        if (timer >= speed)
        {
            timer = 0f;

            EveryPlant();
            numAlive();
            round++;

            //Protect first seed not die in 10 round
            if (round > 10)
            {
                RandomDeadWaker();
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void PlaceCells()
    {
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                float height = Terrain.grid[x + 5, y + 5].height;

                //Debug.Log (height);
                //Place Cell
                PlantCell Plantcell =
                    Instantiate(Resources.Load("PlantCell", typeof (PlantCell)),
                    new Vector3(x - (float) SCR_Width / 2,
                        height,
                        y - (float) SCR_Height / 2),
                    Quaternion.identity) as
                    PlantCell;
                grid[x, y] = Plantcell;
                grid[x, y].transform.SetParent(transform, true);

                //Random Active Test
                grid[x, y].HasPlant = false;
                grid[x, y].SeaLevel = height;

                if (height > -0.4f)
                {
                    CanLiveBlock++;
                    grid[x, y].inWater = false;
                }
                else
                {
                    grid[x, y].inWater = true;
                }
            }
        }

        grid[15, 15].HasPlant = true;
        grid[15, 15].liveLength++;
        grid[15, 15].animator.SetTrigger("GrowSeed");
    }

    //giving data
    void CellData()
    {
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                grid[x, y].T0 = SeedlingTime;
                grid[x, y].T1 = MaturingTime;
                grid[x, y].T2 = ReproducingTime;
                grid[x, y].T3 = DeadTime;
                CrowdRadius (x, y);
                SeedRadius (x, y);
            }
        }
    }

    void EveryPlant()
    {
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                //grid[x, y].LiveLengthCount();
                grid[x, y].StatusUpdate();

                //grid[x, y].ColorUpdate();
                if (grid[x, y].PlantStatus == 1)
                {
                    //grid[x, y].CrowdNum = countAliveNeighbor(x, y);
                    int numNeigh = 0;
                    for (int i = 0; i < grid[x, y].CrowdPlants.Count; i++)
                    {
                        Vector2 PlantAB =
                            new Vector2(grid[x, y].CrowdPlants[i].x,
                                grid[x, y].CrowdPlants[i].y);
                        if (grid[(int) PlantAB.x, (int) PlantAB.y].HasPlant)
                        {
                            numNeigh++;
                            grid[(int) PlantAB.x, (int) PlantAB.y].ClosestUnit =
                                numNeigh;
                        }
                    }
                    grid[x, y].CrowdNum = numNeigh;
                }

                if (grid[x, y].PlantStatus == 2)
                {
                    Vector2 Alive =
                        grid[x, y]
                            .SeedPlants[Random
                                .Range(0, grid[x, y].SeedPlants.Count)];
                    if (grid[(int) Alive.x, (int) Alive.y].HasPlant == false)
                    {
                        grid[(int) Alive.x, (int) Alive.y].HasPlant = true;
                        grid[(int) Alive.x, (int) Alive.y].liveLength++;
                    }

                    //Debug.Log (Alive);
                }
                grid[x, y].DeadRateCourter();

                //grid[x, y].TestActivate();
            }
        }
    }

    public void RandomDeadWaker()
    {
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                grid[x, y].randomDead();
            }
        }
    }

    public void CrowdRadius(int x, int y)
    {
        for (int a = 0; a < SCR_Height; a++)
        {
            for (int b = 0; b < SCR_Width; b++)
            {
                if (!(x == a && y == b))
                {
                    if (
                        Mathf.Pow(((float) a - x), 2f) +
                        Mathf.Pow(((float) b - y), 2f) <
                        Mathf.Pow(1.5f, 2f)
                    )
                    {
                        //Vector2 add = new Vector2(a,b);
                        grid[x, y].CrowdPlants.Add(new Vector2(a, b));
                    }
                }
            }
        }
    }

    public void SeedRadius(int x, int y)
    {
        for (int a = 0; a < SCR_Height; a++)
        {
            for (int b = 0; b < SCR_Width; b++)
            {
                if (!(x == a && y == b))
                {
                    if (
                        Mathf.Pow(((float) a - x), 2f) +
                        Mathf.Pow(((float) b - y), 2f) <
                        Mathf.Pow(NearbyRadius, 2f)
                    )
                    {
                        //Vector2 add = new Vector2(a,b);
                        grid[x, y].SeedPlants.Add(new Vector2(a, b));
                    }
                }
            }
        }
    }

    // public int countAliveNeighbor(int x, int y)
    // {
    //     int numNeigh;
    //     for (int a = 0; a < SCR_Height; a++)
    //     {
    //         for (int b = 0; b < SCR_Width; b++)
    //         {
    //             if (grid[a, b].HasPlant)
    //             {
    //                 numNeigh++;
    //             }
    //         }
    //     }
    //     return numNeigh;
    // }
    bool RandomAliveCell()
    {
        int rand = UnityEngine.Random.Range(0, 100);

        if (rand > randomlive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void numAlive()
    {
        int number = 0;
        for (int y = 0; y < SCR_Height; y++)
        {
            for (int x = 0; x < SCR_Width; x++)
            {
                if (grid[x, y].HasPlant)
                {
                    number++;
                }
            }
        }
        numberAlive = number;
    }
}

// public class Nearby
// {
//     private int nearX;

//     private int nearY;

//     public int NearX
//     {
//         get
//         {
//             return nearX;
//         }
//     }

//     public int NearY
//     {
//         get
//         {
//             return nearY;
//         }
//     }

//     public Nearby(int NearX, int NearY)
//     {
//         this.nearX = NearX;
//         this.nearY = NearY;
//     }
//}
