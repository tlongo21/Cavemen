using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This is the main script that will create our Cube.
// This script calls another script that holds information about our cube called "CubeMeshData"

// For all self made meshes we need unity to apply these components (MeshFIlter,MeshRenderer) to our mesh.
// These components will create our texture and adjust the lighting and whatnot.
[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour {

    // Start by defining some variables to be used throughout the scripts
	Mesh mesh;
    // Note that any mesh is made up on verticies connected in a specific triangular pattern.
    // Thus, we will want lists of our vertices and patterns for the creation on the cube.
    List<Vector3> vertices;
	List<int> triangles;

    // These variables will help us adjust the scale and position of the cube in unity
    public float scale = 1f;
    float adjScale; // the adjusted scale just places the cube center at the origin.
    public int posX, posY, posZ;

    // At the beginning of the program generate the mesh and place it at the origin.
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * 0.5f;
	}

    // This will generate our cube based on the size and position around the origin.
    void Start() {
        MakeCube(adjScale, new Vector3((float)posX*scale, (float)posY*scale, (float)posZ*scale));
        UpdateMesh(); // This is what adjusts the data of the verticies and triangels
    }

    // We make a cube based on the vertex locations and the triangular patterns.
	void MakeCube(float cubeScale, Vector3 cubePos){
		vertices = new List<Vector3>();
		triangles = new List<int>();

        // the cube contains 6 triangles so we will generate each face from the six triangle patterns
		for (int i = 0; i < 6; i++){
			MakeFace(i, cubeScale, cubePos);
		}
	}
    // This function generates the face of our cube based on direction scale and position.
	void MakeFace(int dir, float faceScale, Vector3 facePos){
        // This references our other script "CubeMeshData" see this script for more details.
		vertices.AddRange(CubeMeshData.faceVertices(dir, faceScale, facePos));

        // Store the vertex that we are looking at in an count integer.
		int vCount = vertices.Count;

        // Generate the face of the cube from the triangle data
		triangles.Add( vCount - 4);     // the face value is set up in this way for some reason that 
		triangles.Add( vCount - 4 + 1); //I don't understand. But this basically just calls the face based on the vertex.
		triangles.Add( vCount - 4 + 2);
		triangles.Add( vCount - 4);
		triangles.Add( vCount - 4 + 2);
		triangles.Add( vCount - 4 + 3);
	}

    // Update the mesh in the initialization of the script. 
    void UpdateMesh() {
        mesh.Clear();                       //clear previous mesh if there was one previously made
        mesh.vertices = vertices.ToArray(); // the verticies must be an array when generating the mesh
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();          // This calculates the normal vector of the face and uses it to
                                            // accurately generate the lighting.

    }
}
