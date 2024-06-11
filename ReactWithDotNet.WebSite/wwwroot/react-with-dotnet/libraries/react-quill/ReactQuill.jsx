import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';

import { ImageResize } from 'quill-image-resize-module';

ReactQuill.register('modules/imageResize', ImageResize);

export default ReactQuill;