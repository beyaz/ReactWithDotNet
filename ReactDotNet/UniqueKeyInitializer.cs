using System;
using System.Collections.Generic;

namespace ReactDotNet
{
    static class UniqueKeyInitializer
    {
      


        public static void InitializeKeyIfNotExists(IReadOnlyList<ReactElement> siblings)
        {
            var key = 0;

            foreach (var sibling in siblings)
            {
                

                if (!sibling.Props.ContainsKey("key"))
                {
                    sibling.Props["key"] = key++.ToString();
                }
            }
        }
    }


}