import React from 'react';

import { useItemProgressListener } from "@rpldy/uploady";

const UploadProgress = () =>
{
    const [uploads, setUploads] = React.useState({});
    const progressData = useItemProgressListener();

    if (progressData && progressData.completed)
    {
        const upload = uploads[progressData.id] ||
            { name: progressData.url || progressData.file.name, progress: [0] };

        if (!~upload.progress.indexOf(progressData.completed))
        {
            upload.progress.push(progressData.completed);

            setUploads({
                ...uploads,
                [progressData.id]: upload,
            });
        }
    }

    const entries = Object.entries(uploads);

    return <div>
        {entries
            .map(([id, { progress, name }]) =>
            {
                const lastProgress = progress[progress.length - 1];

                return <progress key={id} max={100} value={lastProgress} />;
            })}
    </div>;
};

export default UploadProgress;