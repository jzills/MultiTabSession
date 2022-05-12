import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import { convertToWords } from "../utilities/dataConversion"

const SessionCard = ({data}) => {
	return (
		<Card sx={{ minWidth: 275 }}>
			<CardContent>
			{
                Object.keys(data).map(key => <div>{convertToWords(key)}: {data[key] ?? "N/A"}</div>)
            }
			</CardContent>
			<CardActions>
				<Button size="small">Learn More</Button>
			</CardActions>
		</Card>
	);
}

export default SessionCard