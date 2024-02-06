import { PlayArrow } from "@mui/icons-material";
import { Button } from "@mui/material";
import { ForwardedRef, forwardRef, useEffect, useState } from "react";

interface Props {
    initialVideoUrl?: string
    newVideoUrl?: string
}

const VideoPreview = forwardRef<HTMLVideoElement, Props>(
    ({ initialVideoUrl, newVideoUrl }, ref: ForwardedRef<HTMLVideoElement>) => {
        const [showVideo, setShowVideo] = useState(false);

        useEffect(() => {
            if (newVideoUrl) {
                setShowVideo(true);
            }
        }, [newVideoUrl]);

        function getCloudinaryVideoThumbnail(videoUrl: string): string {
            const urlParts = videoUrl.split('/');

            urlParts.splice(6, 0, 'so_0');

            const lastPartIndex = urlParts.length - 1;
            urlParts[lastPartIndex] = urlParts[lastPartIndex].replace(/\.\w+$/, '.jpg');

            return urlParts.join('/');
        }

        const handleDownloadClick = () => {
            setShowVideo(true);
        };

        const videoKey = newVideoUrl || initialVideoUrl;

        return (
            <>
                {showVideo ? (
                    <video key={videoKey} ref={ref} style={{ maxWidth: '100%', maxHeight: 200, flex: '0 1 auto' }} controls>
                        <source src={newVideoUrl || initialVideoUrl} type="video/mp4" />
                        Ваш браузер не підтримує відео тег.
                    </video>
                ) : initialVideoUrl ? (
                    <Button
                        onClick={handleDownloadClick}
                        style={{
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                            maxWidth: '100%',
                            width: '300px',
                            height: '200px',
                            backgroundImage: `url(${getCloudinaryVideoThumbnail(initialVideoUrl)})`,
                            backgroundSize: 'cover',
                            backgroundPosition: 'center',
                            border: '1px solid black'
                        }}>
                        <PlayArrow sx={{ fontSize: '70px' }} htmlColor="#ce7556" />
                    </Button>
                ) : null}
            </>
        );
    }
);

export default VideoPreview;
