using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedParticleController : MonoBehaviour
{
    public Product ProductType;
    public ParticleSystem ParticleSystem;
    public float MaxSpeed = 1.0f;
    public float MaxEmission = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (ParticleSystem == null)
        {
            ParticleSystem = GetComponent<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        float value = Mathf.Clamp01(GameController.Instance.GetProductValue(ProductType) / ProductType.maxValue);

        var main = ParticleSystem.main;
        main.startSpeedMultiplier = MaxSpeed * value;

        var emission = ParticleSystem.emission;
        emission.rateOverTimeMultiplier = MaxEmission * value;
    }
}
