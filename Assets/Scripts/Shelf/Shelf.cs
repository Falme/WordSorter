using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    [SerializeField] private Transform shelfBlocks;
    
    private Block[] blocks;
    private int blockPositionIndex = 0;

    private bool Selected {get; set;}

    public Vector3 PositionOfNextLetter =>
        new Vector3(0.8f * blockPositionIndex, 0f);

    public bool IsFilled => blocks[blocks.Length - 1] != null;
    
    private void Start()
    {
        StartCoroutine(GenerateBlocks("CASA", 4));
    }

    public IEnumerator GenerateBlocks(string word, int shelfSize)
    {
        if(IsWordBiggerThanShelf(word, shelfSize)) yield break;

        word = word.ToUpper();

        blocks = new Block[shelfSize];

        foreach (char letter in word)
        {
            var _block = Instantiate(blockPrefab.gameObject, shelfBlocks).GetComponent<Block>();
            _block.Letter = letter;

            AppendBlock(_block);

            yield return null;
        }

    }

    private bool IsWordBiggerThanShelf(string word, int shelfSize)
    {
        if (word.Length > shelfSize)
        {
            Debug.LogError("Number of Letters in a Shelf was Exceeded at GenerateBlocks");
            Debug.LogError("Make Sure to not put a longer word to a smaller shelf");
            return true;
        }
        return false;
    }

    public void SelectShelf()
    {
        Selected = !Selected;
        blocks[blockPositionIndex-1].Highlight(Selected);
    }

    private void AppendBlock(Block block)
    {
        block.transform.localPosition = PositionOfNextLetter;
        blocks[blockPositionIndex] = block;
        blockPositionIndex++;
    }

    public string Stringify()
    {
        StringBuilder _result = new StringBuilder();

        for(int a=0; a<blocks.Length;a++)
            _result.Append(blocks[a].Letter);

        return _result.ToString();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) Debug.Log(Stringify());
    }

    public void OnMouseDown() => SelectShelf();
}