using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will create a plane of connected triangles that are repeatable.

// Like the cube this requires the following commonents to add texture and light to the plane.
[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralPlane : MonoBehaviour {

    // Define some variables for use throughout the script
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    // This is how the user will create the size of the plane.
    public int gridSize;
    public float cellSize = 1;
    public Vector3 gridOffset;

    // Get the mesh
    void Awake() {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Use this for initialization
    void Start () {
        MakeContiguousProceduralGrid();
        UpdateMesh();
	}

    // This will generate our Gird.
    void MakeContiguousProceduralGrid()
    { //set array size based on initial parameters.
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];

        //set tracker integers these will help us set the size of the plane.
        int v = 0;
        int t = 0;

        // set vertex offset 
        float vertexOffeset = cellSize * 0.5f;

        // generate Grid (note that x,y) correspond to the (x,z) plane.
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

        // set each cell triangle generate the cells based on the total gridSize.
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

    // Place the vertex locations and the triangles for the plane, get the normal vector correct.
    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

    }
}
