using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public static LevelSettings Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public int beatsPerMinute;

    [Header("Level Creator Settings")]
    public string PathToFile;
    public GameObject GroundPrefab;
    public GameObject ObstaclePrefab;
    public GameObject ObjectivePrefab;
    public List<int> dataList;
    public List<GameObject> objectList;
    


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
        int width = 0;
        int length = 0;

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
                //Create Obstacle at Row, Col
                Vector3 position = new Vector3(row, 1, col);
                //PrefabUtility.InstantiatePrefab(ObstaclePrefab, position);
                GameObject clone = Instantiate(ObjectivePrefab, position, Quaternion.identity, this.transform);
                objectList.Add(clone);
            }

            row++;
            if (row >= 3)
            {
                row = 0;
                col++;
            }
            
        }

        row++;
        col++;

        GameObject groundClone = Instantiate(GroundPrefab, new Vector3(GroundPrefab.transform.position.x, 0, col/2), Quaternion.identity);
        

        objectList.Add(groundClone);

        groundClone.transform.localScale = new Vector3(3, 1, col);
        Material[] mat = groundClone.GetComponent<MeshRenderer>().sharedMaterials;

        mat[0].mainTextureScale = new Vector2(3, 1);
        mat[1].mainTextureScale = new Vector2(3, col);
        mat[2].mainTextureScale = new Vector3(col, 1);

    }
}
