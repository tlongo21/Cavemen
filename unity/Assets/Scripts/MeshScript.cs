using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshScript : MonoBehaviour {

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int gridSize;
    public float cellSize = 1;
    public Vector3 gridOffset;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Use this for initialization
    void Start () {
        MakeContiguousProceduralGrid();
        UpdateMesh();
	}

    void MakeContiguousProceduralGrid()
    { //set array size
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];

        //set tracker integers
        int v = 0;
        int t = 0;

        // set vertex offset
        float vertexOffeset = cellSize * 0.5f;

        // generate Grid
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffeset,0,(y*cellSize)-vertexOffeset);
                v += 1;
            }
        }

        //reset vertex tracker;
        v = 0;

        // set each cell triangle
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridSize+1);
                triangles[t + 5] = v + (gridSize+1)+1;

                v++;
                t += 6;
            }
            v++;
        }
    }


    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
}
