using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Product> products;

    private List<float> productQuantities = new List<float>();

    private void Awake()
    {
        InternalInstance = this;

        foreach (Product product in products)
        {
            productQuantities.Add(product.initialValue);
        }
    }


    private static GameController InternalInstance;

    public static GameController Instance
    {
        get
        {
            if (InternalInstance == null)
            {
                InternalInstance = FindObjectOfType<GameController>();
            }

            return InternalInstance;
        }
    }

    private int GetIndex(Product product)
    {
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i] == product)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddProduct(Product product, float quantity) 
    {
        int index = GetIndex(product);
        float newValue = productQuantities[index] + quantity;
        productQuantities[index] = Mathf.Min(product.maxValue, newValue);
    }

    public void RemoveProduct(Product product, float quantity)
    {
        int index = GetIndex(product);
        float newValue = productQuantities[index] - quantity;
        productQuantities[index] = Mathf.Max(0.0f, newValue);
    }

    public float GetProductValue(Product product)
    {
        return productQuantities[GetIndex(product)];
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        foreach(Product product in products)
        {
            RemoveProduct(product, product.consumptionSpeed * Time.deltaTime);
            if (productQuantities[GetIndex(product)] <= 0.0f && product.isVital)
            {
                LoseGame();
            }
        }
    }
    public void LoseGame()
    {
        Debug.Log("You lost !");
    }

    public override string ToString()
    {
        return string.Join(", ", productQuantities.Zip(products, (a, b) => Tuple.Create(a, b)));
    }
}
