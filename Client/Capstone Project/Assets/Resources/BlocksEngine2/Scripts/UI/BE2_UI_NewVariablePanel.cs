﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BE2_UI_NewVariablePanel : MonoBehaviour
{
    Button _buttonCreate;
    InputField _inputVarName;

    public Transform variablePanelTemplate;

    void Awake()
    {
        _buttonCreate = transform.GetChild(2).GetComponent<Button>();
        _inputVarName = transform.GetChild(1).GetComponent<InputField>();
    }

    void Start()
    {
        _buttonCreate.onClick.AddListener(OnButtonCreateVariable);
    }

    //void Update()
    //{
    //
    //}

    void OnButtonCreateVariable()
    {
        string varName = _inputVarName.text;
        if (varName != "")
        {
            CreateVariable(varName);
        }
    }

    public void CreateVariable(string varName)
    {
        Transform newVarPanel = Instantiate(variablePanelTemplate, Vector3.zero, Quaternion.identity, transform.parent);
        newVarPanel.SetSiblingIndex(transform.GetSiblingIndex() + 1);

        I_BE2_Block newBlock = newVarPanel.GetChild(0).GetComponent<I_BE2_Block>();
        
        // v2.1 - using BE2_Text to enable usage of Text or TMP components
        //                           | block     | section   | header    | text      |
        //Text newVarName = newVarPanel.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        BE2_Text newVarName = BE2_Text.GetBE2Text(newVarPanel.GetChild(0).GetChild(0).GetChild(0).GetChild(0)); 
        newVarName.text = varName;

        BE2_VariablesManager.instance.AddOrUpdateVariable(varName, "0");

        newVarPanel.GetComponent<BE2_UI_VariableViewer>().RefreshViewer();
    }
}
