using System.Text;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Block blockPrefab;
    private Block[] blocks;

    private bool Selected {get; set;}

    private void Start()
    {
        GenerateBlocks("CASA");
    }

    public void GenerateBlocks(string word)
    {
        word = word.ToUpper();

        blocks = new Block[word.Length];

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = Instantiate(blockPrefab.gameObject).GetComponent<Block>();
            blocks[i].Letter = word[i];
        }

    }

    public Vector3 PositionOfNextLetter()
    {
        return Vector3.zero;
    }

    public bool IsFilled()
    {
        return false;
    }

    public void SelectShelf()
    {

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
}