using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemGeneration", menuName = "ScriptableObjects/ItemGeneration")]
public class ItemGeneration : ScriptableObject
{
	public float DelayBetweenCycles = 10.0f;
	public int ItemsPerCycle = 1;
	public List<ItemConfig> Items = new List<ItemConfig>();
	public ItemGeneration Next;

	[System.Serializable]
	public struct ItemConfig
	{
        public GameObject Item;
        public int Count;
	}
}