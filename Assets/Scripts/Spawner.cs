using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _explodeForce = 200f;

    private void OnEnable()
    {
        _cube.Separation += CreateCubes;
    }

    private void OnDisable()
    {
        _cube.Separation -= CreateCubes;
    }

    private void CreateCubes(Cube cube)
    {
        Cube newCube;
        int minimumNumberObjects = 2;
        int maximumNumberObjects = 6;
        int creatingObjects = UnityEngine.Random.Range(minimumNumberObjects, ++maximumNumberObjects);

        for (int i = 0; i < creatingObjects; i++)
        {
            newCube = Instantiate(cube, transform.position, Quaternion.identity);
            newCube.Init();

            foreach (Rigidbody explorableObject in GetNearRigidbodies())
            {
                explorableObject.AddExplosionForce(_explodeForce, transform.position, transform.localScale.x);
            }
        }
    }

    private List<Rigidbody> GetNearRigidbodies()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}