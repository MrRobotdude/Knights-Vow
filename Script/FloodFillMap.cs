using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFillMap : MonoBehaviour
{
    public static int sizeX = 10, sizeY = 15, trapRatio = 2;
    public GameObject floor;
    public GameObject[,] arrFloor = new GameObject[150, 150];


    public GameObject wall;
    public GameObject pintu;
    public GameObject[,] dinding1 = new GameObject[150, 150];
    public GameObject[,] dinding2 = new GameObject[150, 150];
    public int[,] isTrap = new int[150, 150];


    public static float respX, respY, respZ;
    bool mapValid = false;
    System.Random rand = new System.Random();
    int counter = 0;
    public GameObject lamp;
    public GameObject smoke;
    public GameObject portal;
    public GameObject portalSound;
    public AudioSource trapSound;

    void Start()
    {
        generateMainMaze(sizeX, sizeX, trapRatio);
        generateAll(sizeX, sizeX);
    }

    void OnTriggerEnter(Collider collider)
    {
        checkTrigger(sizeX, sizeY, collider);
    }

    void generateMainMaze(int x, int y, int ratio)
    {
        generateBasicMaze(x, y);
        while (mapValid == false)
        {
            int maxTrap = ((sizeX * sizeY) - (sizeY)) / ratio;
            deleteAllTrap();
            while (maxTrap != 0)
            {
                int iX = RandomNumber(1, sizeX);
                int iY = RandomNumber(0, sizeY);
                if (isTrap[iX, iY] == 0)
                {
                    isTrap[iX, iY] = 1;
                    maxTrap--;
                }
            }
            int iiX = sizeX;
            int iiY = RandomNumber(0, sizeY);
            isTrap[iiX, iiY] = 0;
            FloodFill(0, 0);
        }
        findTrapError();
        if (counter > 0)
        {
            do
            {
                int iX = RandomNumber(1, sizeX);
                int iY = RandomNumber(0, sizeY);
                if (isTrap[iX, iY] == 1)
                {
                    isTrap[iX, iY] = 0;
                    counter--;
                }
                if (counter == 0)
                {
                    restoreStatus();
                    FloodFill(0, 0);
                    findTrapError();
                }
            } while (counter > 0);
        }
    }

    void generateBasicMaze(int x, int y)
    {
        for (int i = 0; i < x + 2; i++)
        {
            for (int j = 0; j < y; j++)
            {
                arrFloor[i, j] = Instantiate(floor);
                arrFloor[i, j].transform.position = new Vector3(4f * j, 0, 4f * i);
                if (i == x)
                    isTrap[i, j] = 1;

                else
                    isTrap[i, j] = 0;
            }
        }
    }

    void generateAll(int x, int y)
    {
        for (int i = 0; i < x + 2; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (i == x + 1 && j == (y) / 2)
                {
                    portal.transform.position = new Vector3(4f * j, 5, 4f * i);
                    portalSound.transform.position = new Vector3(4f * j, 5, 4f * i);
                    Instantiate(portal);
                    Instantiate(portalSound);
                }
                if (j == y / 2 && i % 3 == 0)
                {
                    lamp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    lamp.transform.position = new Vector3(4f * j, 6, 4f * i);
                    lamp.transform.localScale = new Vector3(3, 3, 3);
                    Instantiate(lamp);
                }
                if (j == 0)
                {
                    dinding1[i, j] = Instantiate(wall);
                    dinding1[i, j].transform.position = new Vector3(4f * j - 2.04f, 0, 4f * i);

                    dinding2[i, j] = Instantiate(wall);
                    dinding2[i, j].transform.position = new Vector3(4f * j - 2.04f, 4f, 4f * i);
                }
                if (j == y - 1)
                {
                    dinding1[i, j] = Instantiate(wall);
                    dinding1[i, j].transform.position = new Vector3(4f * j - 2.04f + 4f, 0, 4f * i);

                    dinding2[i, j] = Instantiate(wall);
                    dinding2[i, j].transform.position = new Vector3(4f * j - 2.04f + 4f, 4f, 4f * i);
                }
                if (i == 0)
                {
                    if (j == y / 2)
                    {
                        transform.position = new Vector3(4f * j, 0, 4f * i);
                        dinding1[i, j] = Instantiate(pintu);
                        dinding1[i, j].transform.rotation = Quaternion.Euler(0, 90, 0);
                        dinding1[i, j].transform.position = new Vector3(4f * j, 0, 4f * i - 2f);
                        respX = 4f * j;
                        respY = 0;
                        respZ = 4f * i;
                    }
                    else
                    {
                        dinding1[i, j] = Instantiate(wall);
                        dinding1[i, j].transform.rotation = Quaternion.Euler(0, 90, 0);
                        dinding1[i, j].transform.position = new Vector3(4f * j, 0, 4f * i - 2f);
                    }

                    dinding2[i, j] = Instantiate(wall);
                    dinding2[i, j].transform.rotation = Quaternion.Euler(0, 90, 0);
                    dinding2[i, j].transform.position = new Vector3(4f * j, 4f, 4f * i - 2f);
                }
                if (i == x + 1)
                {
                    dinding1[i, j] = Instantiate(wall);
                    dinding1[i, j].transform.rotation = Quaternion.Euler(0, 90, 0);
                    dinding1[i, j].transform.position = new Vector3(4f * j, 0, 4f * i - 2.04f + 4f);

                    dinding2[i, j] = Instantiate(wall);
                    dinding2[i, j].transform.rotation = Quaternion.Euler(0, 90, 0);
                    dinding2[i, j].transform.position = new Vector3(4f * j, 4f, 4f * i - 2.04f + 4f);
                }
                floor.transform.position = new Vector3(4f * j, 8, 4f * i);
                Instantiate(floor);
            }
        }
    }

    private int RandomNumber(int min, int max)
    {
        int result = rand.Next(min, max);
        return result;
    }

    void findTrapError()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (isTrap[i, j] == 0)
                {
                    isTrap[i, j] = 1;
                    counter++;
                }
            }
        }
    }

    void deleteAllTrap()
    {
        for (int i = 0; i < sizeX + 2; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (i == sizeX)
                {
                    isTrap[i, j] = 1; // trap
                }
                else
                {
                    isTrap[i, j] = 0; // not trap
                }
            }
        }
    }

    void restoreStatus()
    {
        for (int i = 0; i < sizeX + 2; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (isTrap[i, j] == 2)
                {
                    isTrap[i, j] = 0;
                }
            }
        }
    }

    void FloodFill(int x, int y)
    {
        if (x < 0 || x > sizeX + 1 || y < 0 || y > sizeY - 1) return;
        if (isTrap[x, y] != 0) return;
        if (x == sizeX + 1)
        {
            mapValid = true;
            return;
        }

        isTrap[x, y] = 2;
        FloodFill(x + 1, y);
        FloodFill(x - 1, y);
        FloodFill(x, y + 1);
        FloodFill(x, y - 1);
    }

    void checkTrigger(int x, int y, Collider collider)
    {
        for (int i = 0; i < x + 2; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (arrFloor[i, j] == collider.gameObject)
                {

                    if (isTrap[i, j] == 1)
                    {
                        smoke.transform.position = arrFloor[i, j].transform.position;
                        Instantiate(smoke);
                        dropFloor(arrFloor[i, j]);
                    }
                }
            }
        }
    }

    IEnumerator delayTrap(GameObject dropFloor)
    {
        trapSound.Play();
        yield return new WaitForSeconds(0.6f);
        dropFloor.GetComponent<Rigidbody>().useGravity = true;
        dropFloor.GetComponent<Rigidbody>().isKinematic = false;
    }

    void dropFloor(GameObject dropFloor)
    {
        StartCoroutine(delayTrap(dropFloor));
    }

    void createLamp(int x, int y)
    {
        for (int i = 0; i < x + 2; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (j == y / 2 && i % 3 == 0)
                {
                    lamp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    lamp.transform.position = new Vector3(4f * j, 6, 4f * i);
                    lamp.transform.localScale = new Vector3(3, 3, 3);
                    Instantiate(lamp);
                }
            }
        }
    }


}
