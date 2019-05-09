using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
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
}