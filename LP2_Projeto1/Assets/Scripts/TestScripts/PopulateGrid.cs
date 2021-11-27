using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int numberToCreate;

    private void Start()
    {
        Populate();
    }

    private void Populate()
    {
        GameObject newObj;

        for (int i = 0; i < numberToCreate; i++)
        {
            newObj = (GameObject)Instantiate(prefab, transform);
            newObj.GetComponent<Image>().color = Random.ColorHSV();
        }
    }
}
