using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidIconController : MonoBehaviour
{
    public GameObject AstoroidPannel;
    public GameObject OtherPannel;
    public EventManager EventManager;
    public Product ShieldProduct;


    // Update is called once per frame
    void Update()
    {
        if (EventManager.AsteroidFieldEventActive && !GameController.Instance.IsProductEnabled(ShieldProduct))
        {
            AstoroidPannel.SetActive(true);
            OtherPannel.SetActive(false);
        }
        else
        {
            AstoroidPannel.SetActive(false);
            OtherPannel.SetActive(true);
        }
    }
}
