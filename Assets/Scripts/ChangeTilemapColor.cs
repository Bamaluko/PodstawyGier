using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeTilemapColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("no_damage_water"))
        {
            Tilemap tiles = gameObject.GetComponent<Tilemap>();
            tiles.color = Color.green;
        }
        
    }
  
}
