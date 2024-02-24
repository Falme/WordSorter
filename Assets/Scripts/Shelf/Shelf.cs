using System.Collections;
using System.Text;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [Header("Prefab Reference Values")]
    [SerializeField] private Block blockPrefab;
    [SerializeField] private Transform shelfBlocks;
    
    [Header("Shelf Generation Data")]
    [SerializeField] private string word;
    [SerializeField] private int shelfSize;
    
    private Block[] blocks;
    private int blockPositionIndex = 0;

    private bool Selected {get; set;}

    public Vector3 PositionOfNextLetter =>
        new Vector3(0.8f * blockPositionIndex, 0f);

    public bool IsFilled => blocks[blocks.Length - 1] != null;
    
    private void Start()
    {
        StartCoroutine(GenerateBlocks(word, shelfSize));
    }

    public IEnumerator GenerateBlocks(string _word, int _shelfSize)
    {
        if(IsWordBiggerThanShelf(_word, _shelfSize)) yield break;

        _word = _word.ToUpper();

        blocks = new Block[_shelfSize];

        foreach (char letter in _word)
        {
            var _block = Instantiate(blockPrefab.gameObject, shelfBlocks).GetComponent<Block>();
            _block.Letter = letter;

            AppendBlock(_block);

            yield return null;
        }

    }

    public void SelectShelf()
    {
        Selected = !Selected;
        blocks[blockPositionIndex-1].Highlight(Selected);
    }

    private void AppendBlock(Block block)
    {
        if(IsFilled)
        {
            Debug.LogError("Trying to Add a Letter Block to a Filled Shelf");
            return;
        }

        block.transform.localPosition = PositionOfNextLetter;
        blocks[blockPositionIndex] = block;
        blockPositionIndex++;
    }

    private void DetachBlock(Block block)
    {

    }

    public string Stringify()
    {
        StringBuilder _result = new StringBuilder();

        for(int a=0; a<blocks.Length;a++)
            _result.Append(blocks[a].Letter);

        return _result.ToString();
    }

    private bool IsWordBiggerThanShelf(string _word, int _shelfSize)
    {
        if (_word.Length > _shelfSize)
        {
            Debug.LogError("Number of Letters in a Shelf was Exceeded at GenerateBlocks");
            Debug.LogError("Make Sure to not put a longer word to a smaller shelf");
            return true;
        }
        return false;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) Debug.Log(Stringify());
    }

    public void OnMouseDown() => SelectShelf();
}