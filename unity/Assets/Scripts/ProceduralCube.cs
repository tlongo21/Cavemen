using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour {

	Mesh mesh;
  private System.Collections.Generic.List<Vector3> vertices;
	private System.Collections.Generic.List<int> triangles;

	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
	}

    // Use this for initialization
    void Start() {
        MakeCube();
        UpdateMesh();
    }

	void MakeCube(){
		vertices = new list<Vector3>();
		triangles = new list<int>();

		for (int i = 0; i < 6; i++){
			MakeFace(i);
		}
	}

	void MakeFace(int dir){
		vertices.AddRange(CubeMeshData.faceVertices(dir));

		int vCount = vertices.Count;

		triangles.Add( vCount - 4);
		triangles.Add( vCount - 4 + 1);
		triangles.Add( vCount - 4 + 2);
		triangles.Add( vCount - 4);
		triangles.Add( vCount - 4 + 2);
		triangles.Add( vCount - 4 + 3);
	}

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

    }
}
