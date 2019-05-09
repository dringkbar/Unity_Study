using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LevelEditor : MonoBehaviour
{
    private string levelName = "Level1";
    int xMin = 0;
    int xMax = 0;
    int yMin = 0;
    int yMax = 0;

    public List<Transform> tiles;

    GameObject dynamicParent;
    private Transform toCreate;

    private int[][] level = new int[][]
{
    new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 4, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    new int[]{1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 3, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 3, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 2, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
   };

    private void Start()
    {
        dynamicParent = GameObject.Find("Dynamic Objects");
        BuildLevel();
        toCreate = tiles[0];

        enabled = false;
        GameController._instance.UpdateOrbTotals(true);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z * -1;
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

            int posX = Mathf.FloorToInt(pos.x + .5f);
            int posY = Mathf.FloorToInt(pos.y + .5f);

            Collider[] hitColliders = Physics.OverlapSphere(pos, 0.45f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (toCreate.name != hitColliders[i].gameObject.name)
                {
                    DestroyImmediate(hitColliders[i].gameObject);
                }
                else { return; }
                i++;
            }
            CreateBlock(tiles.IndexOf(toCreate) + 1, posX, posY);
            GameController._instance.UpdateOrbTotals();
        }
        if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            Physics.Raycast(ray, out hit, 100);

            if ((hit.collider != null) && (hit.collider.name != "Player"))
            {
                Destroy(hit.collider.gameObject);
            }
            GameController._instance.UpdateOrbTotals();
        }
    }

    void BuildLevel()
    {
        for (int yPos = 0; yPos < level.Length; yPos++)
        {
            for (int xPos = 0; xPos < (level[yPos]).Length; xPos++)
            {
                CreateBlock(level[yPos][xPos], xPos, level.Length - yPos);
            }
        }
    }

    public void CreateBlock(int value, int xPos, int yPos)
    {
        toCreate = null;
        if (xPos < xMin)
        {
            xMin = xPos;
        }
        if (xPos > xMax)
        {
            xMax = xPos;
        }

        if (yPos < yMin)
        {
            yMin = yPos;
        }
        if (yPos > yMax)
        {
            yMax = yPos;
        }

        if (value != 0)
        {
            toCreate = tiles[value - 1];
        }

        if (toCreate != null)
        {
            Transform newObject = Instantiate(toCreate, new Vector3(xPos, yPos, 0), Quaternion.identity) as Transform;
            newObject.name = toCreate.name;

            if (toCreate.name == "Goal")
            {
                GameController._instance.GoalPS = newObject.gameObject.GetComponent<ParticleSystem>();
                newObject.transform.Rotate(-90, 0, 0);
            }
            newObject.parent = dynamicParent.transform;
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 20, 100, 100));
        levelName = GUILayout.TextField(levelName);
        if (GUILayout.Button("Save"))
        {
            SaveLevel();
        }
        if (GUILayout.Button("Load"))
        {
            if (File.Exists(Application.persistentDataPath + "/" + levelName + ".lvl"))
            {
                LoadLevelFile(levelName);
                PlayerStart.spawned = false;
                StartCoroutine(LoadedUpdate());
            }
            else
            {
                levelName = "Error";
            }
        }
        if (GUILayout.Button("Quit"))
        {
            enabled = false;
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(Screen.width - 110, 20, 100, 800));

        foreach (Transform  item in tiles)
        {
            if (GUILayout.Button(item.name))
            {
                toCreate = item;
            }
        }
        GUILayout.EndArea();
    }

    void SaveLevel()
    {
        List<string> newLevel = new List<string>();

        for (int i = yMin; i < yMax; i++)
        {
            string newRow = "";
            for (int j = xMin; j < xMax; j++)
            {
                Vector3 pos = new Vector3(j, i, 0);
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit = new RaycastHit();

                Physics.Raycast(ray, out hit, 100);

                Collider[] hitColliders = Physics.OverlapSphere(pos, 0.1f);

                if (hitColliders.Length>0)
                {
                    for (int k = 0; k < tiles.Count; k++)
                    {
                        if (tiles[k].name==hitColliders[0].gameObject.name)
                        {
                            newRow += (k + 1).ToString() + ",";
                        }
                    }
                }
                else
                {
                    newRow += "0,";
                }
            }
            newRow += "\n";
            newLevel.Add(newRow);
        }
        newLevel.Reverse();

        string levelComplete = "";

        foreach (string level in newLevel)
        {
            levelComplete += level;
        }
        print(levelComplete);

        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + levelName + ".lvl");
        bFormatter.Serialize(file, levelComplete);
        file.Close();
    }

    void LoadLevelFile(string level)
    {
        foreach (Transform child in dynamicParent.transform)
        {
            Destroy(child.gameObject);
        }
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream file = File.OpenRead(Application.persistentDataPath + "/" + levelName + ".lvl");

        string levelData = bFormatter.Deserialize(file) as string;

        file.Close();

        LoadLevelFromString(levelData);

        levelName = level;
    }

    public void LoadLevelFromString(string content)
    {
        List<string> lines = new List<string>(content.Split('\n'));
        for (int i = 0; i < lines.Count; i++)
        {
            string[] blockIDs = lines[i].Split(',');
            for (int j = 0; j < blockIDs.Length-1; j++)
            {
                CreateBlock(int.Parse(blockIDs[j]), j, lines.Count - i);
            }
        }
    }

    IEnumerator LoadedUpdate()
    {
        yield return 0;
        GameController._instance.UpdateOrbTotals(true);
    }
}