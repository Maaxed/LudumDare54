using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] public List<Product> products;

    private List<float> productQuantities = new List<float>();
    private string loseReason;

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

    public string GetLoseReason() {
        return loseReason;
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
                string reason;
                switch(product.label) {
                    case "Satiety":
                        reason = "of hunger";
                        break;
                    case "Oxygen":
                        reason = "of suffocation";
                        break;
                    case "Shield":
                        reason = "of an asteroid rain";
                        break;
                    default:
                        reason = "of unknown reason";
                        break;
                }
                LoseGame(reason);
            }
        }
    }
    public void LoseGame(string reason)
    {
        Debug.Log("You lost !");
        loseReason = reason;
        SceneManager.LoadSceneAsync("GameOverScreen", LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameOverScreen"));
    }

    public override string ToString()
    {
        return string.Join(", ", productQuantities.Zip(products, (a, b) => Tuple.Create(a, b)));
    }
}
