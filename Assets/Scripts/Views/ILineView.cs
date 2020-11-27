using System;
using UnityEngine;

public interface ILineView
{
    void SelectField(GameObject go);
    void ClearLine(bool value);

    event EventHandler<TwoFieldsSelectedEventArgs> OnTwoFieldsSelected;
}
