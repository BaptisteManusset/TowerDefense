using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public float width = 1;
    public float lenght = 1;

    public GameObject SpawnMob(GameObject obj, Transform parent)
    {
        Vector3 position = transform.position;

        position.x += Random.Range(-width, width) / 2;
        position.z += Random.Range(-lenght, lenght) / 2;

        return Instantiate(obj, position, transform.rotation, parent);
    }

    private void OnDrawGizmos()
    {
        DebugExtension.DrawBounds(new Bounds(transform.position, new Vector3(width, 10, lenght)), Color.white);
    }
}
