
using System;
using UnityEngine;

//Base class for all objects that have info, abilities, names, basically anything you can highlight with the mouse
[Serializable]
public class DataHolder
{
    public Sprite typeIcon;
    public rarity rarity;
    public dataTags tags;
    
    public Sprite visual;
    public string name;
    public string description;

    public virtual void OnGameStart() { }
    public virtual void OnGameEnd() { }
    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
}

[Serializable]
public enum rarity
{
    common,
    rare,
    epic,
    legendary
}

//all tags for all objects. 
[Serializable]
public enum dataTags
{
    jelly,
    depth,
    shallow,
    predator
}
