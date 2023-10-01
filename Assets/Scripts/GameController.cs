using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Product> products;
    public AudioSource audioSource;

    private List<ProductData> productData = new List<ProductData>();

    private void Awake()
    {
        InternalInstance = this;

        foreach (Product product in products)
        {
            var data = new ProductData();
            data.Quantity = product.initialValue;
            data.Enabled = product.initiallyEnabled;
            productData.Add(data);
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
        ProductData data = productData[index];
        float oldQuantity = data.Quantity;
        data.Quantity = Mathf.Min(product.maxValue, data.Quantity + quantity);
        productData[index] = data;

        if (oldQuantity <= 0.0f && data.Quantity > 0.0f && product.switchOnSound != null)
        {
            audioSource.PlayOneShot(product.switchOnSound);
        }
    }

    public void RemoveProduct(Product product, float quantity)
    {
        int index = GetIndex(product);
        ProductData data = productData[index];
        data.Quantity = Mathf.Max(0.0f, data.Quantity - quantity);
        productData[index] = data;
    }

    public bool IsProductEnabled(Product product)
    {
        return productData[GetIndex(product)].Enabled;
    }

    public void EnableProduct(Product product)
    {
        int index = GetIndex(product);
        ProductData data = productData[index];
        data.Enabled = true;
        productData[index] = data;
        if (productData[GetIndex(product)].Quantity > 0.0f && product.switchOnSound != null)
        {
            audioSource.PlayOneShot(product.switchOnSound);
        }
    }

    public void DisableProduct(Product product)
    {
        int index = GetIndex(product);
        ProductData data = productData[index];
        data.Enabled = false;
        productData[index] = data;
    }

    public float GetProductValue(Product product)
    {
        return productData[GetIndex(product)].Quantity;
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
            int index = GetIndex(product);
            if (!productData[index].Enabled)
                continue;

            RemoveProduct(product, product.consumptionSpeed * Time.deltaTime);
            if (productData[index].Quantity <= 0.0f && product.isVital)
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
        return string.Join(", ", productData.Zip(products, (a, b) => Tuple.Create(a, b)));
    }

    public struct ProductData
    {
        public float Quantity;
        public bool Enabled;

        public override string ToString()
        {
            return Quantity + " " + (Enabled ? "Enabled" : "Disabled");
        }
    }
}
