using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Hero heroInfo;


    public void Spawn(Hero hero, Transform parent)
    {
        heroInfo = hero;
        spriteRenderer.sprite = heroInfo.heroIcon;
        parent.AddChild(gameObject);
    }

    private void OnMouseDown()
    {

    }
}
