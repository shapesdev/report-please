using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRuleDisplayer
{
    event EventHandler<PageClosedEventArgs> OnPageBack;
}
