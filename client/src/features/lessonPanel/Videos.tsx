import Checkbox from "@mui/joy/Checkbox";

interface Props {
    completed: boolean[];
    onTheoryClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
    onPracticeClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function Videos({ completed, onTheoryClick, onPracticeClick }: Props) {
    const handleContextMenu = (event: React.MouseEvent<HTMLVideoElement>) => {
        event.preventDefault();
    }

    return (
        <>
            <div className="video-wrapper">
                <video src="\videos\🍒 УРОК 7. ТЕОРІЯ _ буквенні вирази, ОДЗ, властивості дробів.mp4" controls controlsList="nodownload" onContextMenu={handleContextMenu}></video>
                <Checkbox label="Theory" onChange={onTheoryClick} checked={completed[0]} sx={{ display: "flex" }} />
            </div>
            <div className="video-wrapper">
                <video src="\videos\🍒УРОК 7. ПРАКТИКА.mp4" controls controlsList="nodownload" onContextMenu={handleContextMenu}></video>
                {/* <iframe title="1" src="https://drive.google.com/file/d/1bmXiKjOnpqXDwXgWpqnnEBCXrBrofe2D/preview" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowFullScreen></iframe> */}
                <Checkbox label="Practice" onChange={onPracticeClick} checked={completed[1]} sx={{ display: "flex" }} />
            </div>
        </>
    )
}