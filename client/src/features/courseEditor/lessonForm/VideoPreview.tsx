import { ForwardedRef, forwardRef, useEffect } from "react";

interface Props {
    videoUrl?: string
}

const VideoPreview = forwardRef<HTMLVideoElement, Props>(
    ({ videoUrl }, ref: ForwardedRef<HTMLVideoElement>) => {
        useEffect(() => {
            if (ref && 'current' in ref) {
                ref.current?.load();
            }
        }, [ref]);

        return (
            <>
                {videoUrl ? (
                    <video ref={ref} style={{ maxWidth: '100%',  maxHeight: 200, flex: '0 1 auto' }} controls>
                        <source src={videoUrl} type="video/mp4" />
                        Your browser does not support the video tag.
                    </video>
                ) : null}
            </>
        );
    }
);

export default VideoPreview;
