using Fusion.Fluid;
using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class Waves : Fluid
{
    // Public Properties
    public int Dimension = 10;
    public float UVScale = 2f;
    public Octave[] Octaves;

    // Mesh
    protected MeshFilter MeshFilter;
    protected Mesh Mesh;
    protected MeshCollider MeshCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Mesh Setup
        Mesh = new Mesh();
        Mesh.name = gameObject.name;

        Mesh.vertices = GenerateVerts();
        Mesh.triangles = GenerateTries();
        Mesh.uv = GenerateUVs();

        Mesh.RecalculateNormals();
        Mesh.RecalculateBounds();

        // Get Components
        MeshFilter = GetComponent<MeshFilter>();
        MeshCollider = GetComponent<MeshCollider>();


        // Assign Mesh
        MeshFilter.sharedMesh = Mesh;

        // Assign Initial Collision Mesh
        MeshCollider.sharedMesh = null;
        MeshCollider.sharedMesh = Mesh;
    }

    public float GetHeight(Vector3 position)
    {
        // Scale factor and position in local space
        var scale = new Vector3(
            1f / transform.lossyScale.x,
            0,
            1f / transform.lossyScale.z
        );

        var localPos = Vector3.Scale(
            position - transform.position,
            scale
        );

        // Get edge points
        var p1 = new Vector3(
            Mathf.Floor(localPos.x), 0,
            Mathf.Floor(localPos.z)
        );

        var p2 = new Vector3(
            Mathf.Floor(localPos.x), 0,
            Mathf.Ceil(localPos.z)
        );

        var p3 = new Vector3(
            Mathf.Ceil(localPos.x), 0,
            Mathf.Floor(localPos.z)
        );

        var p4 = new Vector3(
            Mathf.Ceil(localPos.x), 0,
            Mathf.Ceil(localPos.z)
        );

        // Clamp if the position is outside the plane
        p1.x = Mathf.Clamp(p1.x, 0, Dimension);
        p1.z = Mathf.Clamp(p1.z, 0, Dimension);

        p2.x = Mathf.Clamp(p2.x, 0, Dimension);
        p2.z = Mathf.Clamp(p2.z, 0, Dimension);

        p3.x = Mathf.Clamp(p3.x, 0, Dimension);
        p3.z = Mathf.Clamp(p3.z, 0, Dimension);

        p4.x = Mathf.Clamp(p4.x, 0, Dimension);
        p4.z = Mathf.Clamp(p4.z, 0, Dimension);

        // Calculate distances
        float d1 = Vector3.Distance(p1, localPos);
        float d2 = Vector3.Distance(p2, localPos);
        float d3 = Vector3.Distance(p3, localPos);
        float d4 = Vector3.Distance(p4, localPos);

        float max = Mathf.Max(
            d1,
            d2,
            d3,
            d4 + Mathf.Epsilon
        );

        float w1 = max - d1;
        float w2 = max - d2;
        float w3 = max - d3;
        float w4 = max - d4;

        float dist = w1 + w2 + w3 + w4;

        // Weighted height sum
        var vertices = Mesh.vertices;

        float height =
            vertices[index(p1.x, p1.z)].y * w1 +
            vertices[index(p2.x, p2.z)].y * w2 +
            vertices[index(p3.x, p3.z)].y * w3 +
            vertices[index(p4.x, p4.z)].y * w4;

        // Scale
        if (dist <= Mathf.Epsilon)
            return transform.TransformPoint(
                new Vector3(localPos.x, 0, localPos.z)
            ).y;

        return transform.position.y +
               height * transform.lossyScale.y / dist;
    }

    private Vector3[] GenerateVerts()
    {
        var verts = new Vector3[
            (Dimension + 1) * (Dimension + 1)
        ];

        // Equally distributed vertices
        for (int x = 0; x <= Dimension; x++)
        {
            for (int z = 0; z <= Dimension; z++)
            {
                verts[index(x, z)] = new Vector3(x, 0, z);
            }
        }

        return verts;
    }

    private int[] GenerateTries()
    {
        var tries = new int[Mesh.vertices.Length * 6];

        // Two triangles are one tile
        for (int x = 0; x < Dimension; x++)
        {
            for (int z = 0; z < Dimension; z++)
            {
                tries[index(x, z) * 6 + 0] = index(x, z);
                tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                tries[index(x, z) * 6 + 2] = index(x + 1, z);

                tries[index(x, z) * 6 + 3] = index(x, z);
                tries[index(x, z) * 6 + 4] = index(x, z + 1);
                tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
            }
        }

        return tries;
    }

    private Vector2[] GenerateUVs()
    {
        var uvs = new Vector2[
            (Dimension + 1) * (Dimension + 1)
        ];

        for (int x = 0; x <= Dimension; x++)
        {
            for (int z = 0; z <= Dimension; z++)
            {
                var vec = new Vector2(
                    (x / UVScale) % 2,
                    (z / UVScale) % 2
                );

                uvs[index(x, z)] = new Vector2(
                    vec.x <= 1 ? vec.x : 2 - vec.x,
                    vec.y <= 1 ? vec.y : 2 - vec.y
                );
            }
        }

        return uvs;
    }

    private int index(int x, int z)
    {
        return x * (Dimension + 1) + z;
    }

    private int index(float x, float z)
    {
        return index((int)x, (int)z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mesh == null)
            return;

        var verts = Mesh.vertices;

        for (int x = 0; x <= Dimension; x++)
        {
            for (int z = 0; z <= Dimension; z++)
            {
                float y = 0f;

                for (int o = 0; o < Octaves.Length; o++)
                {
                    if (Octaves[o].alternate)
                    {
                        float perl = Mathf.PerlinNoise(
                            (x * Octaves[o].scale.x) / Dimension,
                            (z * Octaves[o].scale.y) / Dimension
                        ) * Mathf.PI * 2f;

                        y += Mathf.Cos(
                            perl +
                            Octaves[o].speed.magnitude * Time.time
                        ) * Octaves[o].height;
                    }
                    else
                    {
                        float perl = Mathf.PerlinNoise(
                            (x * Octaves[o].scale.x +
                             Time.time * Octaves[o].speed.x) / Dimension,

                            (z * Octaves[o].scale.y +
                             Time.time * Octaves[o].speed.y) / Dimension
                        ) - 0.5f;

                        y += perl * Octaves[o].height;
                    }
                }

                verts[index(x, z)] = new Vector3(x, y, z);
            }
        }

        // Update visual mesh
        Mesh.vertices = verts;
        Mesh.RecalculateNormals();
        Mesh.RecalculateBounds();

        // Refresh collision mesh at runtime
        MeshCollider.sharedMesh = null;
        MeshCollider.sharedMesh = Mesh;
    }

    public bool IsUnderwater(Vector3 position)
    {
        // Make sure the position is inside the wave mesh
        Vector3 localPos = transform.InverseTransformPoint(position);

        if (localPos.x < 0 || localPos.x > Dimension ||
            localPos.z < 0 || localPos.z > Dimension)
        {
            return false;
        }

        // Compare object position with wave height
        return position.y < GetHeight(position);
    }

    private void OnDestroy()
    {
        if (Mesh != null)
        {
            Destroy(Mesh);
        }
    }

    [Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternate;
    }
}