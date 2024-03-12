import AddCarModal from "../Components/Car/AddCarModal";
import CarTable from "../Components/Car/CarTable";

const ManageCarPage = () => {
    return (
        <div className="Container">
            <h1>Cars: </h1> 
            <AddCarModal/>        
            <CarTable/>
        </div>
    )
}

export default ManageCarPage;