using JAM.Entities._Player;
using JAM.TileMap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JAM.Spells
{
    public class SpellsShooter : MonoBehaviour
    {
        public List<Spell> _spells;
        private TilemapChecker _tilemapChecker = new TilemapChecker();
        [SerializeField] private Spell _currentSpell;


        void Start()
        {
            if (_spells == null)
            {
                Debug.Log("Upload spells TITAN");
                return;
            }

        }

        public bool ShootSpell(Vector3Int start, Vector3Int tile)
        {
            if (TileMapManager.Instance.IsDistanceValid(start, tile, _currentSpell.SpellStats.TilesLength))
            {
                var pos = new Vector3Int(start.x, start.y, 0);
                if (_currentSpell.SpellStats.DirectionOfAttack == DirectionOfAttack.CROSS)
                {
                    
                    if (_tilemapChecker.CheckTilesInCross(pos, tile, _currentSpell.SpellStats.TilesLength, TileMapManager.Instance.Tilemap))
                    {
                        var instaciateSpell = Instantiate(_currentSpell, Player.Instance.transform);
                        instaciateSpell.transform.position = pos;
                        instaciateSpell.Instaciate(tile);
                        return true;
                    }
                }
                if (_currentSpell.SpellStats.DirectionOfAttack == DirectionOfAttack.STAR)
                {
                    if (_tilemapChecker.CheckTilesInCrossAndDiagonals(pos, tile, _currentSpell.SpellStats.TilesLength, TileMapManager.Instance.Tilemap))
                    {
                        var instaciateSpell = Instantiate(_currentSpell, Player.Instance.transform);
                        instaciateSpell.transform.position = pos;
                        instaciateSpell.Instaciate(tile);
                        return true;
                    }
                }

            }
            return false;
        }



        public void SetSpell(string spellName)
        {
            _currentSpell = _spells.Find(spell => spell.name == spellName);

            if (_currentSpell == null)
            {
                Debug.Log($"No se encontro el hechizo");
                return;
            }

        }

    }
}
