using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _woodAmountText;
    [SerializeField] private TextMeshProUGUI _mineralsAmountText;
    [SerializeField] private TextMeshProUGUI _clockText;

    private int _woodAmount = 0;
    private int _mineralsAmount = 0;

    private void Start()
    {
        _woodAmountText.text = $"{_woodAmount}";
        _mineralsAmountText.text = $"{_mineralsAmount}";
    }

    private void OnEnable()
    {
        EventManager.AddListener("ResourceCollected", _OnResourceCollected);
        EventManager.AddListener("TimeSet", _OnTimeSet);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener("ResourceCollected", _OnResourceCollected);
        EventManager.RemoveListener("TimeSet", _OnTimeSet);
    }

    private void _OnResourceCollected(object data)
    {
        object[] d = (object[])data;
        string resourceType = (string) d[0];
        int amount = (int)d[1];

        if (resourceType == "Wood")
        {
            _woodAmount += amount;
            _woodAmountText.text = $"{_woodAmount}";
        }
        else if (resourceType == "Minerals")
        {
            _mineralsAmount += amount;
            _mineralsAmountText.text = $"{_mineralsAmount}";
        }
    }

    private void _OnTimeSet(object data)
    {
        _clockText.text = (string)data;
    }

}
