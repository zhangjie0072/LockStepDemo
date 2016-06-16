using UnityEngine;
using System.Collections;

public class AloneImageHolder : MonoBehaviour {

    public string name { get; set; }
    public bool SaveToBytes;
    public Texture Texture { get; set; }
    public string Desc_ImageName { get; set; }
    public int Desc_ImageWidth { get; set; }
    public int Desc_ImageHeight { get; set; }
    //读取原始图片文件二进制数据到宿主脚本中
    public byte[] ImageData;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
