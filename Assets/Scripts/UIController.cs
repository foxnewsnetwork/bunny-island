using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Action OnRoadPlacement, onWallPlacement, OnHousePlacement, OnSpecialPlacement;
    public Button placeRoadButton, placeWallButton, placeHouseButton, placeSpecialButton;

    public Color outlineColor;
    List<Button> buttonList;

    private void Start()
    {
        buttonList = new List<Button> { placeHouseButton, placeWallButton, placeRoadButton, placeSpecialButton };

        placeRoadButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();

        });
        placeWallButton.onClick.AddListener(() => {
            ResetButtonColor();
            ModifyOutline(placeWallButton);
            onWallPlacement?.Invoke();
        });
        placeHouseButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();

        });
        placeSpecialButton.onClick.AddListener(() =>
        {
            ResetButtonColor();
            ModifyOutline(placeSpecialButton);
            OnSpecialPlacement?.Invoke();

        });
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonColor()
    {
        foreach (Button button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
