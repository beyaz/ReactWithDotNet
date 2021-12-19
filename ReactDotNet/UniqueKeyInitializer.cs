using System;
using System.Collections.Generic;

namespace ReactDotNet
{
    static class UniqueKeyInitializer
    {
        public static void InitializeKeyIfNotExists(IReadOnlyList<IElement> siblings)
        {
            var key = 0;

            foreach (var sibling in siblings)
            {
                if (sibling.key == null)
                {
                    sibling.key = key++.ToString();
                }
            }
        }
    }


}