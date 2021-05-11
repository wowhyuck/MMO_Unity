﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    [SerializeField]
    Text _text;

    int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
        _text.text = $"점수 : {_score}";
    }
}
