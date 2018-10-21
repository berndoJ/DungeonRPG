using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightFactory : MonoBehaviour
{
    /// <summary>
    /// The light source tilemap (each tile is a light source)
    /// </summary>
    public Tilemap LightSourceTilemap;

    /// <summary>
    /// The parent object of all light sources.
    /// </summary>
    public GameObject LightSourceParentObj;

	/// <summary>
    /// Init of this code.
    /// </summary>
	void Start ()
    {
        foreach (Vector3Int tilePos in this.LightSourceTilemap.cellBounds.allPositionsWithin)
        {
            TileBase tileBase = this.LightSourceTilemap.GetTile(tilePos);
            if (tileBase != null)
            {
                GameObject tileLightObj = new GameObject(string.Format("L_x{0}_y{1}_z{2}", tilePos.x, tilePos.y, tilePos.z));
                tileLightObj.transform.parent = this.LightSourceParentObj.transform;
                Light tileLight = tileLightObj.AddComponent<Light>();
                tileLight.type = LightType.Point;
                tileLight.range = 5F;
                tileLight.color = new Color(231F / 255, 191F / 255, 74F / 255);
                tileLight.renderMode = LightRenderMode.ForcePixel;
                tileLight.intensity = 4F;
                tileLight.transform.position = this.LightSourceTilemap.CellToWorld(tilePos) + new Vector3(0.5F, 0.5F, -0.6F);
            }
        }
	}
}
