using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables {get; private set;}
    public Transform player;
    public void StartListening(Story story){
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public StoryVariables(TextAsset globalStateJSON, Transform player){
        this.player = player;
        Story globalState = new Story(globalStateJSON.text);
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach(string name in globalState.variablesState){
            Ink.Runtime.Object val = globalState.variablesState.GetVariableWithName(name);
            variables.Add(name, val);
            Debug.Log("Init global var: " + name + " = " + val);
        }
    }

    public void StopListening(Story story){
        VariablesToStory(story);
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    void VariableChanged(string name, Ink.Runtime.Object value){
        Debug.Log("Variable changed: " + name + " = " + value);
        if(variables.ContainsKey(name)){
            if(name == "giveMedkit" && (Ink.Runtime.BoolValue) value == true){
                player.GetComponent<HealthItems>().medkitUsage(false);
            }
            if(name == "useMedkit" && (Ink.Runtime.BoolValue) value == true){
                player.GetComponent<HealthItems>().medkitUsage(true);
            }
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    void VariablesToStory(Story story){
        Debug.Log("Variables to story");
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables){
            Debug.Log("Variable: " + variable.Key + " " + variable.Value);
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}