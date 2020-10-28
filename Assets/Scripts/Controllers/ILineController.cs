using System;
using UnityEngine;

public interface ILineController
{
    void SelectField(GameObject go);
    void ClearLine(bool value);

    event EventHandler<TwoFieldsSelectedEventArgs> OnTwoFieldsSelected;
}
