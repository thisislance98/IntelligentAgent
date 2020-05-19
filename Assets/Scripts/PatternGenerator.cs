using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PatternGenerator : MonoBehaviour
{
    public List<TMP_FontAsset> Fonts = new List<TMP_FontAsset>();
    public float MinRotation;
    public float MaxRotation;
    public float MinScale;
    public float MaxScale;

    public float MinTranslation;
    public float MaxTranslation;

    public TextMeshPro Text;

    Vector3 _startPos;

    public static PatternGenerator Instance;


    void Awake() {
        Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        
        _startPos = Text.transform.position;
        Generate();
    }

    public void Generate() {

        Text.font = Fonts[Random.Range(0,Fonts.Count)];
        Text.transform.rotation = Quaternion.Euler(0,0,Random.Range(MinRotation,MaxRotation));

        Text.transform.position = _startPos + new Vector3(Random.Range(MinTranslation,MaxTranslation),Random.Range(MinTranslation,MaxTranslation),0);
    
        Text.transform.localScale = Vector3.one * Random.Range(MinScale,MaxScale);
        Text.text = Random.Range(0,10).ToString();
    }
}
