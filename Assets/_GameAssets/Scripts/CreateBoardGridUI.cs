using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class CreateBoardGridUI : MonoBehaviour
    {
        [SerializeField] RectTransform BoardGrid = null;
        [SerializeField] Sprite SpriteTile = null;

        private float tileSize = 100;

        public void GenerateGrid(int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    float posX = col * tileSize;
                    float posY = row * -tileSize;

                    GameObject tile = new GameObject("tile", typeof(Image));
                    tile.gameObject.transform.SetParent(BoardGrid);
                    Image img = tile.GetComponent<Image>();
                    img.sprite = SpriteTile;

                    tile.transform.position = new Vector2(-1 * BoardGrid.rect.x + posX, -1 * BoardGrid.rect.y + posY);
                }
            }
        }
    }
}
