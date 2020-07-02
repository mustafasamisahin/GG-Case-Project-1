using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectRandom : MonoBehaviour
{
    private int rnd;
    private int count;
    private int lastIndex;
    public GameObject[] skins;
    public Sprite[] sprites;
    private List<Button> buttons;
    private List<Image> images;
    private List<int> availableNumbers;

    void Start()
    {
        availableNumbers = new List<int>();
        buttons = new List<Button>();
        images = new List<Image>();

        for (int i = 0; i < 9; i++)
            availableNumbers.Add(i);

        foreach (var skin in skins)
        {
            buttons.Add(skin.GetComponent<Button>());
            images.Add(skin.GetComponent<Image>());
        }
    }

    public void pickRandomTile()
    {
        if (availableNumbers.Count == 0)
        {
            return;
        }
        for (int i = 0; i < 9; i++)
        {
            buttons[i].interactable = false;
            images[i].color = new Color32(128, 128, 128, 255);
        }

        int index = Random.Range(0, availableNumbers.Count - 1);
        while (lastIndex != -1 && lastIndex == index)
        {
            if (availableNumbers.Count == 2)
            {
                if (index == 0)
                    index = 1;
                else
                    index = 0;
            }
            else if (availableNumbers.Count == 1){
                count = 4;
                break;
            }
            else
                index = Random.Range(0, availableNumbers.Count - 1);
        }
        lastIndex = index;
        rnd = availableNumbers[index];
//
        skins[rnd].GetComponent<Button>().interactable = true;
        skins[rnd].GetComponent<Image>().color = new Color32(41, 135, 45, 255);

        count++;
        if (count == 5)
        {
            CancelInvoke();
            Invoke("unlockSkin", 1f);
            availableNumbers.RemoveAt(index);
        }
    }

    public void shuffle()
    {
        count = 0;
        lastIndex = -1;
        InvokeRepeating("pickRandomTile", 0f, 0.3f);
    }

    private void unlockSkin()
    {
        skins[rnd].transform.Find("SkinImage").GetComponent<Image>().sprite = sprites[rnd];
    }

}