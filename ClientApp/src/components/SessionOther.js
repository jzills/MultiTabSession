import { List, ListItem, ListItemText } from "@mui/material"
import { Paper } from "@mui/material"

const SessionOther = ({data}) => {
    return (
        <Paper>
        <List
            sx={{
                width: "100%",
                maxWidth: "100%",
                bgcolor: "background.paper",
                position: "relative",
                overflow: "auto",
                maxHeight: 100,
                "& ul": { padding: 0 },
            }}
            subheader={<li />}
        >
            {
                data && data.length ? data.map(element => (
                    <ListItem key={element.id}>
                        <ListItemText primary={element.windowName} />
                    </ListItem>)
                ) : (<ListItem>
                        <ListItemText primary="No other sessions" />
                    </ListItem>)
            }
        </List>
        </Paper>
    )
}

export default SessionOther