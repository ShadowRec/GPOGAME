using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using static Cinemachine.DocumentationSortingAttribute;

public class LevelGenerator : MonoBehaviour
{

    public int LevelNumber;
    public int ApproximateRoomCount;
    public int Seed;
    
    public GameObject room_0;
    public GameObject room_1;
    public GameObject room_2;
    public GameObject room_3;
    public GameObject room_4;
    public GameObject room_5;
    [SerializeField]
    static public int[,] grid;


    // Start is called before the first frame update
    void Start()
    {
        LevelGrid level1 = new LevelGrid(LevelNumber, ApproximateRoomCount, Seed);
        grid = new int[level1.levelGrid.GetLength(0), level1.levelGrid.GetLength(1)];
        grid = level1.levelGrid;
        grid = Transponse(grid);
        for (int i = 0; i < level1.levelGrid.GetLength(0); i++) 
        {
            for (int j = 0; j < level1.levelGrid.GetLength(1); j++)
            {
                int currentRoom = level1.levelGrid[i,j];
               
                switch (currentRoom/10) 
                {
                    
                    case 2:

                         GameObject NewObject= (GameObject) Instantiate(room_0, new Vector3(i*22, 0, j * 22), Quaternion.Euler(new Vector3(0, 0, 0)));
                        NewObject.name = currentRoom.ToString();
                        RoomScript room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10==2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if(currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 3:
                        NewObject = (GameObject) Instantiate(room_0, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 180, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 4:
                        NewObject = (GameObject)Instantiate(room_0, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, -90, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }

                        break;
                    case 5:
                        NewObject = (GameObject)Instantiate(room_0, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 90, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 6:
                        NewObject = (GameObject)Instantiate(room_1, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 0, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 8:
                        NewObject = (GameObject)Instantiate(room_5, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 0, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 10:
                        NewObject = (GameObject)Instantiate(room_4, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 180, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 12:
                        NewObject = (GameObject)Instantiate(room_4, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 0, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 15:
                        NewObject = (GameObject)Instantiate(room_5, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 180, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 20:
                        NewObject = (GameObject)Instantiate(room_1, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 90, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 24:
                        NewObject = (GameObject)Instantiate(room_3, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 90, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 30:
                        NewObject = (GameObject)Instantiate(room_3, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, -90, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 40:
                        NewObject = (GameObject)Instantiate(room_3, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 180, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 60:
                        NewObject = (GameObject)Instantiate(room_3, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 0, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;
                    case 120:
                        NewObject = (GameObject)Instantiate(room_2, new Vector3(i * 22, 0, j * 22), Quaternion.Euler(new Vector3(0, 0, 0)));
                        NewObject.name = currentRoom.ToString();
                        room = NewObject.transform.GetChild(0).GetComponent<RoomScript>();
                        if (currentRoom % 10 == 2)
                        {
                            room.EnableShop();
                        }

                        if (currentRoom % 10 == 3)
                        {
                            room.EnableTreasure();
                        }

                        if (currentRoom % 10 == 0)
                        {
                            room.EnableEnemys();
                        }
                        break;


                }



            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    int[,] Transponse(int[,] list)
    {
        int tmp;
        for (int i = 0; i <list.GetLength(0); i++)
        {
            for (int j = 0; j < list.GetLength(1); j++)
            {
                tmp = list[i, j];
                list[i, j] = list[j, i];
                list[j, i] = tmp;
            }
        }
        return list;
    }

   
}
