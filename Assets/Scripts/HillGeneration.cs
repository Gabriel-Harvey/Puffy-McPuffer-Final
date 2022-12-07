

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillGeneration : MonoBehaviour
{
    [SerializeField]
    private float width = 5f;

    [SerializeField]
    private float depth = 50f;

    [SerializeField]
    private float maxHeight = 5.0f;

    [SerializeField]
    private int cellsX1 = 100;

    [SerializeField]
    private int cellsZ1 = 100;

    [SerializeField]
    private float perlinStepSizeX = 0.1f;

    [SerializeField]
    private float perlinStepSizeZ = 0.1f;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private Vector3[] normals;
    private bool updateTerrain = true;

    private Mesh mesh;

    // Is called when an edit is made in the editor
    private void OnValidate()
    {
        updateTerrain = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (updateTerrain == true)
        {
            CreateTerrain();
        }
        updateTerrain = false;
    }

    void CreateTerrain()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;
        }

        int verticesRowCount = cellsX1 + 1;
        int verticesCount = verticesRowCount * (cellsZ1 + 1);
        int trianglesCount = 6 * cellsX1 * cellsZ1;


        vertices = new Vector3[verticesCount];
        uvs = new Vector2[verticesCount];
        normals = new Vector3[verticesCount];
        triangles = new int[trianglesCount];

        // Set the vertices of the mesh
        int vertexIndex = 0;
        for (int z = 0; z <= cellsZ1; ++z)
        {
            float percentageZ = (float)z / (float)cellsZ1;
            float startZ = percentageZ * depth;

            for (int x = 0; x <= cellsX1; ++x)
            {
                float percentageX = ((float)x / (float)cellsX1);
                float startX = percentageX * width;

                float height = Mathf.PerlinNoise(perlinStepSizeX * x, perlinStepSizeZ * z) * maxHeight;

                float heightPercentage = height / maxHeight;
                
                vertices[vertexIndex] = new Vector3(startX, height, startZ);
                ++vertexIndex;

                
            }
        }

        // Setup the indexes so they are in the correct order and will render correctly
        vertexIndex = 0;
        int trianglesIndex = 0;
        for (int z = 0; z < cellsZ1; ++z)
        {
            for (int x = 0; x < cellsX1; ++x)
            {
                vertexIndex = x + (verticesRowCount * z);

                triangles[trianglesIndex++] = vertexIndex;
                triangles[trianglesIndex++] = vertexIndex + verticesRowCount;
                triangles[trianglesIndex++] = (vertexIndex + 1) + verticesRowCount;
                triangles[trianglesIndex++] = (vertexIndex + 1) + verticesRowCount;
                triangles[trianglesIndex++] = vertexIndex + 1;
                triangles[trianglesIndex++] = vertexIndex;
            }
        }

        // Assign all of the data that has been created to the mesh and update it
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.RecalculateNormals();
        mesh.UploadMeshData(false);
    }
}

