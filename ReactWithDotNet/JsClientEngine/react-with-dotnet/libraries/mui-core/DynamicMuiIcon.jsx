import React, { Suspense } from "react";

/**
 * DynamicMuiIcon
 * 
 * @param {string} name 
 * @param {object} props
 */
export default function DynamicMuiIcon({ name, ...props }) {
  if (!name) return null;

  const Icon = React.lazy(() =>
    import(`@mui/icons-material/${name}`)
      .then(module => ({ default: module.default }))
      .catch(() => {
        console.warn(`Icon "${name}" not found in @mui/icons-material`);
        return { default: () => null };
      })
  );

  return (
    <Suspense fallback={null}>
      <Icon {...props} />
    </Suspense>
  );
}
