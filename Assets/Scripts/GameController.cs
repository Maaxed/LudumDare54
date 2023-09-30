using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int INIT_OXYGEN = 100;
    private const int INIT_SATIETY = 10;
    private const int INIT_SPEED = 20;
    private const int INIT_ELECTRICITY = 30;
    private const int INIT_SHIELD = 10;

    private IDictionary<string, int> products = new Dictionary<string, int>();

    private void Awake() { 
        Instance = this;

        products.Add("Oxygen", INIT_OXYGEN);
        products.Add("Satiety", INIT_SATIETY);
        products.Add("Speed", INIT_SPEED);
        products.Add("Electricity", INIT_ELECTRICITY);
        products.Add("Shield", INIT_SHIELD);
    }

    public static GameController Instance { get; private set; }

    public void AddProduct(Product product, int quantity) {
        products[product.label] += quantity;
    }

    public void RemoveProduct(Product product, int quantity) {
        products[product.label] -= quantity;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override string ToString() {
        return String.Join(", ", products);
    }
}
