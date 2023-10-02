using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockController : MonoBehaviour
{
    public DoorController InteriorDoor;
    public DoorController ExteriorDoor;
    public AudioSource OpenAudio;
    public AudioSource EjectAudio;
    public AudioSource EjectPlayerAudio;
    public float InitialDelay = 0.0f;
    public float OpenTime = 1.0f;
    public float DestroyTime = 1.0f;
    public Vector3 EjectionForce;
    public Transform ItemParent;
    public Bounds ItemSpawnArea;
    public ItemGeneration ItemGeneration;
    private List<GameObject> ItemPool;
    private System.Random random = new System.Random();
    private bool EjectingItems = false;
    private float StartOpenTime = -1.0f;

    public float Progress => StartOpenTime < 0.0f ? 1.0f : Mathf.Clamp01((Time.time - StartOpenTime) / OpenTime);

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
        yield return new WaitForSeconds(InitialDelay);

        while (true)
        {
            yield return PlayCycle();
        }
    }

    public IEnumerator PlayCycle()
    {
        yield return new WaitForSeconds(ItemGeneration.DelayBetweenCycles);

        GenerateItems();

        OpenAudio.Play();
        yield return InteriorDoor.Open();
        StartOpenTime = Time.time;
        yield return new WaitForSeconds(OpenTime);
        StartOpenTime = -1.0f;
        yield return InteriorDoor.Close();

        EjectAudio.Play();
        yield return ExteriorDoor.Open();
        EjectingItems = true;
        yield return new WaitForSeconds(DestroyTime);
        EjectingItems = false;
        yield return ExteriorDoor.Close();
    }

    private void SetupItemPool()
    {
        ItemPool.Clear();

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

    private GameObject ChooseNextItem()
    {
        if (ItemPool == null)
        {
            ItemPool = new List<GameObject>();
            SetupItemPool();
        }

        GameObject item = ItemPool[ItemPool.Count - 1];

        ItemPool.RemoveAt(ItemPool.Count - 1);

        if (ItemPool.Count <= 0)
        {
            if (ItemGeneration.Next != null)
            {
                ItemGeneration = ItemGeneration.Next;
            }

            SetupItemPool();
        }

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
            // No gravity / drag in space
            rb.useGravity = false;
            rb.drag = 0.0f;
            rb.angularDrag = 0.0f;
            rb.constraints &= RigidbodyConstraints.FreezeRotation;

            rb.AddForce(transform.rotation * EjectionForce);

            PlayerController player = other.GetComponentInParent<PlayerController>();

            if (player != null)
            {
                player.enabled = false;
                if (!EjectPlayerAudio.isPlaying)
                    EjectPlayerAudio.Play();
                GameController.Instance.LoseGame("you have been ejected");
            }
        }
    }
}
