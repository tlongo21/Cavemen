using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this program is only designed to hold data about our cube to be used in a different script.
public static class CubeMeshData{ // remove "MoneBehavior" because it will not exist in our scene.

    // There are 8 unique vertices for a cube. The ones defined below are based on a cube centered at the origin.
    public static Vector3[] vertices = {
        new Vector3( 1, 1, 1),
        new Vector3(-1, 1, 1),
        new Vector3(-1,-1, 1),
        new Vector3( 1,-1, 1),
        new Vector3(-1, 1,-1),
        new Vector3( 1, 1,-1),
        new Vector3( 1,-1,-1),
        new Vector3(-1,-1,-1)
    };
    // This part below is slightly confusing, but we are basically defining our faces of the cube
    // The facesTriangles create a quad that has two shared verticies (because each quad is
    // made up of two triangles sharing one side (or two verticies)
    // Also, the verticies must be connected in a clockwise direction. This happens because Unity only renders
    // one face of the triangle so we want to make sure that it is rendering the outside of our cube. If we were
    // to make the walls of a room for instance we would want to make our cube with counter clockwise triangles.
    public static int[][] faceTriangles = {
        new int[]{ 0, 1, 2, 3 }, // so this is triangle (0,1,2) and triangle (2,1,3)
        new int[]{ 5, 0, 3, 6 },
        new int[]{ 4, 5, 6, 7 },
        new int[]{ 1, 4, 7, 2 },
        new int[]{ 5, 4, 1, 0 },
        new int[]{ 3, 2, 7, 6 }

    };
    // We generate the triangle faces and connect them below.
    public static Vector3[] faceVertices(int dir, float scale, Vector3 pos){
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++) 
        {
            fv[i] = (vertices[faceTriangles[dir][i]] * scale) + pos;  // set it up based on position, scale, dir.     
        }
        return fv;
       
    }
}
