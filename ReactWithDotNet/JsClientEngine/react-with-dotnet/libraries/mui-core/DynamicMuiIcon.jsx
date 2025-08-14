import * as MuiIcons from "@mui/icons-material";

import React from "react";

export default function DynamicMuiIcon({ name, ...props })
{
  const IconComponent = MuiIcons[name];
  if (!IconComponent) 
  {
    console.warn(`Icon "${name}" not found in @mui/icons-material`);
    return null;
  }

  return <IconComponent {...props} />;
}
