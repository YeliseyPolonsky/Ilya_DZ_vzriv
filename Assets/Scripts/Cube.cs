using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private int _separationProbability = 100;
    private int _decreaseNumber = 2;

    public event Action<Cube> Separation;

    private void Start()
    {
        SetRandomColor();
    }
    
    private void SetRandomColor()
    {
        GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();
    }

    private void OnMouseUpAsButton()
    {
        if (IsProbable(_separationProbability))
            Separation?.Invoke(this);
           
        Destroy(this.gameObject);  
    }
    
    private bool IsProbable(int separationProbability)
    {
        int minimum = 1;
        int maximum = 100;
        return UnityEngine.Random.Range(minimum, ++maximum) <= separationProbability;
    }

    public void Init()
    {
        _separationProbability /= _decreaseNumber;
        transform.localScale /= _decreaseNumber;
    }
}
