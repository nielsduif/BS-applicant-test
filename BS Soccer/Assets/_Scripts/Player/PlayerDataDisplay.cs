using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshPro jerseyNumber, speedNumber;

    public void SetJerseyNumber(int _number)
    {
        jerseyNumber.text = $"{_number}";
    }

    public void UpdateSpeedText(float _speed)
    {
        speedNumber.text = $"{Mathf.Ceil(_speed)}";
    }
}