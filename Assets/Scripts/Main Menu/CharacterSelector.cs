using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Vampire
{
    public class CharacterSelector : MonoBehaviour
    {
        [SerializeField] protected CharacterBlueprint[] characterBlueprints;
        [SerializeField] protected GameObject characterCardPrefab;
        [SerializeField] protected CoinDisplay coinDisplay;

        private CharacterCard[] characterCards;
        
        public void Init()
        {
            characterCards = new CharacterCard[characterBlueprints.Length];
            for (int i = 0; i < characterBlueprints.Length; i++)
            {
                characterCards[i] = Instantiate(characterCardPrefab, this.transform).GetComponent<CharacterCard>();
                characterCards[i].Init(this, characterBlueprints[i], coinDisplay);

                // Test Character 1 (index 1) hidden until identity pass
                // Test Character 2 (index 2) retained as locked/future test character
                // Full naming/art cleanup later PR.
                if (i == 1)
                {
                    characterCards[i].gameObject.SetActive(false);
                }
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            for (int i = 0; i < characterBlueprints.Length; i++)
            {
                if (characterCards[i].gameObject.activeSelf)
                {
                    characterCards[i].UpdateLayout();
                }
            }
        }
        
        public void StartGame(CharacterBlueprint characterBlueprint)
        {
            CrossSceneData.CharacterBlueprint = characterBlueprint;
            SceneManager.LoadScene(1);
        }
    }
}
