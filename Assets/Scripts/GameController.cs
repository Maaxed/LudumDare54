using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Product> products;

    private IDictionary<Product, float> productsQuantity = new Dictionary<Product, float>();

    private void Awake() 
    { 
        Instance = this;

        foreach(Product product in products) {
            productsQuantity.Add(product, product.initialValue);
        }
    }

    public static GameController Instance { get; private set; }

    public void AddProduct(Product product, float quantity) 
    {
        productsQuantity[product] += quantity;
    }

    public void RemoveProduct(Product product, float quantity) 
    {
        productsQuantity[product] -= quantity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        foreach(Product product in products) {
            RemoveProduct(product, product.consumptionSpeed * Time.deltaTime);
        }
    }

    public override string ToString() 
    {
        return String.Join(", ", productsQuantity);
    }
}
