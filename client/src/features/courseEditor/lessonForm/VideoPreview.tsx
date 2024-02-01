import { ForwardedRef, forwardRef, useEffect } from "react";

interface Props {
    watchFile: any
}

const VideoPreview = forwardRef<HTMLVideoElement, Props>(
    ({ watchFile }, ref: ForwardedRef<HTMLVideoElement>) => {
        useEffect(() => {
            if (watchFile && watchFile.preview && ref && 'current' in ref) {
                ref.current?.load();
            }
        }, [watchFile, ref]);

        return (
            watchFile && (
                <video ref={ref} style={{ maxHeight: 200 }} controls>
                    <source src={watchFile.preview} type={watchFile.type} />
                    Your browser does not support the video tag.
                </video>
            )
        );
    }
);

export default VideoPreview;
