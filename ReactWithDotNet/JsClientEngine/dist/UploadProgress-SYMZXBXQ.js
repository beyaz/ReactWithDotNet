import "./chunk-UQVZSXQB.js";
import {
  useItemProgressListener
} from "./chunk-FB6RAV5B.js";
import "./chunk-QNQS7X5M.js";
import {
  require_react
} from "./chunk-XDFXK7K5.js";
import {
  __toESM
} from "./chunk-QRPWKJ4C.js";

// react-with-dotnet/libraries/_uploady_/UploadProgress.jsx
var import_react = __toESM(require_react());
var UploadProgress = () => {
  const [uploads, setUploads] = import_react.default.useState({});
  const progressData = useItemProgressListener();
  if (progressData && progressData.completed) {
    const upload = uploads[progressData.id] || { name: progressData.url || progressData.file.name, progress: [0] };
    if (!~upload.progress.indexOf(progressData.completed)) {
      upload.progress.push(progressData.completed);
      setUploads({
        ...uploads,
        [progressData.id]: upload
      });
    }
  }
  const entries = Object.entries(uploads);
  return /* @__PURE__ */ import_react.default.createElement("div", null, entries.map(([id, { progress, name }]) => {
    const lastProgress = progress[progress.length - 1];
    return /* @__PURE__ */ import_react.default.createElement("progress", { key: id, max: 100, value: lastProgress });
  }));
};
var UploadProgress_default = UploadProgress;
export {
  UploadProgress_default as default
};
