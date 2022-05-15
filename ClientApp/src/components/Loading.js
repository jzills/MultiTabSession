import { Box, CircularProgress } from "@material-ui/core"

const Loading = () => {
    return (
        <Box
            display="flex"
            justifyContent="center"
            alignItems="center"
            minWidth="100vw"
            minHeight="100vh"
        >
            <CircularProgress />
        </Box> 
    )
}

export default Loading