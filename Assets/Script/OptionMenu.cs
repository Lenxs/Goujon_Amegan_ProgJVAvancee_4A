using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    [SerializeField]private GameObject optionMenu;

    public void ReturnButton()
    {
        optionMenu.SetActive(false);
    }
}
