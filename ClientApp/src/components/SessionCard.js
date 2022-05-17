import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import { convertToWords } from "../utilities/dataConversion"

const SessionCard = ({data}) => {
	const handleClick = () => window.open(window.origin, "_blank")

	return (
		<Card sx={{ minWidth: 500 }}>
			<CardContent>
			{
                Object.keys(data).map(key => <div key={key}>{convertToWords(key)}: {data[key] ?? "N/A"}</div>)
            }
			</CardContent>
			<CardActions>
				<Button size="small" onClick={handleClick}>Start New Session</Button>
			</CardActions>
		</Card>
	);
}

export default SessionCard