using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatorTest : MonoBehaviour
{
    // Reference to the submarine
    public GameObject submarine;

    // Reference to the terrainPatch prefab
    public GameObject terrainPatch;

    // Reference to the terrainpatch that the submarine is hovering over
    MarchingCompute centerPatch;

    // The square root of the number of terrain patches(patchesSide * patchesSide terrain patches
    // centered in the submarine position)
    public int patchesSide = 5;

    // The maximum distance from the submarine that a terrainPatch is allowed to have 
    int maximum_distance = 200;

    //List of terrain patches
    List<MarchingCompute> patchesList = new List<MarchingCompute>();

    public int perlinType = 0;

    public GameObject pauseMenu;

    public List<MarchingCompute> getPatchesList()
    {
        return patchesList;
    }

    // Start is called before the first frame update
    void Start()
    {

        Initialization();

    }

    void Initialization()
    {
        // Create the patchesSide * patchesSide terrain patches
        // centered in the submarine position

        MarchingCompute patch_MarchingCompute = terrainPatch.GetComponent<MarchingCompute>();
        int patchWidth = patch_MarchingCompute.width;
        int patchHeight = patch_MarchingCompute.height;
        int xDistanceToSub = (int)submarine.transform.position.x - patchesSide * patchWidth / 2;
        int zDistanceToSub = (int)submarine.transform.position.z - patchesSide * patchWidth / 2;
        int yDistanceToSub = (int)submarine.transform.position.y - patchesSide * patchHeight / 2;

        for (int i = 0; i < patchesSide; i++)
        {
            for (int j = 0; j < patchesSide; j++)
            {
                    GameObject patch = Instantiate(terrainPatch);
                    MarchingCompute patch_child_MarchingCompute = patch.GetComponent<MarchingCompute>();
                    patch_child_MarchingCompute.perlinType = perlinType;

                    int xStart = patchWidth * i + xDistanceToSub;
                    int zStart = patchWidth * j + zDistanceToSub;
                    patch_child_MarchingCompute.SetStartingPosition(new Vector3Int(xStart, 0, zStart));

                    if (i == patchesSide / 2 && j == patchesSide / 2)
                    {
                        centerPatch = patch_child_MarchingCompute;
                    }

                    patchesList.Add(patch_child_MarchingCompute);

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the submarine crossed the border from one terrain patch to another
        if (!isPointInCube(submarine.transform.position, centerPatch.GetStartingPosition(), centerPatch.GetBoundaries()))
        {

            // Update the map and set the new center patch
            SetCenter();
            UpdateMap();

        }

        // Bring up the Pause Menu if Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.transform.parent.gameObject.active = !pauseMenu.transform.parent.gameObject.active;

            for (int i = 0; i < pauseMenu.transform.parent.transform.childCount; i++) {
                pauseMenu.transform.parent.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            pauseMenu.active = !pauseMenu.active;

            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = 1;
            }
        }

    }

    // Potentially add new terrain patches depending on the current center patch
    // The center patch has to be in the center of patchesSide * patchesSide
    // adiacent terrain patches.
    void UpdateMap()
    {

        MarchingCompute patch_MarchingCompute = terrainPatch.GetComponent<MarchingCompute>();
        int patchWidth = patch_MarchingCompute.width;
        int xDistanceToSub = centerPatch.GetStartingPosition().x - (patchesSide - 1) * patchWidth / 2;
        int zDistanceToSub = centerPatch.GetStartingPosition().z - (patchesSide - 1) * patchWidth / 2;

        for (int i = 0; i < patchesSide; i++)
        {
            for (int j = 0; j < patchesSide; j++)
            {
                
                // Calculate the position of the new patch
                int xStart = patchWidth * i + xDistanceToSub;
                int zStart = patchWidth * j + zDistanceToSub;
                int sem = 0;

                // If the a patch already exists at the position continue
                foreach (MarchingCompute patch_it in patchesList)
                {

                    Vector3Int patch_start = patch_it.GetStartingPosition();

                    if (xStart == patch_start.x && zStart == patch_start.z)
                    {

                        sem = 1;
                        break;

                    }

                }

                // Create a new patch 
                if (sem == 0)
                {

                    GameObject patch = Instantiate(terrainPatch);
                    MarchingCompute patch_child_MarchingCompute = patch.GetComponent<MarchingCompute>();
                    patch_child_MarchingCompute.perlinType = perlinType;

                    patch_child_MarchingCompute.SetStartingPosition(new Vector3Int(xStart, 0, zStart));

                    patchesList.Add(patch_child_MarchingCompute);

                }

            }
        }


        // Destroy terrain patches that are too far away from the submarine
        List<MarchingCompute> patchesToDestroy = new List<MarchingCompute>();
        Vector2 submarinePos2D = new Vector2(submarine.transform.position.x, submarine.transform.position.z);

        foreach (MarchingCompute patch_it in patchesList)
        {

            Vector2 patchPos2D = new Vector2(patch_it.GetStartingPosition().x, patch_it.GetStartingPosition().z);

            if (Vector2.Distance(patchPos2D, submarinePos2D) > maximum_distance)
            {
                patchesToDestroy.Add(patch_it);
            }

        }

        foreach (MarchingCompute patch_it in patchesToDestroy)
        {
            patchesList.Remove(patch_it);
            Destroy(patch_it.gameObject);
        }
    }

    // Determines the terrain patch that the submarine is currently floating over
    void SetCenter()
    {

        foreach (MarchingCompute patch in patchesList)
        {

            if (isPointInCube(submarine.transform.position, patch.GetStartingPosition(), patch.GetBoundaries()))
            {

                centerPatch = patch;
                break;

            }

        }

    }

    // Checks if a point is in a cube(the cube is determined by two opposite corners)
    bool isPointInCube(Vector3 point, Vector3Int start, Vector3Int boundary)
    {

        if (point.x >= start.x && point.x < boundary.x &&
            point.z >= start.z && point.z < boundary.z)
        {
            return true;
        }

        return false;
    }
}
