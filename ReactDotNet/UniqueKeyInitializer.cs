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


        public static void InitializeKeyIfNotExists(IReadOnlyList<ReactElement> siblings)
        {
            var key = 0;

            foreach (var sibling in siblings)
            {
                //if (sibling.RootElement != null)
                //{
                //    if (!sibling.RootElement.Props.ContainsKey("key"))
                //    {
                //        sibling.RootElement.Props["key"] = key++.ToString();
                //    }

                //    continue;
                //}

                if (!sibling.Props.ContainsKey("key"))
                {
                    sibling.Props["key"] = key++.ToString();
                }
            }
        }
    }


}