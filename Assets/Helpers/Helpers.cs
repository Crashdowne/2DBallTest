using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

public static class Helpers
{
    public static GameObject FindChild(this GameObject parent, string name)
    {
        var childTransform = parent.transform.Find(name);

        if(childTransform == null)
        {
            return null;
        }

        return childTransform.gameObject;
    }
}
