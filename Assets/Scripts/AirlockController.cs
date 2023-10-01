using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockController : MonoBehaviour
{
    public DoorController InteriorDoor;
    public DoorController ExteriorDoor;
    public float OpenTime = 1.0f;
    public float ClosedTime = 1.0f;
    public float DestroyTime = 1.0f;
    public Vector3 EjectionForce;
    public Transform ItemParent;
    public Bounds ItemSpawnArea;
    public ItemGeneration ItemGeneration;
    private List<GameObject> ItemPool;
    private System.Random random = new System.Random();
    private bool EjectingItems = false;

    private void OnEnable()
    {
        StartCoroutine(Run());
    }
    private void OnDisable()
    {
        StopCoroutine(Run());
    }

    public IEnumerator Run()
    {
        while (true)
        {
            yield return PlayCycle();
        }
    }

    public IEnumerator PlayCycle()
    {
        yield return new WaitForSeconds(ClosedTime);

        GenerateItems();

        yield return InteriorDoor.Open();
        yield return new WaitForSeconds(OpenTime);
        yield return InteriorDoor.Close();

        yield return ExteriorDoor.Open();
        EjectingItems = true;
        yield return new WaitForSeconds(DestroyTime);
        EjectingItems = false;
        yield return ExteriorDoor.Close();
    }

    private GameObject ChooseNextItem()
    {
        if (ItemPool == null || ItemPool.Count <= 0)
        {
            if (ItemPool == null)
            {
                ItemPool = new List<GameObject>();
            }
            else if (ItemGeneration.Next != null)
            {
                ItemGeneration = ItemGeneration.Next;
            }

            foreach (ItemGeneration.ItemConfig itemConfig in ItemGeneration.Items)
            {
                for (int i = 0; i < itemConfig.Count; i++)
                {
                    ItemPool.Add(itemConfig.Item);
                }
            }

            // Shuffle item list
            for (int i = ItemPool.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                // Swap item i and j
                GameObject value = ItemPool[j];
                ItemPool[j] = ItemPool[i];
                ItemPool[i] = value;
            }

        }
        GameObject item = ItemPool[ItemPool.Count - 1];

        ItemPool.RemoveAt(ItemPool.Count - 1);

        return item;
    }

    public void GenerateItems()
    {
        int count = ItemGeneration.ItemsPerCycle;
        for (int  i = 0; i < count; i++)
        {
            GameObject ItemPrefab = ChooseNextItem();
            float x = Random.Range(ItemSpawnArea.min.x, ItemSpawnArea.max.x);
            float y = Random.Range(ItemSpawnArea.min.y, ItemSpawnArea.max.y);
            float z = Random.Range(ItemSpawnArea.min.z, ItemSpawnArea.max.z);
            Instantiate(ItemPrefab, transform.position + new Vector3(x, y ,z), Quaternion.identity, ItemParent);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!EjectingItems)
            return;

        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb == null)
            return;

        if (rb.CompareTag("Player") || rb.CompareTag("Item"))
        {
            rb.AddForce(transform.rotation * EjectionForce);

            // No gravity / drag in space
            rb.useGravity = false;
            rb.drag = 0.0f;
            rb.angularDrag = 0.0f;

            PlayerController player = other.GetComponentInParent<PlayerController>();

            if (player != null)
            {
                player.enabled = false;
                GameController.Instance.LoseGame();
            }
        }
    }
}
