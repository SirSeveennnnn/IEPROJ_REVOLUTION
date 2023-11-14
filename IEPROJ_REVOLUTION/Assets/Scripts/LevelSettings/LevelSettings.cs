using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public AudioClip levelClip;

    [Header("Song BPM")]
    public int beatsPerMinute;

    [Header("Level Creator Settings")]
    public int numberOfRows = 5;
    public float laneDistance = 1;

    [Space(10)]
    public GameManager gameManager;
    

    [Space(10)]
    public string PathToFile;
    public GameObject GroundPrefab;
    public GameObject ObstaclePrefab;
    public GameObject MoveLeftBlock;
    public GameObject MoveRightBlock;
    public GameObject ForceLeft;
    public GameObject ForceRight;
    public GameObject portalHandler;

    [Header("Neon Path Settings")]
    public GameObject NeonPathPrefab;
    public float neonPathY = 0.52f;
    public float neonPathWidth = 0.7f;
    public float neonPathlengthTrim = 0.2f;

    [Space(10)]
    public List<int> dataList;
    public List<GameObject> objectList;

    //Other Settings
    private int portalCounter = 0;
    private PortalHandler tempPortalHandler;
    

    
    public void CreateLevel()
    {
        Debug.Log("Create Level!");
        ReadCsvFile();
        GenerateLevel();
    }

    public void DeleteLevels()
    {
        Debug.Log("Delete Lists");
        dataList.Clear();
        for (int i = 0; i < objectList.Count; i++)
        {
            DestroyImmediate(objectList[i]);
        }
        objectList.Clear();
    }

    private void ReadCsvFile()
    {
        StreamReader reader = new StreamReader(PathToFile);
        bool endOfFile = false;
        while (!endOfFile)
        {
            string dataString = reader.ReadLine();
            if (dataString == null)
            {
                endOfFile = true;
                break;
            }

            var dataValues = dataString.Split(',');
            foreach (string value in dataValues)
            {
                dataList.Add(int.Parse(value));
            }
        }

        reader.Close();
    }

    private void GenerateLevel()
    {
        
        int row = 0, col = 0;
      

        Vector2 neonPathStart = new Vector2();
        Vector2 neonPathEnd = new Vector2();

        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i] == 1)
            {
                //Create Obstacle at Row, Col
                Vector3 position = new Vector3(row, 1, col);
                //PrefabUtility.InstantiatePrefab(ObstaclePrefab, position);
                GameObject clone = Instantiate(ObstaclePrefab, position, Quaternion.identity, this.transform);
                objectList.Add(clone);
                
            }
            else if (dataList[i] == 2)
            {
                if (neonPathStart == Vector2.zero)
                {
                    neonPathStart = new Vector2(row, col);
                }
                else 
                {
                    neonPathEnd = new Vector2(row, col);

                    Vector3 position = new Vector3(neonPathStart.x, neonPathY, (neonPathStart.y + neonPathEnd.y) / 2);
                    GameObject clone = Instantiate(NeonPathPrefab, position, NeonPathPrefab.transform.rotation, this.transform);

                    SpriteRenderer sprite = clone.GetComponent<SpriteRenderer>();
                    sprite.size = new Vector2(neonPathWidth, Mathf.Abs(neonPathStart.y - neonPathEnd.y) - neonPathlengthTrim + 1f);

                    BoxCollider collider = clone.AddComponent<BoxCollider>();
                    collider.isTrigger = true;
                    collider.size = new Vector3(collider.size.x, collider.size.y, 1);
                    collider.center = new Vector3(collider.center.x, collider.center.y, -0.5f);


                    neonPathStart = Vector2.zero;
                    neonPathEnd = Vector2.zero;

                    objectList.Add(clone);
                }
                
            }
            else if (dataList[i] == 3)
            {

                Vector3 position = new Vector3(row, neonPathY, col);

                GameObject clone = Instantiate(NeonPathPrefab, position, NeonPathPrefab.transform.rotation, this.transform);

                SpriteRenderer sprite = clone.GetComponent<SpriteRenderer>();
                sprite.size = new Vector2(neonPathWidth, neonPathWidth);

                BoxCollider collider = clone.AddComponent<BoxCollider>();
                collider.isTrigger = true;
                collider.size = new Vector3(collider.size.x, collider.size.y, 1);
                collider.center = new Vector3(collider.center.x, collider.center.y, -0.5f);

                objectList.Add(clone);
            }
            else if (dataList[i] == 4)
            {
                //Create Obstacle at Row, Col
                Vector3 position = new Vector3(row, 1, col);
                //PrefabUtility.InstantiatePrefab(ObstaclePrefab, position);
                GameObject clone = Instantiate(MoveLeftBlock, position, MoveLeftBlock.transform.rotation, this.transform);
                objectList.Add(clone);
                
            }
            else if (dataList[i] == 5)
            {
                //Create Obstacle at Row, Col
                Vector3 position = new Vector3(row, 1, col);
                //PrefabUtility.InstantiatePrefab(ObstaclePrefab, position);
                GameObject clone = Instantiate(MoveRightBlock, position, MoveRightBlock.transform.rotation, this.transform);
                objectList.Add(clone);
            }
            else if (dataList[i] == 6)
            {
                //Create Obstacle at Row, Col
                Vector3 position = new Vector3(row, neonPathY, col);
                //PrefabUtility.InstantiatePrefab(ObstaclePrefab, position);
                GameObject clone = Instantiate(ForceLeft, position, ForceLeft.transform.rotation, this.transform);
                objectList.Add(clone);
            }
            else if (dataList[i] == 7)
            {
                //Create Obstacle at Row, Col
                Vector3 position = new Vector3(row, neonPathY, col);
                //PrefabUtility.InstantiatePrefab(ObstaclePrefab, position);
                GameObject clone = Instantiate(ForceRight, position, ForceRight.transform.rotation, this.transform);
                objectList.Add(clone);
            }
            else if (dataList[i] == 8)
            {
                Vector3 position = new Vector3(row, 1, col);

                if (portalCounter == 0)
                {
                    GameObject clone = Instantiate(portalHandler, position, portalHandler.transform.rotation, this.transform);
                    objectList.Add(clone);

                    tempPortalHandler = clone.GetComponent<PortalHandler>();
                    tempPortalHandler.portals[0].transform.position = position;

                    portalCounter++;
                }
                else if (portalCounter == 1)
                {
                    tempPortalHandler.portals[1].transform.position = position;
                    tempPortalHandler = null;

                    portalCounter = 0;
                }
                
            }

            row++;
            if (row >= numberOfRows)
            {
                row = 0;
                col++;
            }
            
        }

        row++;
        col++;

        GameObject groundClone = Instantiate(GroundPrefab, new Vector3(GroundPrefab.transform.position.x, 0, col/2), Quaternion.identity);
        

        objectList.Add(groundClone);

        groundClone.transform.localScale = new Vector3(numberOfRows, 1, col);
        Material[] mat = groundClone.GetComponent<MeshRenderer>().sharedMaterials;

        mat[0].mainTextureScale = new Vector2(numberOfRows, 1);
        mat[1].mainTextureScale = new Vector2(numberOfRows, col);
        mat[2].mainTextureScale = new Vector3(col, 1);

    }
}
